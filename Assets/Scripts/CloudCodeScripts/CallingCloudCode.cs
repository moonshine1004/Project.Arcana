using UnityEngine;
using Unity.Services.CloudCode.GeneratedBindings;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudCode;
using System.Reflection;
using System.Threading.Tasks;

public class CallingCloudCode : Singleton<CallingCloudCode>, GameManager.IGameManger
{
    private async void Start()
    {   

        
    }

    public async Task CloudeCodeAuthentication()
    {
        // Initialize the Unity Services Core SDK
        await UnityServices.InitializeAsync();
        // Authenticate by logging into an anonymous account
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

    }


    public async void UseUGS()
    {
        try
        {
            var module = new MyModuleBindings(CloudCodeService.Instance);
            var result = await module.SayHello("say hi");
            var result2 = await module.GetRandom(6);
            Debug.Log(result2);
        }
        catch(CloudCodeException exception)
        {
            Debug.LogException(exception);
            Debug.Log("fail");
        }
    }


    public void IOnStart()
    {
        
    }

    public async Task IOnStartAsync()
    {
        await CloudeCodeAuthentication();
        UseUGS();
    }
}