using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class Deck : MonoBehaviour
{
    private static Deck _instance;
    public static Deck Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new Deck();
            }    
            return _instance;
        }
    }
    
    //플레이어가 구성한 카드 덱 클래스 입니다
    //카드 덱 리스트
    [SerializeField]private List<CardModel> _cardDeck = new List<CardModel>(12);
    public List<CardModel> CardDeck
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
