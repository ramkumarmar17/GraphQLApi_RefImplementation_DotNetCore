# GraphQL API Reference Implementation in .NET Core
A reference implementation of GraphQL API in .NET Core

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
