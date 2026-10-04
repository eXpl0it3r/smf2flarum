# SMF to Flarum Migration Converter

This project provides a complete migration tool to convert Simple Machines Forum (SMF 2.0+) data to Flarum 1.8.5 format.

## Features

The converter handles the migration of the following entities:

### Core Entities
- **Users**: Converts SMF members to Flarum users with proper mapping of usernames, emails, join dates, post counts, and user preferences
- **Groups**: Maps SMF member groups to Flarum groups, including custom groups beyond the standard ones
- **Group Memberships**: Maintains user-group relationships

### Forum Structure
- **Boards → Tags**: Converts SMF boards to Flarum tags, preserving hierarchy and order
- **Topics → Discussions**: Migrates forum topics to discussions with proper title extraction from first post
- **Messages → Posts**: Converts all messages to posts, maintaining chronological order and post numbering

### Relationships
- **Discussion-Tag Links**: Automatically creates relationships between discussions and their corresponding tags
- **Post Hierarchy**: Properly handles first posts vs. reply posts
- **User Associations**: Maintains all user-content relationships

## Architecture

### Project Structure
```
src/
├── Schema/           # Entity Framework models for both SMF and Flarum databases
│   ├── Flarum185/   # Flarum 1.8.5 schema
│   └── Smf2019/     # SMF 2.0+ schema
├── Mapping/          # AutoMapper profiles for entity mapping
│   └── Flarum185/   # Mapping profiles for Flarum entities
└── smf2flarum/      # Main migration application
```

### Key Components

#### Mapping Profiles
- `UserProfile`: Maps SMF Member → Flarum User
- `GroupProfile`: Maps SMF Membergroup → Flarum Group  
- `GroupUserProfile`: Maps SMF Member → Flarum GroupUser relationship
- `BoardProfile`: Maps SMF Board → Flarum Tag
- `TopicProfile`: Maps SMF Topic → Flarum Discussion
- `MessageProfile`: Maps SMF Message → Flarum Post

#### Migration Logic
- `Migrator`: Main orchestrator handling the complete migration process
- `MigrationStats`: Tracks and reports migration statistics
- Transaction support for data integrity
- Progress reporting and error handling

## Usage

```bash
dotnet run -- migrate --smf "SMF_CONNECTION_STRING" --flarum "FLARUM_CONNECTION_STRING"
```

### Connection String Examples
```
SMF: "Server=localhost;Database=smf;User=username;Password=password;"
Flarum: "Server=localhost;Database=flarum;User=username;Password=password;"
```

## Migration Process

The migration follows this sequence:

1. **Groups & Users**: Migrates user accounts and group memberships
2. **Boards → Tags**: Converts forum structure to Flarum's tag system
3. **Topics → Discussions**: Creates discussions from forum topics
4. **Messages → Posts**: Migrates all message content to posts
5. **Relationships**: Links discussions to tags and maintains associations

## Features & Safeguards

### Data Integrity
- **Transaction Support**: Full rollback capability on errors
- **Duplicate Detection**: Skips already migrated entities
- **Validation**: Checks for required data before migration

### Progress Tracking
- Real-time progress indicators with counts
- Detailed migration statistics
- Error reporting with full stack traces

### Data Mapping
- **Group Mapping**: Standard SMF groups mapped to Flarum equivalents
- **Slug Generation**: Automatic URL slug creation for discussions and tags
- **Timestamp Conversion**: Unix timestamps properly converted to DateTime
- **Content Preservation**: Full message content and metadata retained

## Standard Group Mappings

| SMF Group | Flarum Group |
|-----------|--------------|
| Administrator | Admin (1) |
| Global Moderator | Mod (4) |
| Moderator | Mod (4) |
| Newbie | Member (3) |
| Jr. Member | Member (3) |
| Full Member | Member (3) |
| Sr. Member | Member (3) |
| Hero Member | Member (3) |

Custom groups are preserved with their original IDs.

## Requirements

- .NET 10.0+
- MySQL/MariaDB for both source and target databases
- Entity Framework Core 10.0+
- AutoMapper 14.0+

## Limitations

- **Attachments**: File attachments are not migrated (would require file system access)
- **Private Messages**: Personal messages are not included in this migration
- **Custom Fields**: SMF custom profile fields are not migrated
- **Permissions**: Board-specific permissions are not carried over
- **Themes**: Visual customizations are not migrated

## Development Notes

The project uses:
- **Entity Framework Core** for database access
- **AutoMapper** for object-to-object mapping
- **ConsoleAppFramework** for CLI interface
- **MySQL.EntityFrameworkCore** for MySQL connectivity

All mapping logic is centralized in dedicated profiles, making it easy to modify mapping behavior or add new entity types.