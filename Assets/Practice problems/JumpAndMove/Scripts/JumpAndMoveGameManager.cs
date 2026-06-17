using System;
using UnityEngine;

public class JumpAndMoveGameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameStarted;
    [SerializeField] private int _currentScore;

    public bool IsGameStarted => _isGameStarted;

    public static Action<bool> OnGameStarted;
    public static Action<int> UpdateCurrentScore;
    public static JumpAndMoveGameManager Instance { get; private set; }

    #region Unity Methods
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        JumpAndMoveUIManager.OnButtonClicked += OnButtonClicked;
        TileTrigger.OnPlayerEnterTile += HandlePlayerEnterTile;
        JumpAndMovePlayerController.OnPlayerFall += HandleGameOver;
        _currentScore = -1;
    }

    private void OnDestroy()
    {
        JumpAndMoveUIManager.OnButtonClicked -= OnButtonClicked;
        TileTrigger.OnPlayerEnterTile -= HandlePlayerEnterTile;
        JumpAndMovePlayerController.OnPlayerFall -= HandleGameOver;
    }
    #endregion

    #region Private Methods
    private void OnButtonClicked(ButtonType button)
    {
        if(button == ButtonType.StartGame)
            ToggleGameState();    
    }

    private void ToggleGameState()
    {
        _isGameStarted = !_isGameStarted;
        OnGameStarted?.Invoke(_isGameStarted);
    }

    private void HandlePlayerEnterTile()
    {
        _currentScore++;
        UpdateCurrentScore?.Invoke(_currentScore);
    }

    private void HandleGameOver()
    {
        _isGameStarted = false;
        OnGameStarted?.Invoke(_isGameStarted);
    }

    #endregion
}
