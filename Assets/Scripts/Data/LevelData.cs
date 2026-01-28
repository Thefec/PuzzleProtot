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

    public void SetLevelData(int id, int width, int height, int photoId)
    {
        levelID = id;
        gridWidth = width;
        gridHeight = height;
        photoID = photoId;
    }
}
