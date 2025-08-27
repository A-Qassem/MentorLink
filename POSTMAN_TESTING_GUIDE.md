# Testing MentorLink Authentication in Postman

## 🚀 Quick Start Guide

### Step 1: Set Up Environment Variables
1. In Postman, create a new collection called "MentorLink Authentication API"
2. Go to the collection's "Variables" tab
3. Set the following variables:
   - `baseUrl`: `https://localhost:7001`
   - `accessToken`: (leave empty for now)
   - `refreshToken`: (leave empty for now)

## 📋 Testing Flow

### 1. **Sign Up** (Create New User)
- **Method**: POST
- **URL**: `{{baseUrl}}/api/auth/signup`
- **Headers**: 
  ```
  Content-Type: application/json
  ```
- **Body** (raw JSON):
  ```json
  {
    "name": "John Doe",
    "email": "john@example.com",
    "password": "SecurePass123!",
    "confirmPassword": "SecurePass123!",
    "phone": "12345678901"
  }
  ```

**Expected Response** (200 OK):
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

**After Sign Up Success:**
1. Copy the `accessToken` from the response
2. Copy the `refreshToken` from the response
3. Update the collection variables:
   - Set `accessToken` = the token value
   - Set `refreshToken` = the refresh token value

### 2. **Sign In** (Login Existing User)
- **Method**: POST
- **URL**: `{{baseUrl}}/api/auth/signin`
- **Headers**: 
  ```
  Content-Type: application/json
  ```
- **Body** (raw JSON):
  ```json
  {
    "email": "john@example.com",
    "password": "SecurePass123!"
  }
  ```

**Response:** Same as Sign Up response

### 3. **Test Protected Endpoints**

#### A. Get User Profile
- **Method**: GET
- **URL**: `{{baseUrl}}/api/protected/profile`
- **Headers**: 
  ```
  Authorization: Bearer {{accessToken}}
  ```

**Expected Response** (200 OK):
```json
{
  "message": "This is a protected endpoint",
  "user": {
    "userId": "1",
    "email": "john@example.com",
    "name": "John Doe",
    "phone": "12345678901",
    "level": "Beginner"
  }
}
```

#### B. Test Protected Endpoint
- **Method**: GET
- **URL**: `{{baseUrl}}/api/protected/test`
- **Headers**: 
  ```
  Authorization: Bearer {{accessToken}}
  ```

**Expected Response** (200 OK):
```json
{
  "message": "Authentication is working! You have access to this protected endpoint."
}
```

### 4. **Refresh Token** (Get New Access Token)
- **Method**: POST
- **URL**: `{{baseUrl}}/api/auth/refresh-token`
- **Headers**: 
  ```
  Content-Type: application/json
  ```
- **Body** (raw JSON):
  ```json
  {
    "refreshToken": "{{refreshToken}}"
  }
  ```

**Expected Response** (200 OK):
```json
{
  "success": true,
  "message": "Token refreshed successfully",
  "token": {
    "accessToken": "new_access_token_here",
    "refreshToken": "new_refresh_token_here",
    "expiresAt": "2024-01-01T12:00:00Z",
    "tokenType": "Bearer"
  }
}
```

### 5. **Logout** (Revoke Token)
- **Method**: POST
- **URL**: `{{baseUrl}}/api/auth/logout`
- **Headers**: 
  ```
  Content-Type: application/json
  ```
- **Body** (raw JSON):
  ```json
  {
    "refreshToken": "{{refreshToken}}"
  }
  ```

**Expected Response** (200 OK):
```json
{
  "success": true,
  "message": "Logged out successfully"
}
```

## 🔍 Testing Different Scenarios

### Test Invalid Credentials
**Sign In with Wrong Password:**
```json
{
  "email": "john@example.com",
  "password": "WrongPassword123!"
}
```
**Expected Response** (401 Unauthorized):
```json
{
  "success": false,
  "message": "Invalid email or password"
}
```

### Test Duplicate Registration
**Try to Sign Up with Same Email:**
```json
{
  "name": "Jane Doe",
  "email": "john@example.com",
  "password": "SecurePass123!",
  "confirmPassword": "SecurePass123!",
  "phone": "12345678902"
}
```
**Expected Response** (400 Bad Request):
```json
{
  "success": false,
  "message": "Email is already registered"
}
```

### Test Protected Endpoint Without Token
**Get Profile without Authorization header:**
- Remove the `Authorization` header
- **Expected Response** (401 Unauthorized)

### Test Invalid Token
**Get Profile with Invalid Token:**
- Set `Authorization: Bearer invalid_token_here`
- **Expected Response** (401 Unauthorized)

## 📝 Validation Testing

### Test Password Requirements
**Weak Password:**
```json
{
  "name": "Test User",
  "email": "test@example.com",
  "password": "weak",
  "confirmPassword": "weak",
  "phone": "12345678901"
}
```
**Expected Response** (400 Bad Request):
```json
{
  "success": false,
  "message": "Invalid input data"
}
```

### Test Phone Requirements
**Invalid Phone:**
```json
{
  "name": "Test User",
  "email": "test@example.com",
  "password": "SecurePass123!",
  "confirmPassword": "SecurePass123!",
  "phone": "123456789"
}
```

## 🛠️ Troubleshooting

### Common Issues:

1. **SSL Certificate Error**
   - In Postman, go to Settings → General
   - Turn OFF "SSL certificate verification"

2. **Connection Refused**
   - Make sure the application is running: `dotnet run`
   - Check the URL: `https://localhost:7001`

3. **Token Not Working**
   - Make sure you copied the full token (including the `eyJ...` part)
   - Check that the token hasn't expired (1 hour lifetime)
   - Use the Refresh Token endpoint to get a new token

4. **Database Connection Issues**
   - Make sure the database is accessible
   - Check the connection string in `appsettings.json`

## 🎯 Best Practices

1. **Always test the complete flow**: Sign Up → Sign In → Protected Endpoints → Refresh Token → Logout

2. **Test error scenarios**: Invalid credentials, duplicate emails, validation errors

3. **Keep tokens secure**: Don't share tokens in screenshots or logs

4. **Use environment variables**: Update the collection variables after each successful authentication

5. **Test token expiration**: Wait for tokens to expire and test refresh functionality

## 📊 Expected Status Codes

- **200 OK**: Successful operations
- **201 Created**: User created successfully
- **400 Bad Request**: Validation errors, duplicate data
- **401 Unauthorized**: Invalid credentials, missing/invalid token
- **404 Not Found**: Resource not found
- **500 Internal Server Error**: Server errors

## 🔄 Updated Authentication Flow

**Simplified Sign Up:**
- Only requires: Name, Email, Password, ConfirmPassword, Phone
- No username needed
- Email serves as the unique identifier

**Simplified Sign In:**
- Only requires: Email, Password
- Email is used for authentication

**JWT Claims:**
- `NameIdentifier`: User ID
- `Name`: Email address
- `Email`: Email address
- `Name`: Full name
- `Phone`: Phone number
- `Level`: User level

Happy testing! 🚀
