using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CanvasType
{
    GameStart,
    InGame,
    GameOver
}

public enum ButtonType
{
    StartGame,
    RestartGame
}

public class JumpAndMoveUIManager : MonoBehaviour
{
    [Header("Canvas")] 
    [SerializeField] private Canvas _gameStartCanvas;
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private Canvas _inGameCanvas;

    [Header("Buttons")]
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _restartGameButton;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _currentScoreTextInGameOver;

    private int _currentScore;

    public static event Action<ButtonType> OnButtonClicked;

    #region Unity Methods
    private void Start()
    {
        _startGameButton.onClick.AddListener(OnStartGameButtonClick);
        _restartGameButton.onClick.AddListener(OnRestartGameButtonClick);
        JumpAndMoveGameManager.UpdateCurrentScore += HandleScoreboardUpdate;
        JumpAndMoveGameManager.OnGameStarted += HandleGameStateChanged;
        ToggleCanvas(CanvasType.GameStart);
    }

    private void OnDestroy()
    {
        _startGameButton.onClick.RemoveListener(OnStartGameButtonClick);
        _restartGameButton.onClick.RemoveListener(OnRestartGameButtonClick);
        JumpAndMoveGameManager.UpdateCurrentScore -= HandleScoreboardUpdate;
        JumpAndMoveGameManager.OnGameStarted -= HandleGameStateChanged;
    }
    #endregion

    #region Private Methods

    private void HandleGameStateChanged(bool started)
    {
        if(!started)
        {
            ToggleCanvas(CanvasType.GameOver);
            _currentScoreTextInGameOver.SetText("Score: " + _currentScore.ToString());
        }
        else
        {
            _currentScore = 0;
        }
    }

    private void HandleScoreboardUpdate(int score)
    {
        Debug.Log("Score updated: " + score);
        _currentScore = score;
        _currentScoreText.SetText("Score: " + _currentScore.ToString());
    }
    private void OnStartGameButtonClick()
    {
        OnButtonClicked?.Invoke(ButtonType.StartGame);
        ToggleCanvas(CanvasType.InGame);
    }

    private void OnRestartGameButtonClick()
    {
        OnButtonClicked?.Invoke(ButtonType.RestartGame);
        ToggleCanvas(CanvasType.InGame);
    }

    private void ToggleCanvas(CanvasType canvas)
    {
        _gameStartCanvas?.gameObject.SetActive(canvas == CanvasType.GameStart);
        _inGameCanvas?.gameObject.SetActive(canvas == CanvasType.InGame);
        _gameOverCanvas?.gameObject.SetActive(canvas == CanvasType.GameOver);
    }
    #endregion
}
