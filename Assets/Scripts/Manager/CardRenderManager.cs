using System.Threading.Tasks;
using UnityEngine;

public class CardRenderManager : Singleton<CallingCloudCode>, GameManager.IGameManger
{
    [SerializeField] private CardUIRenderer _cardUIRenderer;
    
    public void IOnStart()
    {
        throw new System.NotImplementedException();
    }

    public Task IOnStartAsync()
    {
        throw new System.NotImplementedException();
    }

    public Task IOnUpdateAsync()
    {
        throw new System.NotImplementedException();
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TestCardRender()
    {
        _cardUIRenderer.CardRender();
    }
}
