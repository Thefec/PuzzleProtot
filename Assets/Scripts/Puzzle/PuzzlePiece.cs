using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a single puzzle piece
/// </summary>
public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private Image pieceImage;
    [SerializeField] private Image highlightBorder;

    private int currentIndex;
    private int correctIndex;
    private RectTransform rectTransform;
    private PuzzleManager puzzleManager;

    public int CurrentIndex => currentIndex;
    public int CorrectIndex => correctIndex;
    public bool IsInCorrectPosition => currentIndex == correctIndex;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
        if (pieceImage == null)
        {
            pieceImage = GetComponent<Image>();
        }

        if (highlightBorder != null)
        {
            highlightBorder.enabled = false;
        }
    }

    /// <summary>
    /// Initialize puzzle piece with sprite and indices
    /// </summary>
    public void Initialize(Sprite sprite, int currentIdx, int correctIdx, PuzzleManager manager)
    {
        if (pieceImage != null)
        {
            pieceImage.sprite = sprite;
        }
        
        currentIndex = currentIdx;
        correctIndex = correctIdx;
        puzzleManager = manager;
    }

    /// <summary>
    /// Update current index after swap
    /// </summary>
    public void SetCurrentIndex(int index)
    {
        currentIndex = index;
    }

    /// <summary>
    /// Highlight this piece
    /// </summary>
    public void SetHighlight(bool highlighted)
    {
        if (highlightBorder != null)
        {
            highlightBorder.enabled = highlighted;
        }
    }

    /// <summary>
    /// Handle piece click
    /// </summary>
    public void OnPieceClicked()
    {
        if (puzzleManager != null)
        {
            puzzleManager.OnPieceClicked(this);
        }
    }

    /// <summary>
    /// Get rect transform
    /// </summary>
    public RectTransform GetRectTransform()
    {
        return rectTransform;
    }
}
