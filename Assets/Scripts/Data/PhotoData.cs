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

    public PhotoData(int id, string name, Sprite sprite)
    {
        this.id = id;
        this.photoName = name;
        this.photoSprite = sprite;
    }
}
