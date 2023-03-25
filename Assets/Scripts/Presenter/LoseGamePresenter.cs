using UnityEngine;

public class LoseGamePresenter
{
    private LoseGameView _loseGameView;
    private LoseGame _loseGame;
    private NeuronCollectorView _neuronCollectorView;

    public void Init(LoseGameView loseGameView, LoseGame loseGame, NeuronCollectorView neuronCollectorView)
    {
        _loseGameView = loseGameView;
        _loseGame = loseGame;
        _neuronCollectorView = neuronCollectorView;
    }

    public void Enable()
    {
        _loseGameView.TryActiveLoseGame += RequestLoseGame;
        _loseGameView.TryGetDayCount += RequestDayCount;

        _loseGame.GiveLoseGame += OnGiveLoseGame;
        _loseGame.GiveDayCount += OnGiveDayCount;
    }

    public void Disable()
    {
        _loseGameView.TryActiveLoseGame -= RequestLoseGame;
        _loseGameView.TryGetDayCount -= RequestDayCount;

        _loseGame.GiveLoseGame -= OnGiveLoseGame;
        _loseGame.GiveDayCount -= OnGiveDayCount;
    }

    private void RequestLoseGame()
    {
        _loseGame.LoseGameRequest();
    }

    private void OnGiveLoseGame(int neuronCount)
    {
        _neuronCollectorView.ChangeNeuronView(neuronCount);
        _loseGameView.LoseGame();
    }

    private void RequestDayCount()
    {
        _loseGame.DayCountRequest();
    }

    private void OnGiveDayCount(int day)
    {
        _loseGameView.SetDayCount(day);
    }
}
