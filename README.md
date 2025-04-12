FieldNotes - A scientific observation logger

#### Setting up the environment variable

##### When running locally

You can use the shell or in the `.env` file.

```shell
export DB_CONNECTION_STRING="Host=localhost;Database=fieldnotes;Username=postgres;Password=postgres"
```

Or if using the `.env` file, export it before running the application.

```shell
source .env
dotnet run --project Api
```

##### When running in Docker

Have the envrionment variable in the `docker-compose.yml` file.

