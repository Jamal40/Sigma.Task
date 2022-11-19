# Sigma.Task

## Description
The task has only one single endpoint, through which you can add or update a candidate. If the candidate already exists, they're updated, otherwise added.

## Used Technology
.Net Core 6.0

## Architecture
The solution has main three layers:

### 1- DAL (Data Access Layer): 
It has the store(SQL Database/Excel/..) models and repositories. Repositories are the services used by other layers to deal with our store.

### 2- BL (Business Layer)
It has the business logic in form of Managers Classes, like UI validation, mapping to DTOs, as Presentation Layer shouldn't be dependent on Store Models.

### 3- PL (Presentation Layer)
It has Web APIs to achieve the business through Managers.

## How to run:
- Clone the repo or download it.
- Open it using VS.
- Run the app.
- Check out the CSV file.

## Potential Improvements:
- Adding other endpoints to retriev Data from our Store.
- Cache the list returned from the store, if the list isn't frequently updated.
- Caching should be handled in the Resource Filter stage (After Authorization and before Model Binding).
- Using Real Database instead of csv like (MongoDb Or Sql Server etc.

## Duration 
4-6 hours.
