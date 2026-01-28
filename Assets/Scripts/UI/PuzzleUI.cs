using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Puzzle game UI controller
/// </summary>
public class PuzzleUI : MonoBehaviour
{
    [Header("Game UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button backButton;

    [Header("Completion Panel")]
    [SerializeField] private GameObject completionPanel;
    [SerializeField] private TextMeshProUGUI completionTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private TextMeshProUGUI newRecordText;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnToMapButton;

    private PuzzleManager puzzleManager;

    private void Start()
    {
        puzzleManager = FindFirstObjectByType<PuzzleManager>();
        
        if (puzzleManager == null)
        {
            Debug.LogError("PuzzleManager not found in scene!");
        }

        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackClicked);
        }

        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.AddListener(OnNextLevelClicked);
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartClicked);
        }

        if (returnToMapButton != null)
        {
            returnToMapButton.onClick.AddListener(OnReturnToMapClicked);
        }

        ShowGameUI();
    }

    /// <summary>
    /// Show game UI and hide completion panel
    /// </summary>
    public void ShowGameUI()
    {
        if (completionPanel != null)
        {
            completionPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Update timer display
    /// </summary>
    public void UpdateTimer(float time)
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    /// <summary>
    /// Show completion panel with results
    /// </summary>
    public void ShowCompletionPanel(float completionTime, float bestTime, bool isNewRecord)
    {
        if (completionPanel != null)
        {
            completionPanel.SetActive(true);
        }

        // Show completion time
        if (completionTimeText != null)
        {
            int minutes = Mathf.FloorToInt(completionTime / 60f);
            int seconds = Mathf.FloorToInt(completionTime % 60f);
            completionTimeText.text = $"Time: {minutes:00}:{seconds:00}";
        }

        // Show best time
        if (bestTimeText != null)
        {
            int minutes = Mathf.FloorToInt(bestTime / 60f);
            int seconds = Mathf.FloorToInt(bestTime % 60f);
            bestTimeText.text = $"Best: {minutes:00}:{seconds:00}";
        }

        // Show new record message
        if (newRecordText != null)
        {
            newRecordText.gameObject.SetActive(isNewRecord);
        }
    }

    private void OnBackClicked()
    {
        if (puzzleManager != null)
        {
            puzzleManager.ReturnToLevelMap();
        }
    }

    private void OnNextLevelClicked()
    {
        if (puzzleManager != null)
        {
            puzzleManager.LoadNextLevel();
        }
    }

    private void OnRestartClicked()
    {
        if (puzzleManager != null)
        {
            puzzleManager.RestartLevel();
        }
    }

    private void OnReturnToMapClicked()
    {
        if (puzzleManager != null)
        {
            puzzleManager.ReturnToLevelMap();
        }
    }
}
