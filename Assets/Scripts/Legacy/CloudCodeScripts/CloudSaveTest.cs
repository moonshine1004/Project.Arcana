using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEditor;
using UnityEngine;

public class CloudSaveTest : MonoBehaviour, GameManager.IGameManger
{
    public ItemData[] itemDatas = new ItemData[10];
    
    async void Start()
    {

    }

    [ContextMenu("SaveData")]
    public async void SaveData()
    {
        var itemDataList = new List<ItemData>();
        var guids = AssetDatabase.FindAssets("t:ItemData", new[] { "Assets/Items" });
        foreach(var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            itemDataList.Add(AssetDatabase.LoadAssetAtPath<ItemData>(path));
        }
        
        var itemDataDiction = new Dictionary<string, object>();
        for(int i = 0; i < itemDataList.Count; i++)
        {
            itemDataDiction.Add($"{itemDataList[i].id}", JsonUtility.ToJson(itemDataList[i]));
        }

        //SaveAsync 메서드: 딕셔너리의 키와 값을 서버에 저장
        await CloudSaveService.Instance.Data.Player.SaveAsync(itemDataDiction);
        
        Debug.Log($"Saved data {string.Join(',', itemDataDiction)}");
    }

    /// <summary>
    /// 데이터 로드
    /// </summary>
    [ContextMenu("LoatData")]
    public async void LoadData()
    {
        for(int i = 0; i<10; i++)
        {
            var itemData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {{i.ToString()}});
            itemData.TryGetValue($"{i}", out var item);
            Debug.Log($"{i} value: {item.Value.GetAs<string>()}");

            var jsonFile = item.Value.GetAs<string>();
            var scriptableObj = ScriptableObject.CreateInstance<ItemData>();
            JsonUtility.FromJsonOverwrite(jsonFile, scriptableObj);
            itemDatas[i] = scriptableObj;
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

    public Task IOnUpdateAsync()
    {
        throw new NotImplementedException();
    }
}
