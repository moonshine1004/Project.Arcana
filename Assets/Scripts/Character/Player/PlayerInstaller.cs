using UnityEngine;

public class PlayerInstaller : MonoBehaviour
{
    private PlayerModel _playerModel;
    [SerializeField] private IPlayerView _playerView;
    private IPlayerPresenter _playerPresenter;

    public void Start()
    {
        _playerModel = new PlayerModel();
        _playerPresenter = new PlayerPresenter(_playerModel, _playerView);
        _playerView.InitializeView(_playerPresenter);
    }
}