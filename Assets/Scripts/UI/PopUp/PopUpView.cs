using System;
using UnityEngine;

public class PopUpView : MonoBehaviour
{
    public event Action CheckClick;
    [SerializeField] private GameObject _popUpUI;

    public GameObject PopUpUI
    {
        get => _popUpUI;
    } 

    public void Show()
    {
        _popUpUI.SetActive(true);
    }
    public void Hide()
    {
        _popUpUI.SetActive(false);
    }
    
}
