using UnityEngine;

/// <summary>
/// Main game manager - Singleton pattern
/// Manages global game state and data
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private PhotoDatabase photoDatabase;
    [SerializeField] private LevelDatabase levelDatabase;

    private LevelData currentLevel;
    private int selectedLevelID;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("GameManager");
                instance = go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    public PhotoDatabase PhotoDatabase => photoDatabase;
    public LevelDatabase LevelDatabase => levelDatabase;
    public LevelData CurrentLevel => currentLevel;
    public int SelectedLevelID => selectedLevelID;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadDatabases();
    }

    /// <summary>
    /// Load databases from Resources
    /// </summary>
    private void LoadDatabases()
    {
        if (photoDatabase == null)
        {
            photoDatabase = Resources.Load<PhotoDatabase>("Data/PhotoDatabase");
            if (photoDatabase == null)
            {
                Debug.LogWarning("PhotoDatabase not found in Resources/Data/");
            }
        }

        if (levelDatabase == null)
        {
            levelDatabase = Resources.Load<LevelDatabase>("Data/LevelDatabase");
            if (levelDatabase == null)
            {
                Debug.LogWarning("LevelDatabase not found in Resources/Data/");
            }
        }
    }

    /// <summary>
    /// Set the current level to play
    /// </summary>
    public void SetCurrentLevel(int levelID)
    {
        selectedLevelID = levelID;
        currentLevel = levelDatabase.GetLevelByID(levelID);
        
        if (currentLevel == null)
        {
            Debug.LogError($"Level with ID {levelID} not found!");
        }
    }

    /// <summary>
    /// Get photo sprite by ID
    /// </summary>
    public Sprite GetPhotoSprite(int photoID)
    {
        PhotoData photo = photoDatabase.GetPhotoByID(photoID);
        return photo?.PhotoSprite;
    }

    /// <summary>
    /// Get best time for a level
    /// </summary>
    public float GetBestTime(int levelID)
    {
        return PlayerPrefs.GetFloat($"Level_{levelID}_BestTime", 0f);
    }

    /// <summary>
    /// Save best time for a level
    /// </summary>
    public bool SaveBestTime(int levelID, float time)
    {
        float currentBest = GetBestTime(levelID);
        
        if (currentBest == 0f || time < currentBest)
        {
            PlayerPrefs.SetFloat($"Level_{levelID}_BestTime", time);
            PlayerPrefs.Save();
            return true; // New record
        }
        
        return false; // Not a new record
    }

    /// <summary>
    /// Check if level is unlocked
    /// </summary>
    public bool IsLevelUnlocked(int levelID)
    {
        if (levelID == 1) return true; // First level always unlocked
        
        // Check if previous level is completed
        float previousBest = GetBestTime(levelID - 1);
        return previousBest > 0f;
    }
}
