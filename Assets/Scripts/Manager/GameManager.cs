using Unity.Services.Authentication;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.CloudSave;
using Unity.VisualScripting;
using System.Threading.Tasks;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private CallingCloudCode _callingCloudCode;

    /// <summary>
    /// 게임 매니저의 Start(), Update() 등에 들어갈 게임 매니저 인터페이스를 상속 받는 클래스의 IOnStart(), IOnUpdate()를 정의
    /// </summary>
    public interface IGameManger
    {
        public void IOnStart();
        public Task IOnStartAsync(); 
    }



    async void Start()
    {
        await _callingCloudCode.IOnStartAsync();
        _callingCloudCode.IOnStart();
        
    }

    async void Awake()
    {
        
    }
    

}
