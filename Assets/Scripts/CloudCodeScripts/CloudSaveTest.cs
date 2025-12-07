using System;
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
        var li = new List<item>();
        li.Add(new item
        {
            Lev = 1,
            id = 1,
        });
        li.Add(new item
        {
            Lev = 2,
            id = 2,
        }); 
        li.Add(new item
        {
            Lev = 3,
            id = 3,
        });
        
        var playerData = new Dictionary<string, object>();
        for(int i = 0; i < li.Count; i++)
        {
            playerData.Add($"{li[i].id}", JsonUtility.ToJson(li[i]));
        }

        await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
        Debug.Log($"Saved data {string.Join(',', playerData)}");
    }
    [Serializable]
    public class item
    {
        public int Lev;
        public int id;
    }

    [ContextMenu("LoatData")]
    public async void LoadData()
    {
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {
          "firstKeyName", "secondKeyName"
        });

        if (playerData.TryGetValue("firstKeyName", out var firstKey))
        {
            Debug.Log($"firstKeyName value: {firstKey.Value.GetAs<string>()}");
        }

        if (playerData.TryGetValue("secondKeyName", out var secondKey))
        {
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
