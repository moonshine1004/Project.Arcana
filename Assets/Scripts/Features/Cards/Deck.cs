using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class Deck : MonoBehaviour
{
    [SerializeField]private DeckSavingModule _saveDeck;

    
    //플레이어가 구성한 카드 덱 클래스 입니다
    //카드 덱 리스트
    [SerializeField]private List<CardData> _cardDeck =new List<CardData>(12);
    public List<CardData> CardDeck
    {
        get{return _cardDeck;}
    }


    public async void Start()
    {

    }

    public void Update()
    {
        
    }

}
