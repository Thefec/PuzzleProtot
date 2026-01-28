using UnityEngine;

/// <summary>
/// Data class for a single puzzle photo
/// </summary>
[System.Serializable]
public class PhotoData
{
    [SerializeField] private int id;
    [SerializeField] private string photoName;
    [SerializeField] private Sprite photoSprite;

    public int ID => id;
    public string PhotoName => photoName;
    public Sprite PhotoSprite => photoSprite;

    /// <summary>
    /// Constructor for runtime initialization only.
    /// Values set through constructor won't persist in Unity Inspector.
    /// For Inspector configuration, set fields directly in the asset.
    /// </summary>
    public PhotoData(int id, string name, Sprite sprite)
    {
        this.id = id;
        this.photoName = name;
        this.photoSprite = sprite;
    }
}
