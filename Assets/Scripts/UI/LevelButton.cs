using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Level button controller for level map
/// </summary>
public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private Button button;
    [SerializeField] private Image lockIcon;

    private int levelID;
    private LevelMapUI levelMapUI;

    /// <summary>
    /// Initialize level button
    /// </summary>
    public void Initialize(int id, LevelMapUI mapUI)
    {
        levelID = id;
        levelMapUI = mapUI;

        // Setup UI
        if (levelNumberText != null)
        {
            levelNumberText.text = levelID.ToString();
        }

        // Check if unlocked
        bool isUnlocked = GameManager.Instance.IsLevelUnlocked(levelID);
        
        if (button != null)
        {
            button.interactable = isUnlocked;
            button.onClick.AddListener(OnLevelClicked);
        }

        if (lockIcon != null)
        {
            lockIcon.gameObject.SetActive(!isUnlocked);
        }

        // Show best time if completed
        if (isUnlocked && bestTimeText != null)
        {
            float bestTime = GameManager.Instance.GetBestTime(levelID);
            if (bestTime > 0f)
            {
                int minutes = Mathf.FloorToInt(bestTime / 60f);
                int seconds = Mathf.FloorToInt(bestTime % 60f);
                bestTimeText.text = $"{minutes:00}:{seconds:00}";
            }
            else
            {
                bestTimeText.text = "--:--";
            }
        }
    }

    private void OnLevelClicked()
    {
        if (levelMapUI != null)
        {
            levelMapUI.OnLevelSelected(levelID);
        }
    }
}
