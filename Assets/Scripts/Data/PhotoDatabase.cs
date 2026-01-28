using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ScriptableObject database for managing puzzle photos
/// </summary>
[CreateAssetMenu(fileName = "PhotoDatabase", menuName = "Puzzle/Photo Database")]
public class PhotoDatabase : ScriptableObject
{
    [SerializeField] private List<PhotoData> photos = new List<PhotoData>();

    public List<PhotoData> Photos => photos;

    /// <summary>
    /// Get photo by ID
    /// </summary>
    public PhotoData GetPhotoByID(int id)
    {
        return photos.Find(p => p.ID == id);
    }

    /// <summary>
    /// Add photo to database
    /// </summary>
    public void AddPhoto(PhotoData photo)
    {
        if (!photos.Exists(p => p.ID == photo.ID))
        {
            photos.Add(photo);
        }
    }

    /// <summary>
    /// Clear all photos
    /// </summary>
    public void ClearPhotos()
    {
        photos.Clear();
    }

    /// <summary>
    /// Refresh photos from Resources/PuzzleImages folder
    /// </summary>
    public void RefreshPhotos()
    {
        photos.Clear();
        Sprite[] sprites = Resources.LoadAll<Sprite>("PuzzleImages");
        
        for (int i = 0; i < sprites.Length; i++)
        {
            photos.Add(new PhotoData(i, sprites[i].name, sprites[i]));
        }

        Debug.Log($"Loaded {photos.Count} photos from Resources/PuzzleImages");
    }
}
