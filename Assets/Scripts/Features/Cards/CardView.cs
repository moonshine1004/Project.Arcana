using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable All

public class CardView : MonoBehaviour
{
    //카드를 UI상에 표시하는 스크립트
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] public Image _image;
    [SerializeField] private CardManager _cardManager;
    private CardData _cardData;


    public async void Initialize(CardData card)
    {
        _cardData = card;
        _costText.text = card.cost.ToString();
        _damageText.text = card.damage.ToString();

    }
    

    public void ResetView()
    {
        _cardData = null;
        _costText.text = "";
        _damageText.text = "";
    }

    private Color GetColorByElement(Enum.Element element)
    {
        switch (element)
        {
            case Enum.Element.Fire: return Color.red;
            case Enum.Element.Ice: return Color.cyan;
            case Enum.Element.Wind: return Color.green;
            case Enum.Element.Rock: return new Color(0.6f, 0.4f, 0.2f);
            case Enum.Element.Elect: return Color.yellow;
            default: return Color.gray;
        }
    }
}
