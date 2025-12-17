using UnityEngine;
using UnityEngine.InputSystem;

public class PopUpController : MonoBehaviour
{
    [SerializeField] private PopUpView _popUpView;  //팝업 view 참조로 연결
    private PopUpModel _popUpModel = new PopUpModel();  //모델은 객체를 생성->어차피 로직만 가져다 쓸거니까

    public void Start()
    {
        _popUpView = gameObject.GetComponent<PopUpView>();
    }

    public void PopUpKeyInput(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            _popUpModel.Toggle();
            if(_popUpModel.IsPopUp)
                _popUpView.Show();
            else
                _popUpView.Hide();
        }
    }
}
