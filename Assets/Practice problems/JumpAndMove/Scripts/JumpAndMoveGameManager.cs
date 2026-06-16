using System;
using UnityEngine;

public class JumpAndMoveGameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameStarted;

    public bool IsGameStarted => _isGameStarted;

    public static Action<bool> OnGameStarted;
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
    }

    private void OnDestroy()
    {
        JumpAndMoveUIManager.OnButtonClicked -= OnButtonClicked;
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

    #endregion
}
