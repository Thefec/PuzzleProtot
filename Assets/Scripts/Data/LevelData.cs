using UnityEngine;

/// <summary>
/// ScriptableObject for individual level configuration
/// </summary>
[CreateAssetMenu(fileName = "Level_", menuName = "Puzzle/Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private int levelID;
    [SerializeField] private int gridWidth = 3;
    [SerializeField] private int gridHeight = 3;
    [SerializeField] private int photoID;

    public int LevelID => levelID;
    public int GridWidth => gridWidth;
    public int GridHeight => gridHeight;
    public int PhotoID => photoID;

    /// <summary>
    /// Set the data for this level
    /// </summary>
    public void SetLevelData(int id, int width, int height, int photoId)
    {
        if (width <= 0 || height <= 0)
        {
            Debug.LogWarning($"Invalid grid dimensions: {width}x{height}. Using minimum of 2x2.");
            width = Mathf.Max(2, width);
            height = Mathf.Max(2, height);
        }
        
        levelID = id;
        gridWidth = width;
        gridHeight = height;
        photoID = photoId;
    }
}
