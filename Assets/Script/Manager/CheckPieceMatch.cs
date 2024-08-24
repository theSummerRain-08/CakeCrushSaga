using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPieceMatch : MonoBehaviourSingletonPersistent<CheckPieceMatch>
{
    public List<PieceManager> piecesToDestroy;

    public void CheckMatches(int x, int y)
    {
        PieceManager checkPiece = BoardManager.Instance.PieceGrid[x, y];
        piecesToDestroy = new List<PieceManager>();

        // Check horizontal match
        CheckHorizontalMatch(x, y, checkPiece);

        // Check vertical match
        CheckVerticalMatch(x, y, checkPiece);

        // Handle matches
        HandleMatches();
    }

    private void CheckHorizontalMatch(int x, int y, PieceManager checkPiece)
    {
        int width = BoardManager.Instance.width;

        // Check right side
        for (int i = x; i < width; i++)
        {
            if (BoardManager.Instance.PieceAtLocation(i, y).pieceData.Type == checkPiece.pieceData.Type)
            {
                piecesToDestroy.Add(BoardManager.Instance.PieceAtLocation(i, y));
            }
            else
            {
                break;
            }
        }

        // Check left side
        for (int i = x - 1; i >= 0; i--)
        {
            if (BoardManager.Instance.PieceAtLocation(i, y).pieceData.Type == checkPiece.pieceData.Type)
            {
                piecesToDestroy.Add(BoardManager.Instance.PieceAtLocation(i, y));
            }
            else
            {
                break;
            }
        }
    }

    private void CheckVerticalMatch(int x, int y, PieceManager checkPiece)
    {
        int height = BoardManager.Instance.height;

        // Check upwards
        for (int i = y + 1; i < height; i++)
        {
            if (BoardManager.Instance.PieceAtLocation(x, i).pieceData.Type == checkPiece.pieceData.Type)
            {
                piecesToDestroy.Add(BoardManager.Instance.PieceAtLocation(x, i));
            }
            else
            {
                break;
            }
        }

        // Check downwards
        for (int i = y - 1; i >= 0; i--)
        {
            if (BoardManager.Instance.PieceAtLocation(x, i).pieceData.Type == checkPiece.pieceData.Type)
            {
                piecesToDestroy.Add(BoardManager.Instance.PieceAtLocation(x, i));
            }
            else
            {
                break;
            }
        }
    }

    private void HandleMatches()
    {
        int matchCount = piecesToDestroy.Count;

        if (matchCount >= 3)
        {
            if (matchCount == 3)
            {
                Debug.Log("Destroy 3");
            }
            else if (matchCount == 4)
            {
                Debug.Log("Spawn special");
            }
            else if (matchCount >= 5)
            {
                Debug.Log("Spawn bomb");
            }

            // Destroy matched pieces
            foreach (PieceManager piece in piecesToDestroy)
            {
                BoardManager.Instance.DestroyPiece(piece);
            }

            piecesToDestroy.Clear(); // Clear the list after handling matches
        }
        else
        {
            piecesToDestroy.Clear(); // Clear the list if there are no sufficient matches
        }
    }
}
