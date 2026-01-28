using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom editor for PhotoDatabase to add refresh button
/// </summary>
[CustomEditor(typeof(PhotoDatabase))]
public class PhotoDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PhotoDatabase database = (PhotoDatabase)target;

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Click 'Refresh Images' to load all sprites from Resources/PuzzleImages folder.", MessageType.Info);
        
        if (GUILayout.Button("Refresh Images", GUILayout.Height(30)))
        {
            database.RefreshPhotos();
            EditorUtility.SetDirty(database);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Total Photos: " + database.Photos.Count, EditorStyles.boldLabel);
    }
}
