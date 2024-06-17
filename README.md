# Starting Code for "Using gRPC in ASP.NET Core"

 An example app for my Pluralsight course (Using gRPC in ASP.NET Core). The Course can be found at:

 > https://shawnl.ink/grpc

 ## Building the Example

 There are a couple of tasks before you can just run the project:

 ### Build the Database

 If you don't have the Entity Framework Core tools installed for .NET 3.0, you'll need to:

```cmd
C:/>dotnet tool install --global dotnet-ef
```

Then you can build the database by running this in the root of the project (at a console):

```cmd
C:/>dotnet ef database update
```

### Setup the NPM Dependencies

First you'll need to install the root project's NPM packages. To do so, go to the src/MeterReader/ directory:

```cmd
C:/> cd {RootFolder}/src/MeterReader
```

Then you'll need to run NPM's install:

```cmd
C:/.../MeterReader> npm install
```

Finally, merge the libraries with Gulp:

```cmd
C:/.../MeterReader> gulp
```

### Build the Vue Project

Next, you'll need to build the Vue project (only once). GO to the client directory under the MeterReader directory:

```cmd
C:/> cd {RootFolder}/src/MeterReader/client
```

Then run npm install in that folder:

```cmd
C:/.../client> npm install
```

Now you can build the project like so:

```
C:/.../client>npm run build
```

Now you're ready to open the project by opening the:

```cmd
C:/> cd {RootFolder}/src
C:/.../src> MeterReader.sln
```

With the project open, you can be ready to start the course.
