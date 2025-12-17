using UnityEngine;

public class PopUpContentView : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private CardView _cardView;


    /// <summary>
    /// UI에 count만큼 카드를 생성
    /// </summary>
    /// <param name="count"></param>
    public void Populate(int count)
    {
        for (int i = 0; i < count; i++)
            Instantiate(_cardView, _content);
    }

    public void Clear()
    {
        for (int i = _content.childCount - 1; i >= 0; i--)
            Destroy(_content.GetChild(i).gameObject);
    }

}
