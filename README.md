# 🏬 Project JwtStore
- This project was designed to practice concepts related to ASP.NET Authentication and Authorization using JwtBearer.

## 💻 User Creation and Authentication Mechanism
- Through this API, it is possible to create a user by providing a name, email, and password. To avoid storing the password in the database, before sending the information, the password is transformed into a hash, making it impossible to revert to the original password and, therefore, safer to store. When a user attempts to log in, this password hashing process occurs again with the password entered by the user. Only then is the generated hash compared to the stored hash.

- For authorization and authentication, JwtBearer was used, allowing us to utilize claims and policies based on the user's roles to enforce authentication requirements
## ⚙️ Technology
- .NET / ASP.NETG
- Entity Framework
- SqlServer
- JwtBearer
- Flunt (Fluent Validation)
- MediatR
 
## 📌 Endpoints
- This project has three endpoints that are used to create a user, autheticate a user and an endpoint to test users that require a certain role.

	1. Method : <b>POST</b>		
		- This endpoint creates a new user.
		
		```
		https://localhost:7225/api/v1/users
		```
		```
		{
			"name" : "Usuario",
			"email": "usuario@balta.io",
			"password": "dmjodfFSDNIUDFU987y597345y73"
		}
		```

	2. Method : <b>POST</b>	
		- This endpoint authenticate a user.
		```
		https://localhost:7225/api/v1/authenticate
		```
		```
		{
			"email": "usuario@balta.io",
			"password": "dmjodfFSDNIUDFU987y597345y73"
		}
		```

	3. Method : <b>POST</b>	
		- This endpoint can be used to test an authenticated user who has the role of 'admin'.
		```
		https://localhost:7225/api/v1/test
		```
		- To perform testing on this endpoint, you must first authenticate a user with an 'admin' role. After successful authentication, you will receive an authentication token. To access this endpoint in Postman, navigate to the 'Authorization' tab and select 'Bearer Token' as the authentication type. Then, input the received token into the appropriate field. This ensures that only users with administrative privileges can access and test the functionality of this particular endpoint.

