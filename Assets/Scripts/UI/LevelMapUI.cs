using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Level map UI controller
/// </summary>
public class LevelMapUI : MonoBehaviour
{
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private Transform levelButtonContainer;
    [SerializeField] private Button backButton;
    [SerializeField] private ScrollRect scrollRect;

    private void Start()
    {
        GenerateLevelButtons();

        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackClicked);
        }
    }

    /// <summary>
    /// Generate level buttons from database
    /// </summary>
    private void GenerateLevelButtons()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance not found!");
            return;
        }

        if (GameManager.Instance.LevelDatabase == null)
        {
            Debug.LogError("LevelDatabase not found!");
            return;
        }

        var levels = GameManager.Instance.LevelDatabase.Levels;

        foreach (var level in levels)
        {
            GameObject buttonObj = Instantiate(levelButtonPrefab, levelButtonContainer);
            LevelButton levelButton = buttonObj.GetComponent<LevelButton>();
            
            if (levelButton != null)
            {
                levelButton.Initialize(level.LevelID, this);
            }
        }

        Debug.Log($"Generated {levels.Count} level buttons");
    }

    /// <summary>
    /// Handle level selection
    /// </summary>
    public void OnLevelSelected(int levelID)
    {
        GameManager.Instance.SetCurrentLevel(levelID);
        SceneLoader.Instance.LoadPuzzleGame();
    }

    private void OnBackClicked()
    {
        SceneLoader.Instance.LoadMainMenu();
    }
}
