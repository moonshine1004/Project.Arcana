using System.Collections.Generic;
using System.Linq;

public interface ICardPresenter
{
    void UseCard(int index, out int thisCardID);
    void InitDeck(List<CardModel> deck);
    int[] GetHandCardIDs();
    List<CardModel> GetCardDeck();
}

public class CardPresenter : ICardPresenter
{
    private ICardView _cardView;
    private CardModel _cardModel;
    
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



    private List<CardModel> _deck= new List<CardModel>(12);

    private List<CardModel> _unusedCards = new List<CardModel>(); //드로우 전 카드 리스트
    private List<CardModel> _usedCards = new List<CardModel>(); //사용된 카드 리스트
    private CardModel[] _hand = new CardModel[5]; //qwert키에 할당되는 카드 배열


    private Dictionary<int, CardModel> cardModels;

    public void InitDeck(List<CardModel> deck)
    {
        _deck = deck;
    }

    public void InitializeCards()
    {
        
        foreach(CardModel data in Deck.Instance.CardDeck)
        {
            CreateNewCard(data);
        }
    }

    public void InitHand(Deck deck)
    {
        List<int> shuffledIDs;
        // 카드 덱을 셔플
        CardShuffleUtil.Shuffle(_deck.Select(card => card.cardID).ToList(), out shuffledIDs);
        for(int i =0; i < shuffledIDs.Count; i++)
        {
            (_deck[i], _deck[shuffledIDs[i]]) = (_deck[shuffledIDs[i]], _deck[i]);
        }
        for(int i =0; i < _hand.Length; i++) // hand에 카드 추가
        {
            _hand[i] = _deck[i];
        }
        for (int i = _hand.Length; i < _deck.Count; i++) // 남은 카드는 _unusedCards에 추가
        {
            _unusedCards.Add(_deck[i]);
        }

    }
    
    public void UseCard(int handIndex, out int thisCardID) // Player가 카드를 사용할 때, 플레이어 입력 키에 맞는 값을 받아옴
    {
        thisCardID = _hand[handIndex].cardID;
        _usedCards.Add(_hand[handIndex]); // handIndex 칸의 있는 카드를 사용된 카드 리스트에 추가
        _hand[handIndex] = null; // 사용한 카드는 hand에서 제거(null로 초기화)
        if (_unusedCards.Count == 0) // 드로우 전 카드 리스트가 비어있을 때 리필
        {
            List<int> shuffledIDs;
            CardShuffleUtil.Shuffle(_usedCards.Select(card => card.cardID).ToList(), out shuffledIDs);
            for(int i = 0; i < _usedCards.Count; i++)
            {
                _unusedCards[i] = _usedCards[shuffledIDs[i]];
            }
            _usedCards.Clear();
        }
        if (_unusedCards.Count > 0) // 카드가 있으면 이동하고 해당 칸 삭제
        {
            _hand[handIndex] = _unusedCards[0];
            _unusedCards.RemoveAt(0);
        }
    }

    public void CreateNewCard(CardModel cardData)
    {
        CardModel model = new CardModel();
        model.cost = cardData.cost;
        cardModels.Add(cardData.cardID,model);
    }

    public List<CardModel> GetAllCards()
    {
        return cardModels.Values.ToList();
    }

    public int[] GetHandCardIDs()
    {
        var hand = _hand.Select(card => card.cardID).ToArray();
        return hand;
    }
    public float[] GetHandCardDamage()
    {
        var hand = _hand.Select(card => card.damage).ToArray();
        return hand;
    }

    public List<CardModel> GetCardDeck()
    {
        return _deck;
    }
}


