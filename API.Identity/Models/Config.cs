﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace API.Identity.Models
{
    public class Config
    {
        public const string OfflineAccess = IdentityServerConstants.StandardScopes.OfflineAccess;
        public const string OpenId = IdentityServerConstants.StandardScopes.OpenId;
        public const string SuperAdmin = "super_admin";
        public const string Admin = "admin";
        public const string User = "user";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope(SuperAdmin, "Super administrateur"),
                new ApiScope(Admin, "Administrateur"),
                new ApiScope(User, "Utilisateur")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "WebApplication",
                    ClientSecrets = { new Secret("879F65E8-A124-4AEA-9944-99771EFDEDC8".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = false,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        OpenId,
                        OfflineAccess,
                        SuperAdmin,
                        Admin,
                        User
                    },
                    RedirectUris={ "http://localhost:3000/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:3000/signout-oidc" }
                }
            };
    }
}
