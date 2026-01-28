using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom editor for LevelData to provide better UI for level configuration
/// </summary>
[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor
{
    private static readonly string[] gridSizeOptions = new string[]
    {
        "3x3", "3x4", "4x3", "4x4", "4x5", "5x4", "5x5", "6x4", "6x5", "Custom"
    };

    private int selectedGridIndex = 0;
    private PhotoDatabase photoDatabase;

    private void OnEnable()
    {
        LoadPhotoDatabase();
    }

    private void LoadPhotoDatabase()
    {
        string[] guids = AssetDatabase.FindAssets("t:PhotoDatabase");
        if (guids.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            photoDatabase = AssetDatabase.LoadAssetAtPath<PhotoDatabase>(path);
        }
    }

    public override void OnInspectorGUI()
    {
        LevelData level = (LevelData)target;
        
        EditorGUI.BeginChangeCheck();

        // Level ID
        EditorGUILayout.LabelField("Level Configuration", EditorStyles.boldLabel);
        int levelID = EditorGUILayout.IntField("Level ID", level.LevelID);

        EditorGUILayout.Space();

        // Grid Size Selection
        EditorGUILayout.LabelField("Grid Size", EditorStyles.boldLabel);
        selectedGridIndex = EditorGUILayout.Popup("Grid Preset", selectedGridIndex, gridSizeOptions);

        int width = level.GridWidth;
        int height = level.GridHeight;

        if (selectedGridIndex < gridSizeOptions.Length - 1)
        {
            ParseGridSize(gridSizeOptions[selectedGridIndex], out width, out height);
        }

        width = EditorGUILayout.IntSlider("Grid Width", width, 2, 8);
        height = EditorGUILayout.IntSlider("Grid Height", height, 2, 8);

        EditorGUILayout.Space();

        // Photo Selection
        EditorGUILayout.LabelField("Photo Selection", EditorStyles.boldLabel);
        
        if (photoDatabase == null)
        {
            EditorGUILayout.HelpBox("PhotoDatabase not found! Please create one at Assets/Resources/Data/PhotoDatabase.asset", MessageType.Warning);
        }
        else if (photoDatabase.Photos.Count == 0)
        {
            EditorGUILayout.HelpBox("No photos in database! Add images to Resources/PuzzleImages and refresh PhotoDatabase.", MessageType.Warning);
        }
        else
        {
            string[] photoNames = new string[photoDatabase.Photos.Count];
            for (int i = 0; i < photoDatabase.Photos.Count; i++)
            {
                photoNames[i] = $"{photoDatabase.Photos[i].ID}: {photoDatabase.Photos[i].PhotoName}";
            }

            int currentIndex = Mathf.Max(0, photoDatabase.Photos.FindIndex(p => p.ID == level.PhotoID));
            int selectedIndex = EditorGUILayout.Popup("Photo", currentIndex, photoNames);
            int photoID = photoDatabase.Photos[selectedIndex].ID;

            // Preview
            if (photoDatabase.Photos[selectedIndex].PhotoSprite != null)
            {
                Texture2D texture = photoDatabase.Photos[selectedIndex].PhotoSprite.texture;
                GUILayout.Label(texture, GUILayout.MaxWidth(200), GUILayout.MaxHeight(200));
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(level, "Modify Level Data");
                level.SetLevelData(levelID, width, height, photoID);
                EditorUtility.SetDirty(level);
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField($"Total Pieces: {width * height}", EditorStyles.helpBox);
    }

    private void ParseGridSize(string gridSize, out int width, out int height)
    {
        string[] parts = gridSize.Split('x');
        if (parts.Length == 2 && int.TryParse(parts[0], out width) && int.TryParse(parts[1], out height))
        {
            return;
        }
        
        // Default fallback if parsing fails
        Debug.LogWarning($"Failed to parse grid size: {gridSize}. Using default 3x3.");
        width = 3;
        height = 3;
    }
}
