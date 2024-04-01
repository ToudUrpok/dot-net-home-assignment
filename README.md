# dot-net-home-assignment

Backend:
- Architecture:
  - BlogPlatform.Data - describes DB model, includes Entities folder with Data Entities Models and db context file.
  - BlogPlatform.Dtos - contains Data Transfer Objects models.
  - BlogPlatform.Services - contains: 
    - Users, Login, Posts and Comments services which implement application logic and used by corresponding controllers.
    - Exceptions folder with custom exceptions used by services.
  - BlogPlatform.WebApi - main web api project. Includes:
    - Controllers folder with User, Authorization, Post and Comment controllers which implement blog platform API endpoints. 
    - Middleware folder with custom Logging and Exception middleware implementation.
    - DB connection string and data required for JWT authorization are store in appsettings.Development.json file.
  - BlogPlatform.Test - test project which uses xUnit and Moq for testing Post, Comment and Authorization controllers.

- Run:
  - before running the WebApi project apply db migrations from:
      - VS Package Manager Console by command (choose BlogPlatform.WebApi): Update-Database
      - terminal via .Net Core CLI command 'dotnet ef database update'
  - then run BlogPlatform.WebApi in debug mode

- Test (with Swagger):
  1. add user via POST /User endpoint
  2. for authorization use POST /Authorization endpoint with email & password of added user
      - after successful authorization and receiving the token copy it -> click Authorize button in right top corner of the page -> paste your token to the Value field -> click Authorize
      - after authorization all endpoints will be available for testing

    Endpoints which don't require authorization:
      - POST /Authorization
      - GET /Commnet/{id}
      - GET /Post
      - GET /Post/{id}
      - POST /User
  3. Use VS Test Exporer for running unit test.

 Frontend:
 - Stack:
    - React + Vite + TS
    - TanStack React Query
    - React Router Dom
    - React Hook Form (library for forms validation)
    - Axios(http client library)
    - scss modules
  
- Architecture: application uses Feature-Sliced Design architectural methodology
  - app : contains application layer components
    - router - implements AppRouter instance (uses react-router-dom)
             - implements rerouting to login page for protected routes (saves protected page route to reroute back to it after login)
    - style: contains global styles files
    - App control
  - pages
    public:
      - BlogPage - main page with Posts list
      - PostPage - page with Post detailed view (including Post comments and add comment form). Accessible by selecting Post to read on BlogPage. Add Comment form available only for authorized user.
      - LoginPage
    private:
      - CreatePostPage
    - NotFoundPage - reroutes to this page if route is not found
  - widgets
    - Navbar - contains login/logout ui
    - Sidebar - displays app routes
    - Page - root for Pages controls
    - PageLoader - is shown while pages loading
  - features
      Authorization
        - contains LoginForm
        - authorization api/queries
  - entities
    - Post/Comment controls and models
  - shared
    - routeConfig: describes app routes
    - ui: custom ui controls library (Input, Button, AppLink, Loader, Card)
    - other instances used by all layers

- Run:
  - open terminal in BlogPlatformClient folder
  - install dependencies by running command 'npm install'
  - run 'npm run dev' command
  - Follow link http://localhost:5173/

SQL Task:
  - uses Recursive Common Table Expressions 
- Run
  - open CreateAmortizationScheduleRecursiveCTE.sql file from ComputationalQuestion folder with SQL Server Management Studio
  - Run the script