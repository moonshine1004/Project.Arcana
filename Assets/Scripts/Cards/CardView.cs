using System.Collections.Generic;
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

    public void SetCartView(CardModel model)
    {
        _costText.text = model.cost.ToString();
        _damageText.text = model.damage.ToString();
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
