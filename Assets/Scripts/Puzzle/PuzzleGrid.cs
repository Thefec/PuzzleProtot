using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Manages the puzzle grid layout and piece creation
/// </summary>
public class PuzzleGrid : MonoBehaviour
{
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private RectTransform gridContainer;
    [SerializeField] private float spacing = 5f;

    private List<PuzzlePiece> pieces = new List<PuzzlePiece>();
    private GridLayoutGroup gridLayout;

    public List<PuzzlePiece> Pieces => pieces;

    private void Awake()
    {
        if (gridContainer == null)
        {
            gridContainer = GetComponent<RectTransform>();
        }

        gridLayout = gridContainer.GetComponent<GridLayoutGroup>();
        if (gridLayout == null)
        {
            gridLayout = gridContainer.gameObject.AddComponent<GridLayoutGroup>();
        }
    }

    /// <summary>
    /// Create puzzle grid based on level data
    /// </summary>
    public void CreateGrid(int width, int height, Sprite fullImage, PuzzleManager manager)
    {
        ClearGrid();

        // Setup grid layout
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = width;
        gridLayout.spacing = new Vector2(spacing, spacing);
        gridLayout.childAlignment = TextAnchor.MiddleCenter;

        // Calculate cell size to fit container
        float containerWidth = gridContainer.rect.width - (spacing * (width - 1));
        float containerHeight = gridContainer.rect.height - (spacing * (height - 1));
        float cellWidth = containerWidth / width;
        float cellHeight = containerHeight / height;
        
        gridLayout.cellSize = new Vector2(cellWidth, cellHeight);

        // Create puzzle pieces
        Texture2D texture = fullImage.texture;
        int pieceWidth = texture.width / width;
        int pieceHeight = texture.height / height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;
                
                // Create sprite for this piece
                Rect rect = new Rect(x * pieceWidth, (height - 1 - y) * pieceHeight, pieceWidth, pieceHeight);
                Sprite pieceSprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
                pieceSprite.name = $"Piece_{index}";

                // Instantiate piece
                GameObject pieceObj = Instantiate(piecePrefab, gridContainer);
                pieceObj.name = $"Piece_{index}";

                PuzzlePiece piece = pieceObj.GetComponent<PuzzlePiece>();
                if (piece != null)
                {
                    piece.Initialize(pieceSprite, index, index, manager);
                    pieces.Add(piece);
                }

                // Add button component for clicking
                Button button = pieceObj.GetComponent<Button>();
                if (button == null)
                {
                    button = pieceObj.AddComponent<Button>();
                }
                button.onClick.AddListener(() => piece.OnPieceClicked());
            }
        }

        Debug.Log($"Created grid: {width}x{height} = {pieces.Count} pieces");
    }

    /// <summary>
    /// Clear all pieces from grid
    /// </summary>
    public void ClearGrid()
    {
        foreach (var piece in pieces)
        {
            if (piece != null)
            {
                Destroy(piece.gameObject);
            }
        }
        pieces.Clear();
    }

    /// <summary>
    /// Get piece at specific index
    /// </summary>
    public PuzzlePiece GetPieceAtIndex(int index)
    {
        if (index >= 0 && index < pieces.Count)
        {
            return pieces[index];
        }
        return null;
    }
}
