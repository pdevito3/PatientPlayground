namespace KeycloakPulumiForPatientPlayground;

using KeycloakPulumiForPatientPlayground.Extensions;
using KeycloakPulumiForPatientPlayground.Factories;
using Pulumi;
using Pulumi.Keycloak;
using Pulumi.Keycloak.Inputs;

class RealmBuild : Stack
{
    public RealmBuild()
    {
        var realm = new Realm("DevRealm-realm", new RealmArgs
        {
            RealmName = "DevRealm",
            RegistrationAllowed = true,
            ResetPasswordAllowed = true,
            RememberMe = true,
            EditUsernameAllowed = true
        });
        var recipemanagementScope = ScopeFactory.CreateScope(realm.Id, "recipe_management");
        
        var patientManagementPostmanMachineClient = ClientFactory.CreateClientCredentialsFlowClient(realm.Id,
            "recipe_management.postman.machine", 
            "974d6f71-d41b-4601-9a7a-a33081f84682", 
            "PatientManagement Postman Machine",
            "https://oauth.pstmn.io");
        patientManagementPostmanMachineClient.ExtendDefaultScopes(recipemanagementScope.Name);
        patientManagementPostmanMachineClient.AddAudienceMapper("recipe_management");
        
        var patientManagementPostmanCodeClient = ClientFactory.CreateCodeFlowClient(realm.Id,
            "recipe_management.postman.code", 
            "974d6f71-d41b-4601-9a7a-a33081f84680", 
            "PatientManagement Postman Code",
            "https://oauth.pstmn.io",
            redirectUris: null,
            webOrigins: null
            );
        patientManagementPostmanCodeClient.ExtendDefaultScopes(recipemanagementScope.Name);
        patientManagementPostmanCodeClient.AddAudienceMapper("recipe_management");
        
        var patientManagementSwaggerClient = ClientFactory.CreateCodeFlowClient(realm.Id,
            "recipe_management.swagger", 
            "974d6f71-d41b-4601-9a7a-a33081f80687", 
            "PatientManagement Swagger",
            "https://localhost:5875",
            redirectUris: null,
            webOrigins: null
            );
        patientManagementSwaggerClient.ExtendDefaultScopes(recipemanagementScope.Name);
        patientManagementSwaggerClient.AddAudienceMapper("recipe_management");
        
        var patientManagementNextClient = ClientFactory.CreateCodeFlowClient(realm.Id,
            "recipe_management.next", 
            "974d6f71-d41b-4601-9a7a-a33081f82188", 
            "PatientManagement Next",
            "https://localhost:8511",
            redirectUris: new InputList<string>() 
                {
                "https://localhost:8511/*",
                },
            webOrigins: new InputList<string>() 
                {
                "https://localhost:5875",
                "https://localhost:8511",
                }
            );
        patientManagementNextClient.ExtendDefaultScopes(recipemanagementScope.Name);
        patientManagementNextClient.AddAudienceMapper("recipe_management");
        
        var bob = new User("bob", new UserArgs
        {
            RealmId = realm.Id,
            Username = "bob",
            Enabled = true,
            Email = "bob@domain.com",
            FirstName = "Smith",
            LastName = "Bobson",
            InitialPassword = new UserInitialPasswordArgs
            {
                Value = "bob",
                Temporary = true,
            },
        });

        var alice = new User("alice", new UserArgs
        {
            RealmId = realm.Id,
            Username = "alice",
            Enabled = true,
            Email = "alice@domain.com",
            FirstName = "Alice",
            LastName = "Smith",
            InitialPassword = new UserInitialPasswordArgs
            {
                Value = "alice",
                Temporary = true,
            },
        });
    }
}