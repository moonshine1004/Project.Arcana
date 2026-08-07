using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using UnityEngine;
/// <summary>
/// 카드 덱을 서버에 저장하고 불러오는 스크립트입니다.
/// </summary>
public class DeckSavingModule : MonoBehaviour
{
    //저장할 덱 구성
    [SerializeField] private List<CardData> _cardDeck;
    [SerializeField] private Deck _deck;

    public Deck Deck
    {
        get => _deck;
    }

    [ContextMenu("SaveData")]
    public async void SaveData()
    {
        var cardList = _cardDeck;

        var cardDataDiction = new Dictionary<string, object>();
        for(int i = 0; i < 12; i++)
        {
            cardDataDiction.Add($"{i}", JsonUtility.ToJson(cardList[i]));
            Debug.Log($"{JsonUtility.ToJson(cardList[i])}");
        }

        //SaveAsync 메서드: 딕셔너리의 키와 값을 서버에 저장
        await CloudSaveService.Instance.Data.Player.SaveAsync(cardDataDiction);
        Debug.Log("ok");
        
    }

    public async Task initCardData(List<CardData> cardDatas)
    {   
        for(int i = 0; i<12; i++)
        {
            var cardData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {{i.ToString()}});
            cardData.TryGetValue($"{i}", out var card);
            var jsonFile = card.Value.GetAs<string>();
            var scriptableObj = ScriptableObject.CreateInstance<CardData>();
            JsonUtility.FromJsonOverwrite(jsonFile, scriptableObj);
            cardDatas[i] = scriptableObj;
            
        }       
    }



}
