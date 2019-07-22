@echo off

echo "Creating initial migration"
dotnet ef migrations add InitialMigration

echo "Running database updates"
dotnet ef database update

echo "Copying sqlite database file to running directory, this will overwrite any existing db file"
xcopy /y applevalAPI.db ..\applevalapi\applevalAPI.db

echo "We will start the database seeding process..."

echo "cd ..\applevalapi"
cd ..\applevalapi
echo "Building project..."
dotnet build

echo "Running project for fist time and seeding the database"

dotnet run



