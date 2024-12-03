# OTP Service Example

This repository is an example for sending otp emails using in .NET 8 minimal Api. 
FluentEmail librar is used for mail service.

## Prerequisites

- SMTP server credentials (e.g., Gmail, Outlook, or custom server)
- Setup database tables

## Setup

**Clone the Repository**:
   ```bash
   git clone https://github.com/LinThitHtwe/OTP-Service-Example.git
   ```

**Configure Environment Variables**: Add environment variables in a ```.env``` file inside ```FluentEmail.Example.Api``` project:

  ```bash
    EMAIL_HOST=smtp.example.com
    EMAIL_PORT=587
    EMAIL=your-email@example.com
    DEFAULT_EMAIL=your-email@example.com
    EMAIL_PASSWORD=your-email-password
  ```

**Set Up Database Table**
Run the following SQL script to set up the necessary tables:

```sql
USE [YourSchemaName]
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Username], [Email], [Password], [Status]) VALUES (1, N'JohnDoe', N'johndoe@example.com', N'password123', N'Active')
INSERT [dbo].[User] ([Id], [Username], [Email], [Password], [Status]) VALUES (3, N'Test', N'Test@gmail.com', N'+ziZLR2+R+8MKS5Ms4FnBxaVyo0+v8uNe0zB/HT1tOsLr4urJeNTeE04FTZWfuGWMAVTL+SSENp8RUzN8gPdhA==:3r4Bq12xFdecUCrp6/ccaAncQEJPQQ9NhCyH8OaAGnY=', N'Test')
INSERT [dbo].[User] ([Id], [Username], [Email], [Password], [Status]) VALUES (4, N'string', N'string', N'G5mGcFwW/f6f4eD3Av5moixiF+mA2zndWOelPoyivGYEKjj5H2VmRYYdxVqmWQ49h30cQ6r9JgBK5aMoPuIvew==:eaSgLN4wleHAEo1rUs9kBQpcSw3EC7bJwowQNL2i16U=', N'Pending')
INSERT [dbo].[User] ([Id], [Username], [Email], [Password], [Status]) VALUES (15, N'Lin Thit', N'linthit2745@gmail.com', N'1vuS2SW0KJJiO40uWGOqC8plNh3Ho+V+myWn3W1OXN34FAroE3Uy2L+lucDIBlOQQF7kN8Hca1jI9NUCUy3n6A==:1KUs3ke7OElE+uGJ6eP7Hl0XvF0ExLCK+7CJvwNU0ns=', N'Varified')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[OTP] ON 

INSERT [dbo].[OTP] ([Id], [OTPCode], [Status], [UserId], [ExpireTime], [CreatedTime]) VALUES (11, N'708166', N'Invalid', 15, CAST(N'2024-12-02T16:16:44.937' AS DateTime), CAST(N'2024-12-02T16:13:44.937' AS DateTime))
INSERT [dbo].[OTP] ([Id], [OTPCode], [Status], [UserId], [ExpireTime], [CreatedTime]) VALUES (12, N'819077', N'Invalid', 15, CAST(N'2024-12-02T16:17:24.573' AS DateTime), CAST(N'2024-12-02T16:14:24.573' AS DateTime))
INSERT [dbo].[OTP] ([Id], [OTPCode], [Status], [UserId], [ExpireTime], [CreatedTime]) VALUES (13, N'619522', N'Invalid', 15, CAST(N'2024-12-03T10:10:29.657' AS DateTime), CAST(N'2024-12-03T10:07:29.657' AS DateTime))
INSERT [dbo].[OTP] ([Id], [OTPCode], [Status], [UserId], [ExpireTime], [CreatedTime]) VALUES (14, N'698434', N'Used', 15, CAST(N'2024-12-03T10:15:07.550' AS DateTime), CAST(N'2024-12-03T10:12:07.550' AS DateTime))
INSERT [dbo].[OTP] ([Id], [OTPCode], [Status], [UserId], [ExpireTime], [CreatedTime]) VALUES (15, N'207603', N'Used', 15, CAST(N'2024-12-03T10:25:46.650' AS DateTime), CAST(N'2024-12-03T10:22:46.650' AS DateTime))
SET IDENTITY_INSERT [dbo].[OTP] OFF
GO
```

## Features

### Signup
- Users can sign up. Upon successful registration, the system sets the user’s status to **Pending** and sends an OTP for verification.

### Verify Signup OTP
- Verify the OTP sent during signup to activate the account.

### Resend OTP
- Resend a new OTP and invalidate any previously active OTPs.

### Signin
- Users with an **Active** status can sign in, which generates an authentication token.


## Usage

You can check out the [postman collections](https://github.com/LinThitHtwe/OTP-Service-Example/blob/main/OTPService.Example.postman_collection.json) for calling the endpoints.
