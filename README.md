# RjnClarityApi
 Sample applications and resources to get you started with the RJN Clarity API.

## Table of Contents  
[Getting Started](#getting-started)  <br>
[Documentation in Swagger](#documentation-in-swagger)  <br>
[Run in Postman](#run-in-postman)  <br>
[Easy NET Integration](#easy-net-integration)<br>
[Sample Projects](#sample-projects)<br>
[Basic Concepts](#basic-concepts)

## Getting Started

There are several ways to get started using the RJN Clarity API. This repository contains a variety of resources that document, demonstrate, and allow you to explore the API and determine how to integrate it into your work flow.

In order to start using the API, you must be granted access as follows:

1. Credentials: Before you can utilize the RJN Clarity API, you must be a client of RJN Group and receive a user/password from RJN staff. Theses credentials will be linked to your account and data store.
2. Authenticate: You will need to use your credentials to generate a token, which is valid for 24 hours.
3. Get Data: Once you get your token, you may use it to call any of the available URLs in the API. To submit the token, issue your request as an Authorization header with a value of "Bearer [your token]" 

The sections below describe how to use the various resources at your disposal.


## Documentation in Swagger

To explore the API in Swagger:

1. Navigate to the [Swagger](https://app.swaggerhub.com/apis/rjn_group/RjnClarityRestApi/1.0.0) documentation.
2. Run the "POST /auth" route in the General category. Make sure to edit the body of the request, substituting your "client_id" and "password".
3. Copy the "token" value in the response.
4. Click the green "Authorize" button and paste the token in the "Value" field. Then click "Authorize".
5. Now you are ready to test any of the other urls.

## Run in Postman

To run in Postman:

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/1419781-cfc2a7ac-8b84-4249-81a2-db60ee55c883?action=collection%2Ffork&collection-url=entityId%3D1419781-cfc2a7ac-8b84-4249-81a2-db60ee55c883%26entityType%3Dcollection%26workspaceId%3D68042aba-edc1-489a-82d0-700eeb2f4097#?env%5BClarityAPI%20Public%5D=W3sia2V5IjoiY2xpZW50X2lkIiwidmFsdWUiOiJbZW50ZXIgY2xpZW50X2lkIHByb3ZpZGVkXSIsImVuYWJsZWQiOnRydWUsInR5cGUiOiJkZWZhdWx0In0seyJrZXkiOiJjbGllbnRfcGFzc3dvcmQiLCJ2YWx1ZSI6IltlbnRlciBwYXNzd29yZCBwcm92aWRlZF0iLCJlbmFibGVkIjp0cnVlLCJ0eXBlIjoiZGVmYXVsdCJ9LHsia2V5IjoidG9rZW4iLCJ2YWx1ZSI6IltlbnRlciB0b2tlbiByZXR1cm5lZCBpbiB0aGUgXCJ0b2tlblwiIHJlcXVlc3RdIiwiZW5hYmxlZCI6dHJ1ZSwidHlwZSI6ImRlZmF1bHQifSx7ImtleSI6InByb2plY3RpZCIsInZhbHVlIjoiW2VudGVyIGEgcHJvamVjdCB1dWlkIGZyb20gdGhlIFwicHJvamVjdFwiIHJlcXVlc3RdIiwiZW5hYmxlZCI6dHJ1ZSwidHlwZSI6ImRlZmF1bHQifSx7ImtleSI6Im1hbmhvbGVfaWQiLCJ2YWx1ZSI6IltlbnRlciBhIG1hbmhvbGUgdXVpZCBmcm9tIHRoZSBcImluc3BlY3Rpb24gbGlzdCAoU3RydWN0dXJlSW5zcGVjdGlvbilcIiByZXF1ZXN0XSIsImVuYWJsZWQiOnRydWUsInR5cGUiOiJkZWZhdWx0In0seyJrZXkiOiJiYXNlX3VybCIsInZhbHVlIjoiaHR0cHM6Ly9yam4tY2xhcml0eS1hcGkuY29tL3YxL2NsYXJpdHkiLCJlbmFibGVkIjp0cnVlLCJ0eXBlIjoiZGVmYXVsdCJ9XQ==)

1. Follow the [link](https://app.getpostman.com/run-collection/1419781-cfc2a7ac-8b84-4249-81a2-db60ee55c883?action=collection%2Ffork&collection-url=entityId%3D1419781-cfc2a7ac-8b84-4249-81a2-db60ee55c883%26entityType%3Dcollection%26workspaceId%3D68042aba-edc1-489a-82d0-700eeb2f4097#?env%5BClarityAPI%20Public%5D=W3sia2V5IjoiY2xpZW50X2lkIiwidmFsdWUiOiJbZW50ZXIgY2xpZW50X2lkIHByb3ZpZGVkXSIsImVuYWJsZWQiOnRydWUsInR5cGUiOiJkZWZhdWx0In0seyJrZXkiOiJjbGllbnRfcGFzc3dvcmQiLCJ2YWx1ZSI6IltlbnRlciBwYXNzd29yZCBwcm92aWRlZF0iLCJlbmFibGVkIjp0cnVlLCJ0eXBlIjoiZGVmYXVsdCJ9LHsia2V5IjoidG9rZW4iLCJ2YWx1ZSI6IltlbnRlciB0b2tlbiByZXR1cm5lZCBpbiB0aGUgXCJ0b2tlblwiIHJlcXVlc3RdIiwiZW5hYmxlZCI6dHJ1ZSwidHlwZSI6ImRlZmF1bHQifSx7ImtleSI6InByb2plY3RpZCIsInZhbHVlIjoiW2VudGVyIGEgcHJvamVjdCB1dWlkIGZyb20gdGhlIFwicHJvamVjdFwiIHJlcXVlc3RdIiwiZW5hYmxlZCI6dHJ1ZSwidHlwZSI6ImRlZmF1bHQifSx7ImtleSI6Im1hbmhvbGVfaWQiLCJ2YWx1ZSI6IltlbnRlciBhIG1hbmhvbGUgdXVpZCBmcm9tIHRoZSBcImluc3BlY3Rpb24gbGlzdCAoU3RydWN0dXJlSW5zcGVjdGlvbilcIiByZXF1ZXN0XSIsImVuYWJsZWQiOnRydWUsInR5cGUiOiJkZWZhdWx0In0seyJrZXkiOiJiYXNlX3VybCIsInZhbHVlIjoiaHR0cHM6Ly9yam4tY2xhcml0eS1hcGkuY29tL3YxL2NsYXJpdHkiLCJlbmFibGVkIjp0cnVlLCJ0eXBlIjoiZGVmYXVsdCJ9XQ==) and fork the Postman setup.
2. Go to the Environments tab and enter in the "client_id" and "client_password" environment variables.
3. Go to the Collections tab and run the "token" request. Copy the "token" value from the body of the response.
4. Return to the Environments tab and past the token into the "token" variable.
5. Return to the Collections tab and run the "projects" request. Select a project_id from one of the projects in the list and copy it.
6. In the Environments tab, paste the project id into the "project_id" variable.
7. In the Collections tab, run the "recordtypes" request. These represent units of data collection and storage such as "StructureInspection" (Manhole inspections), "StructurePipeInspection" (Pipes connected to the inspected manholes), "SmokeObservation" (Smoke observations and defects), "FlowMonitor" (Flow meter site information), and much more. Substitute those into the {record_type} path parameter (see the Swagger documentation) to get data of various types.

## Easy .NET Integration 

The "Integration .NET" folder contains an assembly that can be easily referenced into a .NET project. This allows implementation of the API with easy to use, high-level functions.

### Initialize the API (Example in C#)
```csharp
var api = new Clarity.Api("[Your client_id]", "[Your password]");
```

### Get a list of projects
```csharp
var projects = api.GetProjects();
```

### Get a list of sites for one of the projects
```csharp
var MyProject = projects.Single((p) => p.projectnumber == "[My Project Number like XX-XXXX-XX]");
var sites = MyProject.GetMonitorSites(api);
```

## Sample Projects

### ArcGIS JS Web Map - Smoke Observation
A single page map and data table that demonstrate how to use the geojeson path to show smoke observations. It includes examples of how to configure additional attributes to the features and pull photos for the popup templates.

### ArcGIS JS Web Map - Manhole Surface Type
Similar to the smoke observations map, it is a single page map and data table that demonstrate how to use the geojeson path to show manhole inspections. It includes examples of how to configure additional attributes to the features and pull photos for the popup templates.

### C#.NET Data Explorer (Sample Clarity API App)
Run the sample app, enter your credentials, and then click any of the buttons on the side or in the results tables to explore the data. As you pull data and drill down, you will see a mockup of the function calls and a list of http URIs to help you understand how to implement the API.

## Basic Concepts

### Authentication
Authentication is token based. Before any API route can be called a JWT (JSON Web Token) must be generated. The token can be reused until it expires. Tokens have a limited lifetime of 24 hours. Once it expires (or before that time) a new token should be generated. 

Tokens may be submitted in two ways:
1. An Authorization: Bearer [token] header.
2. A token query parameter (i.e. ?token=[token]).

### Record Types
Record Types represent a single unit of data. Most inspection types must utilize two or more Record Types to facilitate one-to-many relationships. 

So, for example, a standard MACP Manhole Inspection has the StructureInspection, StructurePipeInspection, and StructureDefects Record Types, which correspond to the inspections, connections, and conditions data tables in the MACP database.

Clarity's Record Types are dynamic. That means that every project will automatically contain every record type that is in use. It also means that the API is future proof. New inspection types can be registered by RJN as new services are offered and will seemlessly become available without having to update the API version or schema.

To use the API affectively, one needs to know the Record Type names available on a given project. Those can be accessed using the /clarity/projects/{projectid}/recordtypes path.

### Attributes
Attributes are the details of a "Data Type" (see above) or a record. Each question on a field form is filling out a single Attribute. They are essentially key / value pairs.

Attributes are dynamic. This enables RJN to customize data collection to the clients' needs without having to update the API version or schema.

When querying a single record, by default, all Attributes will be returned as a dictionary type object. When using paths that return all records (the /list or /geojson paths), the attributes are not included by default. However, the attribute_list query parameter can be set to include a comma-delimited list of Attributes that will dynamically extend the schema of the output.  

To use the attribute_list extension feature, one must know what attributes are available on a given project and Data Type. That can be queried using the path, /clarity/projects/{projectid}/{datatype}/attributes.

### GeoJSON
The GeoJSON path (/clarity/{datatype}/geojson) facilitates easy integration with the ArcGIS JavaScript API or any other mapping application that accepts the standard GeoJson format. See the samples directory in this repository for some simple implementations to get you started.

### Flow Monitoring Data
Flow monitoring data is obtained using the /clarity/projects/{projectid}/entities/{entityid} route.

The typical work flow for drilling down to the time series data is this:
1. Get a list of the projects: GET /clarity/projects
2. Get a list of sites using one of the project guids: GET /clarity/projects/{projectid}/sites. To get all sites, loop through the project list and get the sites for each.
3. Get a list of entities. Here you have two options. a) GET /clarity/projects/{projectid}/sites/{siteid}/entities, which gets all entities for a site where the siteid is the site guid or b) GET /clarity/projects/{projectid}/entities/{entityname}, which gets all entities accross all sites that match the entity name provided. Note that the entity name should be URL encoded, so "Final Flow" would be "Final+Flow" in the URL query parameter.
4. Now the data may be queried using the "id" of the entity: GET /clarity/projects/{projectid}/entities/{entityid}/data.  The id will be an integer value usually preceded by an "m" or "s", but not always and will be unique within a given project.

The project, site and entity metadata should be stored locally in the user's database or other storage location. Periodically, those datasets should be refreshed to update based on changes in the source. They shouldn't change too often, so caching them will provide a more streamlined and efficient implementation on the user's side and prevent undue burden on the API. The entity data, however, is more "real-time" and should be pulled more frequently using the date filters provided. 

