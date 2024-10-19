# trainer-journal-backend

[Link to the frontend project](https://github.com/1zbbxzak1/trainer-journal-frontend)

_Developed for the UrFu Project-Practice 2024 (5th semester)_

## Getting Started

1. Clone the repository:
```bash
git clone https://github.com/AnTaif/trainer-journal-backend
```
2. Navigate to the project directory:
```bash
cd trainer-journal-backend
```
3. Create .env file by following [.env.example](https://github.com/AnTaif/trainer-journal-backend/blob/main/.env.example)
4. Build and run the application using Docker Compose:
```bash
docker-compose up -d
```
Once the api is running, you can access it at http://localhost:8080 or swagger docs at http://localhost:8080/swagger
To stop the application and remove the containers, run:
```bash
docker-compose down
```

## Migrations:
1. Navigate to the Infrastructure project:
```bash
cd src/TrainerJournal.Infrastructure
```
2. Make changes to db models and add migration:
```bash
dotnet ef migrations add <Name> -s ../TrainerJournal.API
```
3. Update database:
```bash
dotnet ef database update -s ../TrainerJournal.API
```