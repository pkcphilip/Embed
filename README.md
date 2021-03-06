# Overview

This application aims to provide RESTful API endpoints for user to get, add and update product to the system via GET and PUT API calls. 
Token based authentication (OAuth) is implemented in the project so only authenticated users can perform the API calls. 


## Assumptions

These are some assumptions that I have made based on my understanding of the question:
1. Only GET and PUT endpoints are required.
2. PUT will be used for create instead of POST due to above requirement.
3. The example formats given are the expected response format for respective GET and PUT request.
4. The response from the GET request will display only product id based on the format.
5. It is a requirement to write a GET endpoint to allow user to get all products by a list of product ids in a single request. I have enabled GET request to accept comma delimited string of ids and return the product if found in the system.
6. It is a requirement to write a PUT endpoint to allow user to add or update product in a single request. If user pass an array of product and there are a mix of product with/without id, system will:
   - Create new product if there is no product id.
   - Update product if there is product id and product exist in the system.
   - Return Bad request if the id provided does not exists.
7. Product Id in the request body is an optional field and the rest of the data are mandatory.
8. Id (Unique Identifier) is based on the request's correlation Id in the response format.
9. Timestamp is in UTC format.
10. System to accept JSON body in snake case format.

## Project Structure

| Project Name  | Description |
| ------------- | ------------- |
| Embed.Core | Class Library. To store domain entities and logic.  |
| Embed.Persistance  | Class Library. Set up as persistance layer for ORM implementation and repositories. |
| Embed.Web  | Main startup Web API project. This will contain the logic for the API endpoint to clients.  |
| Embed.UnitTests  | Unit Test project for all relevant tests.  |

## Prerequisites

1. Visual Studio 2017
2. SQL Server 2012 Express LocalDB or later version (to run locally)

## Installing

1. Pull the Git repo and open it in VS. 
2. Build the solution.
3. Open the Package Manager Console (Tools > NuGet Package Manager > Package Manager Console).
4. Set default project as **Embed.Persistance** in the console.
5. In the Package Manager Console window, enter the following command: `Update-Database -StartupProjectName Embed.Web`
6. Right click on **Embed.Web** project and "Set as Startup Project".
7. Run the application.

## Instructions

As authentication is required, these steps are necessary to get the authentication token.

1. Register as an user in the system.
2. Generate an authentication token.
3. Pass the authentication token as Authorization value in the subsequent web request header to the endpoints.

## Built With

- .Net Framework
- Web API 2
- AutoMapper
- Entity Framework
- Ninject
- ASP.Net.Identity and OWIN
- MSTest
- Moq
- FluentAssertion


## Request & Response Examples

- [Register User](#register-user)
- [Get Authentication Token](#get-authentication-token)
- [Get All Products](#get-all-products)
- [Get Single Product By Id](#get-single-product-by-id)
- [Get Multiple Products By Multiple Product Ids](#get-multiple-products-by-multiple-product-ids)
- [Create Single Product](#create-single-product)
- [Update Single Product](#update-single-product)
- [Create and Update Multiple Products](#create-and-update-multiple-products)

### Register User

#### POST api/account/register

Example: ```http://localhost:61982/api/account/register```

Header:
```
Content-Type: application/json
```

Request body:
```
{
  "user_name": "Taiseer11",
  "password": "SuperPass",
  "confirm_password": "SuperPass"
}
```
Return Http Status 200 (OK).

### Get Authentication Token

#### GET	/token

Example: ```http://localhost:61982/token```

Header:

```
Accept:application/json
Content-Type:application/x-www-form-urlencoded
```

Sample Request body:

```
grant_type:password
username:Taiseer11
password:SuperPass
```

Response body:

```
{
    "access_token": "1OgrtXrttjF15-gxflf9YGH9r2U344TV...",
    "token_type": "bearer",
    "expires_in": 86399
}
```


### Get All Products

#### GET	/api/products

Example: ```http://localhost:61982/Api/Products```

Header:

```
Accept: application/json
Content-Type: application/json
Authorization: Bearer ozsFlYeQWOSTPzzt_yEkMq...
```

Response body:
```
{
    "id": "8000008d-0000-d500-b63f-84710c7967bb",
    "timestamp": "2018-12-11T05:04:27.819792Z",
    "products": [
        {
            "id": 1
        },
        {
            "id": 2
        },
        {
            "id": 3
        },
        {
            "id": 4
        }
    ]
}
```


### Get Single Product By Id

#### GET	/api/products/[id]

Example: ```http://localhost:61982/Api/Products/1```

Response body:

```
{
    "id": "8000008d-0000-d500-b63f-84710c7967bb",
    "timestamp": "2018-12-11T05:04:27.819792Z",
    "products": [
        {
            "id": 1
        }
    ]
}
```


### Get Multiple Products By Multiple Product Ids

#### GET	/api/products/[ids]

Example: ```http://localhost:61982/Api/Products/1,2,3```


Response body:

```
{
    "id": "8000008d-0000-d500-b63f-84710c7967bb",
    "timestamp": "2018-12-11T05:04:27.819792Z",
    "products": [
        {
            "id": 1
        },
        {
            "id": 2
        },
        {
            "id": 3
        }
    ]
}
```


### Create Single Product

#### PUT	/api/products

Example: ```http://localhost:61982/Api/Products```

Request Body:

```
{
    "name": "Arcade Machine",
    "quantity": 30,
    "sale_amount": 434.35
}
```

Response body:

```
{
    "id": "8000067d-0002-f100-b63f-84710c7967bb",
    "timestamp": "2018-12-11T03:06:29.8040612Z",
    "products": [
        {
            "id": 1,
            "name": "Arcade Machine",
            "quantity": 30,
            "sale_amount": 434.35
        }
    ]
}
```



### Update Single Product

#### PUT	/api/products/[id]

Example: ```http://localhost:61982/Api/Products/1```

Request body:

```
{
    "id": 1,
    "name": "Arcade Machine",
    "quantity": 50,
    "sale_amount": 500.35
}

```

Response body:

```
{
    "id": "8000067d-0002-f100-b63f-84710c7967bb",
    "timestamp": "2018-12-11T03:06:29.8040612Z",
    "products": [
        {
            "id": 1,
            "name": "Arcade Machine",
            "quantity": 50,
            "sale_amount": 500.35
        }
    ]
}
```



### Create and Update Multiple Products

#### PUT	/api/products/all

Example: ```http://localhost:61982/Api/Products/all```

Request body:

```
[
	{
		"id": 1,
		"name": "Arcade Machine",
		"quantity": 25,
		"sale_amount": 550.50
	},
	{
		"name": "Pinball Machine",
		"quantity": 50,
		"sale_amount": 600.00
	},
	{
		"name": "Basketball Machine",
		"quantity": 10,
		"sale_amount": 750.50
	}
]

```

Response body:

```
{
    "id": "8000008f-0000-d500-b63f-84710c7967bb",
    "timestamp": "2018-12-11T05:06:41.6841771Z",
    "products": [
        {
            "id": 1,
            "name": "Arcade Machine",
            "quantity": 25,
            "sale_amount": 550.5
        },
        {
            "id": 12,
            "name": "Pinball Machine",
            "quantity": 50,
            "sale_amount": 600
        },
        {
            "id": 13,
            "name": "Basketball Machine",
            "quantity": 10,
            "sale_amount": 750.5
        }
    ]
}
```


## Acknowledgement

- http://bitoftech.net/2014/06/01/token-based-authentication-asp-net-web-api-2-owin-asp-net-identity/
- http://www.vannevel.net/2015/03/21/how-to-unit-test-your-owin-configured-oauth2-implementation/
- https://stackoverflow.com/questions/978061/http-get-with-request-body
- https://www.toptal.com/asp-dot-net/asp-net-web-api-tutorial
- https://www.pluralsight.com/guides/asp-net-mvc-creating-solutions-with-separate-projects-for-entities-data-access-and-website-functionality
- https://blog.rapid7.com/2016/12/23/the-value-of-correlation-ids/

