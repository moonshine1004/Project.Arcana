using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public interface ISwitchable
{
    public bool IsActive{ get; }
    public void Toggle(bool IsActive);
}

public class Switch: MonoBehaviour
{
    [SerializeField] private ISwitchable _client;

    private void Toggle()
    {
        _client.Toggle(_client.IsActive);
    }
    
}

public class Light: MonoBehaviour, ISwitchable
{
    private bool _isActive;
    public bool IsActive => _isActive;

    public void Toggle(bool IsActive) //Light의 Toggle() 메서드 구현
    {
        _isActive = !_isActive;
    }
}

public class Door : MonoBehaviour, ISwitchable
{
    private bool _isActive;
    private int password;
    public bool IsActive => _isActive;

    public void Toggle(bool IsActive) //Door의 Toggle() 메서드 구현
    {
        if (IsActive)
        {
            _isActive = false;
        }
        else if(!IsActive && password == 1234)
        {
            _isActive = true;
        }
    }
}