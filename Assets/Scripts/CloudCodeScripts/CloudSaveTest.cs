using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;

public class CloudSaveTest : MonoBehaviour, GameManager.IGameManger
{
    async void Start()
    {
        
    }

    [ContextMenu("SaveData")]
    public async void SaveData()
    {
        var playerData = new Dictionary<string, object>{
          {"firstKeyName", "a text value"},
          {"secondKeyName", 123}
        };
        await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
        Debug.Log($"Saved data {string.Join(',', playerData)}");
    }

    [ContextMenu("LoatData")]
    public async void LoadData()
    {
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {
          "firstKeyName", "secondKeyName"
        });

        if (playerData.TryGetValue("firstKeyName", out var firstKey)) {
            Debug.Log($"firstKeyName value: {firstKey.Value.GetAs<string>()}");
        }

        if (playerData.TryGetValue("secondKeyName", out var secondKey)) {
            Debug.Log($"secondKey value: {secondKey.Value.GetAs<int>()}");
        }
    }

    public void IOnStart()
    {
        throw new System.NotImplementedException();
    }

    public Task IOnStartAsync()
    {
        throw new System.NotImplementedException();
    }
}
