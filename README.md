# MentorLink Authentication System

This project now includes a complete authentication system with JWT tokens, password hashing, and secure API endpoints.

## Features

- ✅ **Sign Up API**: Register new trainees with validation (email-based)
- ✅ **Sign In API**: Login with email and password
- ✅ **Password Hashing**: Secure SHA256 password hashing
- ✅ **JWT Authentication**: Token-based authentication
- ✅ **Refresh Tokens**: Automatic token refresh mechanism
- ✅ **Database Integration**: User data stored in SQL Server
- ✅ **Error Handling**: Proper error messages for invalid input, incorrect credentials, or existing email
- ✅ **Protected Endpoints**: Secure API routes
- ✅ **Input Validation**: Data validation and sanitization

## API Endpoints

### Authentication Endpoints

#### 1. Sign Up
```
POST /api/auth/signup
```

**Request Body:**
```json
{
  "name": "John Doe",
  "email": "john@example.com",
  "password": "SecurePass123!",
  "confirmPassword": "SecurePass123!",
  "phone": "12345678901"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Registration successful",
  "token": {
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "refreshToken": "refresh_token_here",
    "expiresAt": "2024-01-01T12:00:00Z",
    "tokenType": "Bearer"
  },
  "user": {
    "traineeId": 1,
    "name": "John Doe",
    "email": "john@example.com",
    "phone": "12345678901",
    "level": "Beginner",
    "pictureUrl": null
  }
}
```

#### 2. Sign In
```
POST /api/auth/signin
```

**Request Body:**
```json
{
  "email": "john@example.com",
  "password": "SecurePass123!"
}
```

**Response:** Same as Sign Up response

#### 3. Refresh Token
```
POST /api/auth/refresh-token
```

**Request Body:**
```json
{
  "refreshToken": "refresh_token_here"
}
```

#### 4. Logout
```
POST /api/auth/logout
```

**Request Body:**
```json
{
  "refreshToken": "refresh_token_here"
}
```

### Protected Endpoints

#### 1. Get User Profile
```
GET /api/protected/profile
Authorization: Bearer {accessToken}
```

#### 2. Test Protected Endpoint
```
GET /api/protected/test
Authorization: Bearer {accessToken}
```

## Password Requirements

- Minimum 8 characters
- Maximum 100 characters
- Must contain at least one uppercase letter
- Must contain at least one lowercase letter
- Must contain at least one number
- Must contain at least one special character (@$!%*?&)

## Phone Requirements

- Exactly 11 digits
- Numbers only

## JWT Configuration

The JWT configuration is stored in `appsettings.json`:

```json
{
  "Jwt": {
    "Secret": "YourSuperSecretKeyHere123456789012345678901234567890123456789012345678901234567890",
    "Issuer": "MentorLink",
    "Audience": "MentorLinkUsers",
    "ExpiryInHours": 1
  }
}
```

## Database Schema

The `Trainee` table has been updated with authentication fields:

- `TraineeId` (Primary Key)
- `Name` (Required, max 50 chars)
- `Email` (Required, max 100 chars, unique, email format)
- `PasswordHash` (Required, max 255 chars, SHA256 hashed)
- `Phone` (Required, exactly 11 digits)
- `PictureUrl` (Optional, max 200 chars)
- `Level` (Enum: Beginner, Intermediate, Advanced)
- `IsUpdated` (Boolean)
- `IsSubscribed` (Boolean)
- `SessionId` (Optional, max 100 chars)
- `RefreshToken` (Optional, max 500 chars)
- `RefreshTokenExpiryTime` (Optional, DateTime)
- `CreatedAt` (DateTime, auto-set)
- `LastLoginAt` (Optional, DateTime)

## Security Features

1. **Password Hashing**: Passwords are hashed using SHA256 before storage
2. **JWT Tokens**: Secure token-based authentication
3. **Refresh Tokens**: Automatic token refresh mechanism
4. **Input Validation**: Comprehensive validation on all inputs
5. **Error Handling**: Secure error messages that don't leak sensitive information
6. **CORS**: Configured for cross-origin requests
7. **Token Expiry**: Access tokens expire after 1 hour
8. **Refresh Token Expiry**: Refresh tokens expire after 7 days

## Usage Examples

### Using the API with JavaScript/Fetch

```javascript
// Sign Up
const signUpResponse = await fetch('/api/auth/signup', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify({
    name: 'John Doe',
    email: 'john@example.com',
    password: 'SecurePass123!',
    confirmPassword: 'SecurePass123!',
    phone: '12345678901'
  })
});

const signUpData = await signUpResponse.json();
const accessToken = signUpData.token.accessToken;

// Access Protected Endpoint
const profileResponse = await fetch('/api/protected/profile', {
  headers: {
    'Authorization': `Bearer ${accessToken}`
  }
});

const profileData = await profileResponse.json();
```

### Using the API with Postman

1. **Sign Up**: POST to `{{baseUrl}}/api/auth/signup`
2. **Sign In**: POST to `{{baseUrl}}/api/auth/signin`
3. **Protected Endpoints**: Add `Authorization: Bearer {{accessToken}}` header

## Error Responses

### Validation Errors
```json
{
  "success": false,
  "message": "Invalid input data"
}
```

### Authentication Errors
```json
{
  "success": false,
  "message": "Invalid email or password"
}
```

### Duplicate Email
```json
{
  "success": false,
  "message": "Email is already registered"
}
```

## Running the Project

1. **Build the project:**
   ```bash
   dotnet build
   ```

2. **Update the database:**
   ```bash
   dotnet ef database update --project Presistence
   ```

3. **Run the application:**
   ```bash
   dotnet run
   ```

4. **Access Swagger UI:**
   ```
   https://localhost:7001/swagger
   ```

## Testing the Authentication

1. Use the Sign Up endpoint to create a new user
2. Use the Sign In endpoint to get an access token
3. Use the access token in the Authorization header to access protected endpoints
4. Use the Refresh Token endpoint to get a new access token when it expires

## Project Structure

```
MentorLink/
├── DomainLayer/
│   ├── Models/
│   │   └── Trainee.cs (Updated with auth fields, no username)
│   └── Contracts/
│       ├── IGenericRepository.cs
│       ├── ITraineeRepository.cs (Updated)
│       └── IUnitOfWork.cs (Updated)
├── Presistence/
│   ├── Data/
│   │   └── AppDbContext.cs
│   └── Repositories/
│       ├── GenericRepository.cs
│       ├── TraineeRepository.cs (Updated)
│       └── UnitOfWork.cs (Updated)
├── Service/
│   ├── AuthService.cs (Updated for email-only auth)
│   └── JwtService.cs (Updated)
├── Shared/
│   └── DataTransferObjects/
│       ├── AuthDto.cs (Updated, no username)
│       └── TraineeDto.cs (Updated, no username)
└── MentorLink/
    ├── Controllers/
    │   ├── AuthController.cs
    │   └── ProtectedController.cs (Updated)
    ├── Program.cs (Updated with JWT config)
    └── appsettings.json (Updated with JWT settings)
```

## Security Notes

- Change the JWT secret in production
- Use HTTPS in production
- Consider implementing rate limiting
- Consider implementing account lockout after failed attempts
- Consider implementing email verification
- Consider implementing password reset functionality

## Key Changes Made

- ✅ **Removed Username field** - Users now authenticate with email only
- ✅ **Simplified Sign Up** - Only requires: Name, Email, Password, ConfirmPassword, Phone
- ✅ **Simplified Sign In** - Only requires: Email, Password
- ✅ **Updated JWT Claims** - Email serves as the name identifier
- ✅ **Database Migration** - Username column removed from Trainees table

The authentication system is now fully implemented with simplified email-based authentication and ready for use!
