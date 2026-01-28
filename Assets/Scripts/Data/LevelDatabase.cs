using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ScriptableObject database for managing all levels
/// </summary>
[CreateAssetMenu(fileName = "LevelDatabase", menuName = "Puzzle/Level Database")]
public class LevelDatabase : ScriptableObject
{
    [SerializeField] private List<LevelData> levels = new List<LevelData>();

    public List<LevelData> Levels => levels;

    /// <summary>
    /// Get level by ID
    /// </summary>
    public LevelData GetLevelByID(int id)
    {
        return levels.Find(l => l.LevelID == id);
    }

    /// <summary>
    /// Add level to database
    /// </summary>
    public void AddLevel(LevelData level)
    {
        if (!levels.Contains(level))
        {
            levels.Add(level);
            SortLevels();
        }
    }

    /// <summary>
    /// Remove level from database
    /// </summary>
    public void RemoveLevel(LevelData level)
    {
        levels.Remove(level);
    }

    /// <summary>
    /// Sort levels by ID
    /// </summary>
    private void SortLevels()
    {
        levels.Sort((a, b) => a.LevelID.CompareTo(b.LevelID));
    }

    /// <summary>
    /// Get total level count
    /// </summary>
    public int GetLevelCount()
    {
        return levels.Count;
    }
}
