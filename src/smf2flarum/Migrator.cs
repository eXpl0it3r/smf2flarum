using System.Security.Policy;
using Mapping.Flarum185;
using Microsoft.EntityFrameworkCore;
using Schema.Flarum185;
using Schema.Smf2019;

namespace smf2flarum;

public class Migrator
{
    private readonly SmfContext _smfContext;
    private readonly FlarumContext _flarumContext;

    public static Migrator Create(Options options)
    {
        var smfContext = new SmfContext(new DbContextOptionsBuilder<SmfContext>().UseMySQL(options.SmfConnectionString).Options);
        var flarumContext = new FlarumContext(new DbContextOptionsBuilder<FlarumContext>().UseMySQL(options.FlarumConnectionString).Options);

        return new Migrator(smfContext, flarumContext);
    }

    public async Task ExecuteAsync()
    {
        Console.WriteLine("=== Migration Start ===");

        var mapper = MapperFactory.Create();

        Console.WriteLine("-- Migrating User & GroupUser --");

        /* === Group Mappings ===
         * 1 Administrator    => 1 Admin
         * 2 Global Moderator => 4 Mod
         * 3 Moderator        => 4 Mod
         * 4 Newbie           => 3 Member
         * 5 Jr. Member       => 3 Member
         * 6 Full Member      => 3 Member
         * 7 Sr. Member       => 3 Member
         * 8 Hero Member      => 3 Member
         * XYZ                => NEW
         */
        var groupMapping = new Dictionary<int, uint>
        {
            {
                1, 1
            },
            {
                2, 4
            },
            {
                3, 4
            },
            {
                4, 3
            },
            {
                5, 3
            },
            {
                6, 3
            },
            {
                7, 3
            },
            {
                8, 3
            }
        };
        
        foreach (var membergroup in _smfContext.Membergroups.Where(IsUnknownSmfMemberGroup))
        {
            Console.WriteLine($"Adding non-standard Group: {membergroup.IdGroup} {membergroup.GroupName}");

            if (await _flarumContext.Groups.AnyAsync(g => g.Id == membergroup.IdGroup))
            {
                Console.WriteLine("Group already exists, skipping...");
                continue;
            }
            
            var newGroup = mapper.Map<Group>(membergroup);
            await _flarumContext.Groups.AddAsync(newGroup);
            
            groupMapping.Add(membergroup.IdGroup, newGroup.Id);
        }

        await _flarumContext.SaveChangesAsync();
        
        foreach (var member in _smfContext.Members)
        {
            Console.WriteLine($"Adding User: {member.IdMember} {member.MemberName}");

            if (await _flarumContext.Users.AnyAsync(u => u.Id == member.IdMember))
            {
                Console.WriteLine("User already exists, skipping...");
                continue;
            }

            var newUser = mapper.Map<User>(member);
            await _flarumContext.Users.AddAsync(newUser);
            await _flarumContext.SaveChangesAsync();
            
            Console.WriteLine($"Adding GroupUser: {newUser.Id} {groupMapping[member.IdGroup]}");

            if (await _flarumContext.GroupUsers.AnyAsync(gu => gu.UserId == newUser.Id && gu.GroupId == groupMapping[member.IdGroup]))
            {
                Console.WriteLine("GroupUser already exists, skipping...");
                continue;
            }
            
            var newGroupUser = mapper.Map<GroupUser>(member);
            newGroupUser.GroupId = groupMapping[member.IdGroup];
            await _flarumContext.GroupUsers.AddAsync(newGroupUser);
        }
        
        await _flarumContext.SaveChangesAsync();
        
        foreach (var user in _flarumContext.Users)
        {
            Console.WriteLine($"{user.Id} {user.Username}");
        }
        
        Console.WriteLine("=== Migration End ===");
    }

    private static bool IsUnknownSmfMemberGroup(Membergroup membergroup)
    {
        return membergroup.GroupName switch
        {
            "Administrator" => false,
            "Global Moderator" => false,
            "Moderator" => false,
            "Newbie" => false,
            "Jr. Member" => false,
            "Full Member" => false,
            "Sr. Member" => false,
            "Hero Member" => false,
            _ => true
        };
    }

    private Migrator(SmfContext smfContext, FlarumContext flarumContext)
    {
        _smfContext = smfContext;
        _flarumContext = flarumContext;
    }
}