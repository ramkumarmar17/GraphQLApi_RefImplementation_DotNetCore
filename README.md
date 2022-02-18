# GraphQL API Reference Implementation in .NET Core
A reference implementation of GraphQL API in .NET Core

## GraphQL Overview
GraphQL is a open specification for flexible and queryable APIs. It is a server-side runtime for executing queries via APIs. With GraphQL APIs, clients have the power to ask exactly what they need; avoid over-fetching and under-fetching.

GraphQL APIs can be used to perform the following operations:
-	Fetch information from database 
-	Fetch information from 3rd party systems and existing or legacy services
-	Hybrid / Aggregation; Fetch information from varied data sources 

## GraphQL Concepts
### Schema & Types
-	Defines the contract between client and server
-	Represents all objects (models) returned by the API; specified using Schema Definition Language
-	Allows frontend and backend teams to work independently, using the schema definition

### Resolver Functions
-	Responsible for fetching data for each object (model) and attribute (field) in the GraphQL query
-	Each Type/model can map to exactly one resolver function
-	Resolver functions are executed in parallel

### Query
Used to request specific objects (models), and specific attributes (fields) under each object.

### Mutation
Used to add, update or delete objects. Can specify return attributes, as in a query.

### Subscription
Used to get notifications when objects are created, modified or deleted.

## GraphQL API Development
A typical GraphQL API development involves the following steps, in the below sequence:
-	Identify Types (models) & Fields (attributes)
-	Define the schema (SDL): All supported types & operations
-	Add resolver functions for all the Types
-	Integrate with required data sources: Web APIs, 3rd party services, DB (EF, ADO.Net, etc)

## Use-case for this Reference Implementation
A financial services company offers both Web App and Mobile App for its customers. Both apps have a dashboard (landing page) where basic info about the customer and their accounts & cards are displayed. The company has setup microservices for Customers, Cards, etc. The dashboard page makes multiple WebAPI requests to different microservices to get customer/account/card info. This results in too much network traffic/calls and over-fetching of data since only a subset of the fetched info is displayed in the dashboard / landing page. 

GraphQL APIs are a right fit for this kind of scenario.  A GraphQL API endpoint can be setup that will fetch customer, card and account info in a single call, using different Resolver functions for each type/object. The Web App, which has more screen real-estate, will request many specific fields, while the Mobile App with limited screen space will request only basic/necessary fields. The number of network calls and the amount of data fetched/transferred is reduced. 

This is also an example of **Aggregation service** pattern – a service that’s used to aggregate data from multiple data sources (WebAPIs, 3rd party services, DB, Static content, etc).

This GraphQL API reference implementation addresses the use-case mentioned above and can be used as a template to have a head-start with GraphQL.


## High-level view 
![GraphQLApi_ReferenceImplementation_DotNetCore](https://user-images.githubusercontent.com/46394226/153345744-f1840c7f-3dfa-457b-89e3-fa7586688cf6.PNG)

## Prerequisites
- .NET Core 3.1 Runtime & SDK

## Solution contents
- Card.Api
  - A sample REST API for Cards
- Customer.Api
  - A sample REST API for Customers
- Dashboard
  - GraphQL API implementation that fetches Card & Customer info from the above 2 sample APIs.
  - Also fetches Account info from local repository
  - Has GraphQL Schema definition, GraphQL types and Resolvers to fetch Card, Customer & Account info.
  - Has implementation for a GraphQL Query and a GraphQL Mutation
  - Processes GraphQL Queries using 2 endpoints:
    - http://localhost:7000/graphql - Queries processed by GraphQL middleware
    - http://localhost:7000/api/dashboard/graphql - Queries processed by custom implementation, as a controller method/endpoint, in DashboardController

## Execution instructions
- Clone the repository
- Open Command Prompt, Switch to solution folder and Build solution using dotnet 'build' command:
  - dotnet build
  - Alternatively, Open solution in VS and Build solution.
- Project outputs (executables) are created in \output\netcoreapp3.1 folder under the solution folder
- From Command Prompt, Switch to output folder mentioned above
- Launch CardApi and CustomerApi on ports 5000 and 6000 respectively:
  - Card.Api.exe --urls http://localhost:5000
  - Customer.Api.exe --urls http://localhost:6000
- Verify CardApi and CustomerApi endpoints
  - curl http://localhost:5000/api/cards
  - curl http://localhost:6000/api/customers
- If curl is not installed on your system then check the above urls from browser / postman / etc
- Launch Dashboard on port 7000:
  - Dashboard.exe --urls http://localhost:7000
- From browser, access the GraphQL UI Playground to run GraphQL queries and mutations
  - http://localhost:7000/ui/playground

### Sample query
```
query {
  customers{
    id
    fullName
    address {
      street
      city
    }
  }
  cards {
      id
      validFrom
      validTo
  }
  customer(customerId: "110011") {
      id
      fullName
      dateOfBirth
  }
  accounts(customerId: "110011") {
    accountNumber
    balance
    type
  }
}
```

### Sample mutation
```
mutation AccountMutation ($account:AccountInput!) {
  createAccount(account: $account) {
    customerId
    accountNumber
  }
}

Variables:
{ "account": {
      "customerId": "110011",
      "accountNumber": "777777777",
      "type": "savings",
      "balance": "1000"
    }
}
```
