using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Main puzzle manager - handles game logic, swapping, and win conditions
/// </summary>
public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private PuzzleGrid puzzleGrid;
    [SerializeField] private PuzzleUI puzzleUI;

    private LevelData currentLevel;
    private PuzzlePiece selectedPiece;
    private bool isSwapping;
    private bool isPuzzleComplete;
    private float gameTime;
    private bool isTimerRunning;

    private void Start()
    {
        LoadLevel();
        StartGame();
    }

    private void Update()
    {
        if (isTimerRunning && !isPuzzleComplete)
        {
            gameTime += Time.deltaTime;
            if (puzzleUI != null)
            {
                puzzleUI.UpdateTimer(gameTime);
            }
        }
    }

    /// <summary>
    /// Load current level from GameManager
    /// </summary>
    private void LoadLevel()
    {
        currentLevel = GameManager.Instance.CurrentLevel;
        
        if (currentLevel == null)
        {
            Debug.LogError("No level selected!");
            return;
        }

        Sprite photoSprite = GameManager.Instance.GetPhotoSprite(currentLevel.PhotoID);
        
        if (photoSprite == null)
        {
            Debug.LogError($"Photo with ID {currentLevel.PhotoID} not found!");
            return;
        }

        // Create puzzle grid
        puzzleGrid.CreateGrid(currentLevel.GridWidth, currentLevel.GridHeight, photoSprite, this);
    }

    /// <summary>
    /// Start the game
    /// </summary>
    private void StartGame()
    {
        ShufflePuzzle();
        gameTime = 0f;
        isTimerRunning = true;
        isPuzzleComplete = false;

        if (puzzleUI != null)
        {
            puzzleUI.ShowGameUI();
        }
    }

    /// <summary>
    /// Shuffle puzzle pieces ensuring solvability
    /// </summary>
    private void ShufflePuzzle()
    {
        List<PuzzlePiece> pieces = puzzleGrid.Pieces;
        int shuffleCount = pieces.Count * 3; // Number of random swaps

        for (int i = 0; i < shuffleCount; i++)
        {
            int index1 = Random.Range(0, pieces.Count);
            int index2 = Random.Range(0, pieces.Count);

            if (index1 != index2)
            {
                SwapPiecesImmediate(pieces[index1], pieces[index2]);
            }
        }

        // Ensure puzzle is not already solved
        if (CheckWinCondition())
        {
            ShufflePuzzle();
        }
    }

    /// <summary>
    /// Handle piece click
    /// </summary>
    public void OnPieceClicked(PuzzlePiece piece)
    {
        if (isSwapping || isPuzzleComplete)
            return;

        if (selectedPiece == null)
        {
            // First piece selected
            selectedPiece = piece;
            selectedPiece.SetHighlight(true);
        }
        else if (selectedPiece == piece)
        {
            // Deselect same piece
            selectedPiece.SetHighlight(false);
            selectedPiece = null;
        }
        else
        {
            // Second piece selected - swap
            StartCoroutine(SwapPieces(selectedPiece, piece));
        }
    }

    /// <summary>
    /// Swap two pieces with animation
    /// </summary>
    private IEnumerator SwapPieces(PuzzlePiece piece1, PuzzlePiece piece2)
    {
        isSwapping = true;
        piece1.SetHighlight(false);

        // Get positions
        Vector3 pos1 = piece1.GetRectTransform().anchoredPosition;
        Vector3 pos2 = piece2.GetRectTransform().anchoredPosition;

        // Animate swap
        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            piece1.GetRectTransform().anchoredPosition = Vector3.Lerp(pos1, pos2, t);
            piece2.GetRectTransform().anchoredPosition = Vector3.Lerp(pos2, pos1, t);

            yield return null;
        }

        // Ensure final positions
        piece1.GetRectTransform().anchoredPosition = pos2;
        piece2.GetRectTransform().anchoredPosition = pos1;

        // Update indices
        int temp = piece1.CurrentIndex;
        piece1.SetCurrentIndex(piece2.CurrentIndex);
        piece2.SetCurrentIndex(temp);

        // Swap in grid list
        List<PuzzlePiece> pieces = puzzleGrid.Pieces;
        int index1 = pieces.IndexOf(piece1);
        int index2 = pieces.IndexOf(piece2);
        pieces[index1] = piece2;
        pieces[index2] = piece1;

        selectedPiece = null;
        isSwapping = false;

        // Check win condition
        if (CheckWinCondition())
        {
            OnPuzzleComplete();
        }
    }

    /// <summary>
    /// Swap pieces immediately (for shuffling)
    /// </summary>
    private void SwapPiecesImmediate(PuzzlePiece piece1, PuzzlePiece piece2)
    {
        // Get positions
        Vector3 pos1 = piece1.GetRectTransform().anchoredPosition;
        Vector3 pos2 = piece2.GetRectTransform().anchoredPosition;

        // Swap positions
        piece1.GetRectTransform().anchoredPosition = pos2;
        piece2.GetRectTransform().anchoredPosition = pos1;

        // Update indices
        int temp = piece1.CurrentIndex;
        piece1.SetCurrentIndex(piece2.CurrentIndex);
        piece2.SetCurrentIndex(temp);

        // Swap in grid list
        List<PuzzlePiece> pieces = puzzleGrid.Pieces;
        int index1 = pieces.IndexOf(piece1);
        int index2 = pieces.IndexOf(piece2);
        pieces[index1] = piece2;
        pieces[index2] = piece1;
    }

    /// <summary>
    /// Check if puzzle is solved
    /// </summary>
    private bool CheckWinCondition()
    {
        return puzzleGrid.Pieces.All(piece => piece.IsInCorrectPosition);
    }

    /// <summary>
    /// Handle puzzle completion
    /// </summary>
    private void OnPuzzleComplete()
    {
        isPuzzleComplete = true;
        isTimerRunning = false;

        bool isNewRecord = GameManager.Instance.SaveBestTime(currentLevel.LevelID, gameTime);
        float bestTime = GameManager.Instance.GetBestTime(currentLevel.LevelID);

        if (puzzleUI != null)
        {
            puzzleUI.ShowCompletionPanel(gameTime, bestTime, isNewRecord);
        }

        Debug.Log($"Puzzle completed in {gameTime:F2} seconds!");
    }

    /// <summary>
    /// Restart current level
    /// </summary>
    public void RestartLevel()
    {
        SceneLoader.Instance.ReloadScene();
    }

    /// <summary>
    /// Load next level
    /// </summary>
    public void LoadNextLevel()
    {
        int nextLevelID = currentLevel.LevelID + 1;
        LevelData nextLevel = GameManager.Instance.LevelDatabase.GetLevelByID(nextLevelID);

        if (nextLevel != null)
        {
            GameManager.Instance.SetCurrentLevel(nextLevelID);
            SceneLoader.Instance.LoadPuzzleGame();
        }
        else
        {
            // No more levels, return to level map
            SceneLoader.Instance.LoadLevelMap();
        }
    }

    /// <summary>
    /// Return to level map
    /// </summary>
    public void ReturnToLevelMap()
    {
        SceneLoader.Instance.LoadLevelMap();
    }
}
