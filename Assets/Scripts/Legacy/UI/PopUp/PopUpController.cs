using UnityEngine;
using UnityEngine.InputSystem;

public class PopUpController : MonoBehaviour
{    
    [SerializeField] private GameObject _popUpView;
    private bool isPopUp = false;

    //팝업 '상태 전환' 메서드
    public void Toggle()
    {
        isPopUp = !isPopUp;
    }


    public void PopUpKeyInput(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            Toggle();
            if(isPopUp)
                Show();
            else
                Hide();
        }
    }

    public void Show()
    {
        _popUpView.SetActive(true);
    }
    public void Hide()
    {
        _popUpView.SetActive(false);
    }
}
