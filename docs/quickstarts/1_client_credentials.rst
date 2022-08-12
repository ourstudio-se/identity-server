.. _refClientCredentialsQuickstart:

Protecting an API using Client Credentials
==========================================
The following Identity Server quickstart provides step by step instructions for various common IdentityServer scenarios. 
These start with the absolute basics and become more complex as they progress. We recommend that you follow them in sequence.  

Setting up the project
^^^^^^^^^^^^^^^^^^^^^^
First create a base for the quickstart:

    mkdir -p src
    dotnet new sln -n Quickstart

Add package sources for NuGet to locate Ourstudio.IdentityServer by creating a ``NuGet.Config`` file with the following contents::

    <?xml version="1.0" encoding="utf-8"?>
    <configuration>
        <packageRestore>
            <add key="enabled" value="True" />
            <add key="automatic" value="True" />
        </packageRestore>
        <packageSources>
            <clear />
            <add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3" />
            <add key="ourstudio-se" value="https://nuget.pkg.github.com/ourstudio-se/index.json" />
        </packageSources>
        <activePackageSource>
            <add key="All" value="(Aggregate source)" />
        </activePackageSource>
    </configuration>

.. note:: Run ``dotnet dev-certs https --trust`` in order to trust the development certificate. This only needs to be done once.

Defining the server
^^^^^^^^^^^^^^^^^^^
Create an empty .NET6 web application, e.g.::

    dotnet new web -n Server -o src/Server/
    dotnet sln Quickstart.sln add src/Server/Server.csproj

This will create a directory ``Server`` with the following files:

* ``Server.csproj`` - the project file and a ``Properties/launchSettings.json`` file
* ``Program.cs`` - the main application entry point
* ``appsettings.json`` - configuration file

Update the file ``Properties/launchSettings.json`` to look like this::

    {
        "profiles": {
            "Server": {
                "commandName": "Project",
                "dotnetRunMessages": true,
                "launchBrowser": true,
                "applicationUrl": "https://localhost:5001",
                "environmentVariables": {
                    "ASPNETCORE_ENVIRONMENT": "Development"
                }
            }
        }
    }


Add Ourstudio.IdentityServer
^^^^^^^^^^^^^^^^^^^^^^^^^^^^

    dotnet add src/Server/Server.csproj package Ourstudio.IdentityServer

Defining an API Scope
^^^^^^^^^^^^^^^^^^^^^
An API is a resource in your system that you want to protect. 
Resource definitions can be loaded in many ways, using a "code as configuration" approach:

Create a ``Config.cs`` file. Open it, add the following code snippet::

    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("api1", "My API")
            };
        
        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            { 
                new IdentityResources.OpenId()
            };

        public static IEnumerable<Client> Clients =>
            new[] 
                { };
    }

.. note:: If you will be using this in production it is important to give your API a logical name. Developers will be using this to connect to your api though your Identity server.  It should describe your api in simple terms to both developers and users.

Defining the client
^^^^^^^^^^^^^^^^^^^
The next step is to define a client application that we will use to access our new API.

For this scenario, the client will not have an interactive user, and will authenticate using the so called client secret with IdentityServer.

For this, add a client definition:: 

    public static IEnumerable<Client> Clients =>
        new[]
        {
            new Client
            {
                ClientId = "client",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "api1" }
            }
        };

You can think of the ClientId and the ClientSecret as the login and password for your application itself.  
It identifies your application to the identity server so that it knows which application is trying to connect to it.	

	
Configuring Ourstudio.IdentityServer
^^^^^^^^^^^^^^^^^^^^^^^^^^
Loading the resource and client definitions happens in ``Program.cs`` - update the code to look like this::

    var builder = WebApplication.CreateBuilder(args);

    builder.Services
        .AddIdentityServer()

        // WARNING! Do NOT use this in production mode!
        .AddDeveloperSigningCredential()

        .AddInMemoryIdentityResources(Config.IdentityResources)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddInMemoryClients(Config.Clients);

    var app = builder.Build();

    app.UseIdentityServer();
    app.Run();

That's it - your identity server should now be configured. If you run the server (``dotnet run --project src/Server/Server.csproj``) and navigate the browser to ``https://localhost:5001/.well-known/openid-configuration``, you should see the so-called discovery document. 
The discovery document is a standard endpoint in identity servers. The discovery document will be used by your clients and APIs to download the necessary configuration data.

.. image:: images/1_discovery.png

At first startup, Ourstudio.IdentityServer will create a developer signing key for you, it's a file called ``tempkey.jwk``.
You don't have to check that file into your source control, it will be re-created if it is not present.

Adding an API
^^^^^^^^^^^^^
Next, add an API to your solution::

    dotnet new webapi -n Api -o src/Api
    dotnet sln Quickstart.sln add src/Api/Api.csproj

Configure the API application to run on ``https://localhost:6001`` only. You can do this by editing the ``launchSettings.json`` file inside the Properties folder::

    {
        "profiles": {
            "Api": {
                "commandName": "Project",
                "dotnetRunMessages": true,
                "launchBrowser": true,
                "applicationUrl": "https://localhost:6001",
                "environmentVariables": {
                    "ASPNETCORE_ENVIRONMENT": "Development"
                }
            }
        }
    }

You can remove the files ``Controllers/WeatherForecastController.cs`` and ``WeatherForecast.cs``.

The controller
--------------
Add a new class called ``IdentityController``::

    [Route("identity")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }

This controller will be used later to test the authorization requirement, as well as visualize the claims identity through the eyes of the API.

Adding a Nuget Dependency
-------------------------
In order for the configuration step to work the nuget package dependency has to be added, run this command in the root directory::

    dotnet add src/Api/Api.csproj package Microsoft.AspNetCore.Authentication.JwtBearer

Configuration
-------------
The last step is to add the authentication services to DI (dependency injection) and the authentication middleware to the pipeline.
These will:

* validate the incoming token to make sure it is coming from a trusted issuer
* validate that the token is valid to be used with this api (aka audience)

Update ``Program.cs`` to look like this::

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer("Bearer", options =>
        {
            options.Authority = "https://localhost:5001";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
            };

            // WARNING! Do NOT use this in production mode!
            //
            // Since we're using a development certificate, we need to tell HttpClient
            // to trust the server certificate
            options.BackchannelHttpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (arg1, arg2, arg3, arg4) => true,
            };
        });

    var app = builder.Build();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.Run();

* ``AddAuthentication`` adds the authentication services to DI and configures ``Bearer`` as the default scheme. 
* ``UseAuthentication`` adds the authentication middleware to the pipeline so authentication will be performed automatically on every call into the host.
* ``UseAuthorization`` adds the authorization middleware to make sure, our API endpoint cannot be accessed by anonymous clients.

Navigating to the controller ``https://localhost:6001/identity`` on a browser should return a 401 status code. 
This means your API requires a credential and is now protected by IdentityServer.

.. note:: If you are wondering, why the above code disables audience validation, have a look :ref:`here <refResources>` for a more in-depth discussion.

Creating the client
^^^^^^^^^^^^^^^^^^^
The last step is to write a client that requests an access token, and then uses this token to access the API. For that, add a console project to your solution::

    dotnet new console -n Client -o src/
    dotnet sln Quickstart.sln add src/Client/Client.csproj

The token endpoint at IdentityServer implements the OAuth 2.0 protocol, and you could use raw HTTP to access it. 
However, we have a client library called IdentityModel, that encapsulates the protocol interaction in an easy to use API.

Add the ``IdentityModel`` NuGet package to your client::

    dotnet add src/Client/Client.csproj package IdentityModel

IdentityModel includes a client library to use with the discovery endpoint. This way you only need to know the base-address of IdentityServer - the actual endpoint addresses can be read from the metadata::

    // discover endpoints from metadata
    var client = new HttpClient(new HttpClientHandler
    {
        // WARNING! Do NOT use this in production mode!
        //
        // Since we're using a development certificate, we need to tell HttpClient
        // to trust the server certificate
        ServerCertificateCustomValidationCallback = (arg1, arg2, arg3, arg4) =>
        {
            return true;
        }
    });
    var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
    if (disco.IsError)
    {
        Console.WriteLine(disco.Error);
        return;
    }

Next you can use the information from the discovery document to request a token to IdentityServer to access ``api1``::

    // request token
    var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
    {
        Address = disco.TokenEndpoint,

        ClientId = "client",
        ClientSecret = "secret",
        Scope = "api1"
    });
    
    if (tokenResponse.IsError)
    {
        Console.WriteLine(tokenResponse.Error);
        return;
    }

    Console.WriteLine(tokenResponse.Json);

.. note:: Copy and paste the access token from the console to `jwt.ms <https://jwt.ms>`_ to inspect the raw token.

Calling the API
^^^^^^^^^^^^^^^
To send the access token to the API you typically use the HTTP Authorization header. This is done using the ``SetBearerToken`` extension method::

    // call api
    client.SetBearerToken(tokenResponse.AccessToken);

    var response = await client.GetAsync("https://localhost:6001/identity");
    if (!response.IsSuccessStatusCode)
    {
        Console.WriteLine(response.StatusCode);
    }
    else
    {
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(JArray.Parse(content));
    }

The output should look like this:

.. image:: images/1_client_screenshot.png

.. note:: By default an access token will contain claims about the scope, lifetime (nbf and exp), the client ID (client_id) and the issuer name (iss).

Authorization at the API
^^^^^^^^^^^^^^^^^^^^^^^^
Right now, the API accepts any access token issued by your identity server.

In the following we will add code that allows checking for the presence of the scope in the access token that the client asked for (and got granted).
For this we will use the ASP.NET Core authorization policy system. Add the following to ``Program.cs``::

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("ApiScope", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("scope", "api1");
        });
    });

You can now enforce this policy at various levels, e.g.

* globally
* for all API endpoints
* for specific controllers/actions

Typically you setup the policy for all API endpoints in the routing system::

    app.MapControllers().RequireAuthorization("ApiScope");


Further experiments
^^^^^^^^^^^^^^^^^^^
This walkthrough focused on the success path so far

* client was able to request token
* client could use the token to access the API

You can now try to provoke errors to learn how the system behaves, e.g.

* try to connect to IdentityServer when it is not running (unavailable)
* try to use an invalid client id or secret to request the token
* try to ask for an invalid scope during the token request
* try to call the API when it is not running (unavailable)
* don't send the token to the API
* configure the API to require a different scope than the one in the token
