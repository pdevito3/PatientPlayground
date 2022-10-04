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
        var patientmanagementScope = ScopeFactory.CreateScope(realm.Id, "patient_management");
        
        var patientManagementPostmanMachineClient = ClientFactory.CreateClientCredentialsFlowClient(realm.Id,
            "patient_management.postman.machine", 
            "974d6f71-d41b-4601-9a7a-a33081f84682", 
            "PatientManagement Postman Machine",
            "https://oauth.pstmn.io");
        patientManagementPostmanMachineClient.ExtendDefaultScopes(patientmanagementScope.Name);
        patientManagementPostmanMachineClient.AddAudienceMapper("patient_management");
        
        var patientManagementPostmanCodeClient = ClientFactory.CreateCodeFlowClient(realm.Id,
            "patient_management.postman.code", 
            "974d6f71-d41b-4601-9a7a-a33081f84680", 
            "PatientManagement Postman Code",
            "https://oauth.pstmn.io",
            redirectUris: null,
            webOrigins: null
            );
        patientManagementPostmanCodeClient.ExtendDefaultScopes(patientmanagementScope.Name);
        patientManagementPostmanCodeClient.AddAudienceMapper("patient_management");
        
        var patientManagementSwaggerClient = ClientFactory.CreateCodeFlowClient(realm.Id,
            "patient_management.swagger", 
            "974d6f71-d41b-4601-9a7a-a33081f80687", 
            "PatientManagement Swagger",
            "https://localhost:5875",
            redirectUris: null,
            webOrigins: null
            );
        patientManagementSwaggerClient.ExtendDefaultScopes(patientmanagementScope.Name);
        patientManagementSwaggerClient.AddAudienceMapper("patient_management");
        
        var patientManagementNextClient = ClientFactory.CreateCodeFlowClient(realm.Id,
            "patient_management.next", 
            "974d6f71-d41b-4601-9a7a-a33081f82188", 
            "PatientManagement Next",
            "http://localhost:8511",
            redirectUris: new InputList<string>() 
                {
                "http://localhost:8511/*",
                },
            webOrigins: new InputList<string>() 
                {
                "https://localhost:5875",
                "http://localhost:8511",
                }
            );
        patientManagementNextClient.ExtendDefaultScopes(patientmanagementScope.Name);
        patientManagementNextClient.AddAudienceMapper("patient_management");
        
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