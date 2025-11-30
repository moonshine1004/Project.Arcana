using UnityEngine;
using Unity.Services.CloudCode.GeneratedBindings;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudCode;

public class CallingCloudCode : MonoBehaviour
{
    private async void Start()
    {

        // Initialize the Unity Services Core SDK
        await UnityServices.InitializeAsync();

        // Authenticate by logging into an anonymous account
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        try
        {
            // Call the function within the module and provide the parameters we defined in there
            var module = new MyModuleBindings(CloudCodeService.Instance);
            var result = await module.SayHello("World");

            Debug.Log(result);

                        // Call the function within the module and provide the parameters we defined in there
 
            var result２ = await module.GetRandom(6);

            Debug.Log(result２);
        }
        catch (CloudCodeException exception)
        {
            Debug.LogException(exception);

        }
    }
}