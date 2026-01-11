using UnityEngine.Rendering.Universal;

public interface IPlayerPresenter
{
    int OnUseCard(int index);
}

public class PlayerPresenter : IPlayerPresenter
{
    private PlayerModel _playerModel;
    private IPlayerView _playerView;

    public PlayerPresenter(PlayerModel playerModel, IPlayerView playerView)
    {
        _playerModel = playerModel;
        _playerView = playerView;
    }

    private ICardPresenter _cardPresenter;

    public int OnUseCard(int index)
    {
        int thisCard;
        _cardPresenter.UseCard(index, out thisCard);
        return thisCard;
    }
}