using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PopUpContentView : MonoBehaviour
{
    [SerializeField] private Transform _contentTransform;
    //카드의 뷰
    [SerializeField] private CardView _cardView;

    private int _deckCount = 12; //보여줄 개수
    private bool isPopUp = false;

    public void Start()
    {
        Populate(_deckCount);
        
    }

    /// <summary>
    /// UI에 count만큼 카드를 생성
    /// </summary>
    /// <param name="count"></param>
    public void Populate(int count)
    {
        CardPresenter.Instance.GetAllCards();
        for (int i = 0; i < count; i++)
        {
            CardView[] Contents = new CardView[12];
            Contents[i] = Instantiate(_cardView, _contentTransform);
            
        }
    }

    public void Clear()
    {
        for (int i = _contentTransform.childCount - 1; i >= 0; i--)
            Destroy(_contentTransform.GetChild(i).gameObject);
    }


    public void PopUpKeyInput(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            isPopUp = !isPopUp;
            if(isPopUp)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }
    }


}
