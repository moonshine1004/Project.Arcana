using UnityEngine;

public class PopUpContentPresenter : MonoBehaviour
{
    [SerializeField] private PopUpContentView _popUpContentView;
    private PopUpContentModel _popUpContentModel = new PopUpContentModel();

    public void Start()
    {
        initalizeCard();
    }


    public void initalizeCard()
    {
        //_popUpContentView.Clear();
        _popUpContentView.Populate(_popUpContentModel.DeckCount);
    }
}
