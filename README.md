# smf2flarum

This tool allows migrating the database for [SMF (Simple Machine Forum)](https://www.simplemachines.org/) to [Flarum](https://flarum.org/).

## Usage

```
smf2flarum --smf <connection string for smf> --flarum <connection string for flarum>
```

## Architecture

The idea behind the tool is relatively simple:
- Generate the schemas of the databases via `dotnet ef dbcontext scaffold`
- Create AutoMapper profiles to map data from SMF to Flarum, including custom transformations
- Run the migration

Design decisions were made to keep the architecture extensible:
- The DB schemas are versioned, which could allow for migrations of different versions
- The DB schemas are limited to the default installations, while considering that fields from extensions/addons will need to be registered and migrated separately

## Extending

If you want to add your own schema versions, here are the commands used to generate the existing schemas:
```
cd src/Schema
dotnet ef dbcontext scaffold "<connection string>" MySql.EntityFrameworkCore -o <directory> -f
```

- `<connection string>` could be something like `server=localhost;user id=flarum;password=<password>;database=flarum`
- `<directory>` could be something like `Flarum185`, where `185` represents version 1.8.5 of Flarum

**Important:** Don't forget to remove the connection string credentials from the generated DB context!

## Credits

Some ideas have been inspired by the [smf2_to_flarum](https://github.com/ItalianSpaceAstronauticsAssociation/smf2_to_flarum) migration scripts.

### Dependencies

- [CommandLineParser](https://github.com/commandlineparser/commandline)
- [AutoMapper](https://github.com/AutoMapper/AutoMapper)

## License

This software is dual licensed under the Public Domain and the MIT license, choose whichever you prefer. See the LICENSE.md file for more details.
