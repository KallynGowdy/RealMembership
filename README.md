# Real Membership

A library to provide extensible membership capabilities.

### Features

- Login verification
- Account Auditing
- Safe and secure password storage
- Completely replaceable components (including Dates, validation, ect.)
- Storage agnostic
- Full Async Support
- Two factor support
- Multi-Tenancy

### Design

The design is made to be simple, the objects represent themselves, so a login provides all of the features within itself to validate provided credentials. Services just bind all of these properties together and present them in an organized way. 

For example, to change the password of a `IPasswordLogin`, you just call the `SetPassword(string)` method on that login. (Simple, encapsulated, objects controlling themselves) The top level object that you should interact with is the `IUserService`, which just binds to a `ILoginRepository` and provides centralized access to authentication and user account manipulation. The primary goal of the `IUserService` is to provide sensible access to logins and user accounts while preserving result information. `IUserService` does not just return a `bool` when you call one of the `Authenticate` methods, instead it returns a new `AuthenticationResult` that represents the exact result of the operation, whether it was successful, and how it should be handled from now on. No exceptions are thrown, you know exactly how to handle the result and you get to choose how you want to display the result to the user.