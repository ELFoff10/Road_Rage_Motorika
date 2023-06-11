using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates { countDown, running, raceOver};

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private GameStates _gameState = GameStates.countDown;

    private float _raceStartedTime = 0;
    private float _raceCompletedTime = 0;

    public event Action<GameManager> OnGameStateChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void LevelStart()
    {
        _gameState = GameStates.countDown;
    }

    public GameStates GetGameState()
    {
        return _gameState;
    }

    private void ChangeGameState(GameStates newGameState)
    {
        if (_gameState != newGameState)
        {
            _gameState = newGameState;

            OnGameStateChanged?.Invoke(this);
        }
    }

    public float GetRaceTime()
    {
        if (_gameState == GameStates.countDown)
        {
            return 0;
        }
        else if (_gameState == GameStates.raceOver)
        {
            return _raceCompletedTime - _raceStartedTime;
        }
        else
        {
            return Time.time - _raceStartedTime;
        }
    }

    public void OnRaceStart()
    {
        _raceStartedTime = Time.time;

        ChangeGameState(GameStates.running);
    }
    public void OnRaceCompleted()
    {
        _raceCompletedTime = Time.time;

        ChangeGameState(GameStates.raceOver);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LevelStart();
    }
}
