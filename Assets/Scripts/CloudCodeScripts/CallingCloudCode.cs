using UnityEngine;
using Unity.Services.CloudCode.GeneratedBindings;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudCode;
using System.Reflection;
using System.Threading.Tasks;

public class CallingCloudCode : Singleton<CallingCloudCode>, GameManager.IGameManger
{
    #region 관리할 하위 매니저들
    [SerializeField] private DeckSavingModule _saveDeck;
    #endregion
    
    #region 필드
    private bool _login = false;
    #endregion

    #region 프로퍼티
    public bool LogIn
    {
        get => _login;
        set
        {
            _login = value;
        }
    }
    #endregion
    [SerializeField] private ICardPresenter _cardPresenter;
    
    private async void Start()
    {   
        
        
    }
    //async: 
    public async Task CloudeCodeAuthentication()
    {
        //
        await UnityServices.InitializeAsync();
        //
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

    }


    public async void UseUGS()
    {
        try
        {
            var module = new MyModuleBindings(CloudCodeService.Instance);
            //var result = await module.SayHello("say hi");
            //var result2 = await module.GetRandom(6);
            //Debug.Log(result2);
        }
        catch(CloudCodeException exception)
        {
            Debug.LogException(exception);
            Debug.Log("fail");
        }
    }


    public void IOnStart()
    {
        
    }

    public async Task IOnStartAsync()
    {
        await CloudeCodeAuthentication();

        await _saveDeck.InitCardData(_cardPresenter.GetCardDeck());
        Debug.Log("카드 불러오기 끝");

        //await _cardPresenter.Init(_cardPresenter.GetCardDeck());
        //CardPresenter.Instance.InitializeCards(_usingCardList.Deck);
        Debug.Log("카드 이전 완료");
    }

    public Task IOnUpdateAsync()
    {
        throw new System.NotImplementedException(); 
    }
}