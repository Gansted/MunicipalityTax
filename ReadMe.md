# Municipality Tax Service

The municipality tax service is implemented as a rest API. The API allows you to create, update and query municipality taxes.

The service requires .Net Core 3.0 and a LocalDB instance. It can be run from Visual Studio.

By default the service will start on ``http://localhost:2796/`` to modify this change the applicationUrl in the ``launchsettings.json`` file.

## API
### Create municipality tax record
The creation of a single municipality tax record is supported in the ``AddTax`` endpoint (http://localhost:2796/AddTax with default setup).

The endpoint expects a POST request with the tax record in the body of the request and content type JSON.

Example body:
```
{
    "Municipality": "copenhagen",
    "Period": "Week",
    "StartDate": "2017-01-02",
    "Tax": 0.5
}
```

### Create municipality tax records from file
The creation of multiple tax records is supported in the ``AddTaxFromFile`` endpoint (http://localhost:2796/AddTaxFromFile with default setup). 

The endpoint expects a CSV file with format municipality,period,start date,tax posted as form-data.

For an example file see ``MunicipalityTaxesEample.csv`` in the root of the repository.

### Update municipality tax record
Updates of records is support in the ``UpdateTax`` endpoint (http://localhost:2796/UpdateTax with default setup).

The endpoint expects a PUT request with the updated tax record in the request body and content type JSON. The tax record of the request must match the municipality, period, and startDate of the record which should be updated.

Example body:
```
{
    "Municipality": "copenhagen",
    "Period": "Week",
    "StartDate": "2017-01-02",
    "Tax": 0.3
}
```
### Get municipality tax
The retrieval of the tax percentage for a given municipality on a given date is supported in the ``GetTax`` endpoint (http://localhost:2796/GetTax with default setup).

The endpoint expects a GET request with municipality and date as querystring parameters.

Example request:
``http://localhost:2796/GetTax?municipality=Vilnius&taxDate=2016.05.02``

## Consumation examples
The project includes the [Postman](https://www.getpostman.com/) collection ``MunicipalityTax.postman_collection.json`` with examples of requests to each endpoint.