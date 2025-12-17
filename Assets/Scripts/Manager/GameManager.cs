using Unity.Services.Authentication;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.CloudSave;
using Unity.VisualScripting;
using System.Threading.Tasks;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private CallingCloudCode _callingCloudCode;
    [SerializeField] private CardRenderManager _cardRenderManager;

    public static bool LogIn = false;

    /// <summary>
    /// 게임 매니저의 Start(), Update() 등에 들어갈 게임 매니저 인터페이스를 상속 받는 클래스의 IOnStart(), IOnUpdate()를 정의
    /// </summary>
    public interface IGameManger
    {   
        public void IOnStart();
        public Task IOnStartAsync(); 
        public Task IOnUpdateAsync();
    }



    async void Start()
    {
        
        
    }

    async void Awake()
    {
        await _callingCloudCode.IOnStartAsync();
        Debug.Log("로그인 끝");
        _callingCloudCode.LogIn = true;


        
    }

    private void Update()
    {
        if (_callingCloudCode.LogIn==true)
        {
            _cardRenderManager.TestCardRender();
            
        }
    }
    

}
