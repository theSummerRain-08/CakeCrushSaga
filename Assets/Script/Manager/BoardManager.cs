using UnityEngine;

public class BoardManager : MonoBehaviourSingletonPersistent<BoardManager>
{
    public PieceManager[,] pieceGrid;

    [SerializeField] public int width = 5;
    [SerializeField] public int height = 5;
    

    private void Start()
    {
        pieceGrid = new PieceManager[width, height];
    }

    public void AddPieceToGrid(PieceManager piece, Vector2 position)
    {
        int x = (int)position.x;
        int y = (int)position.y;
        pieceGrid[x, y] = piece;
    }

    public void SwapPieces(Vector2 pos1, Vector2 pos2)
    {
        int x1 = (int)pos1.x;
        int y1 = (int)pos1.y;
        int x2 = (int)pos2.x;
        int y2 = (int)pos2.y;

        if (IsValidPosition(x1, y1) && IsValidPosition(x2, y2))
        {
            PieceManager piece1 = pieceGrid[x1, y1];
            PieceManager piece2 = pieceGrid[x2, y2];
            
            pieceGrid[x1, y1] = piece2;
            pieceGrid[x2, y2] = piece1;
            (piece1.transform.position, piece2.transform.position) = (piece2.transform.position, piece1.transform.position);
            
            piece1.SetLocation(x2, y2);
            piece2.SetLocation(x1, y1);
        }
    }

    private bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
}