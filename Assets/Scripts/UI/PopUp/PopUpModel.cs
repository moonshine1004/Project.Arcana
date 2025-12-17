using UnityEngine;

public class PopUpModel : MonoBehaviour
{
    private bool isPopUp = false;
    
    //팝업 '상태'
    public bool IsPopUp
    {
        get => isPopUp;
        set{isPopUp = value;}
    }

    //팝업 '상태 전환' 메서드
    public void Toggle()
    {
        isPopUp = !isPopUp;
    }


}
