.. _refAspNetIdentityQuickstart:
Using ASP.NET Core Identity
===========================

IdentityServer is designed for flexibility and part of that is allowing you to use any database you want for your users and their data (including passwords).
If you are starting with a new user database, then ASP.NET Core Identity is one option you could choose.
This quickstart shows how to use ASP.NET Core Identity with IdentityServer.

The approach this quickstart takes to using ASP.NET Core Identity is to create a new project for the IdentityServer host.
This new project will replace the prior IdentityServer project we built up in the previous quickstarts.
The reason for this new project is due to the differences in UI assets when using ASP.NET Core Identity (mainly around the differences in login and logout).
All the other projects in this solution (for the clients and the API) will remain the same.

.. Note:: This quickstart assumes you are familiar with how ASP.NET Core Identity works. If you are not, it is recommended that you first `learn about it <https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity>`_.

An ASP.NET Core Identity integration project
^^^^^^^^^^^^^^^^^^^^^^^

Add a reference to ``Ourstudio.IdentityServer.AspNetIdentity``. 
This NuGet package contains the ASP.NET Core Identity integration components for Ourstudio.IdentityServer.

Program.cs
----------

When configuring services, make sure the necessary ``AddDbContext<ApplicationDbContext>`` and ``AddIdentity<ApplicationUser, IdentityRole>`` calls are done to configure ASP.NET Core Identity.

Finally, add a call to ``AddAspNetIdentity<ApplicationUser>``.
``AddAspNetIdentity`` adds the integration layer to allow Ourstudio.IdentityServer to access the user data for the ASP.NET Core Identity user database.
This is needed when Ourstudio.IdentityServer must add claims for the users into tokens.

Note that ``AddIdentity<ApplicationUser, IdentityRole>`` must be invoked before ``AddIdentityServer``.

Config.cs
-----------

``Config.cs`` contains the hard-coded in-memory clients and resource definitions.
To keep the same clients and API working as the prior quickstarts, we need to copy over the configuration data from the old IdentityServer project into this one.
Do that now, and afterwards ``Config.cs`` should look like this::

    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new []
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new []
            {
                new ApiScope("api1", "My API")
            };

        public static IEnumerable<Client> Clients =>
            new []
            {
                // machine to machine client
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                
                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                }
            };
    }


Logging in user accounts
------------------------

Use of the ``SignInManager<ApplicationUser>`` and ``UserManager<ApplicationUser>`` from ASP.NET Core Identity to validate credentials and manage the authentication session.


.. image:: images/aspid_mvc_client.png

You should be redirected to the ASP.NET Core Identity login page.
Login with your newly created user:

.. image:: images/aspid_login.png

After login you see the normal consent page. 
After consent you will be redirected back to the MVC client application where your user's claims should be listed.

.. image:: images/aspid_claims.png

You should also be able to click "Call API using application identity" to invoke the API on behalf of the user:

.. image:: images/aspid_api_claims.png

And now you're using users from ASP.NET Core Identity in IdentityServer.
