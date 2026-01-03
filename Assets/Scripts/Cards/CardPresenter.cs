using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Single ton
public class CardPresenter
{
    private static CardPresenter _instance;
    public static CardPresenter Instance
    {
        get 
        {
            if(_instance == null)
            {
                _instance = new CardPresenter();
            }    
            return _instance;
        }
    }



    private List<CardModel> _deck = new List<CardModel>(12);
    public List<CardModel> CardDeck
    {
        get{return _deck;}
    }
    private List<CardModel> undealtDeck = new List<CardModel>(); //드로우 전 카드 리스트
    private List<CardModel> discardPile = new List<CardModel>(); //사용된 카드 리스트
    public CardModel[] hand = new CardModel[5]; //qwert키에 할당되는 카드 배열


    private Dictionary<int, CardModel> cardModels;

    public void Initialize()
    {
        
    }

    public void InitializeCards(Deck deck)
    {
        foreach(CardModel data in deck.CardDeck)
        {
            CreateNewCard(data);
        }
    }

    public void CreateNewCard(CardModel cardData)
    {
        // Model 만들기
        CardModel model = new CardModel();
        model.cost = cardData.cost;
        cardModels.Add(cardData.cardID,model);
    }

    public List<CardModel> GetAllCards()
    {
        return cardModels.Values.ToList();
    }





}


