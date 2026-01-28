using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom editor for LevelDatabase to add level management buttons
/// </summary>
[CustomEditor(typeof(LevelDatabase))]
public class LevelDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelDatabase database = (LevelDatabase)target;

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Click 'Add New Level' to create a new level data asset.", MessageType.Info);
        
        if (GUILayout.Button("Add New Level", GUILayout.Height(30)))
        {
            CreateNewLevel(database);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Total Levels: " + database.GetLevelCount(), EditorStyles.boldLabel);
    }

    private void CreateNewLevel(LevelDatabase database)
    {
        int nextID = database.GetLevelCount() + 1;
        
        LevelData newLevel = ScriptableObject.CreateInstance<LevelData>();
        newLevel.SetLevelData(nextID, 3, 3, 0);

        string path = $"Assets/Resources/Data/Levels/Level_{nextID}.asset";
        AssetDatabase.CreateAsset(newLevel, path);
        
        database.AddLevel(newLevel);
        
        EditorUtility.SetDirty(database);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorGUIUtility.PingObject(newLevel);
        Selection.activeObject = newLevel;

        Debug.Log($"Created new level with ID {nextID}");
    }
}
