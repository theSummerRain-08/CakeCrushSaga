using UnityEngine;
using DG.Tweening;
public class BoardManager : MonoBehaviourSingletonPersistent<BoardManager>
{
    public PieceManager[,] PieceGrid;
    private bool _canSwipe = true;

    [SerializeField] public int width = 5;
    [SerializeField] public int height = 5;

    private void Start()
    {
        PieceGrid = new PieceManager[width, height];
    }
    

    public void AddPieceToGrid(PieceManager piece, Vector2 position)
    {
        int x = (int)position.x;
        int y = (int)position.y;
        PieceGrid[x, y] = piece;
    }

    public void SwapPieces(Vector2 pos1, Vector2 pos2)
    {
        int x1 = (int)pos1.x;
        int y1 = (int)pos1.y;
        int x2 = (int)pos2.x;
        int y2 = (int)pos2.y;

        if (IsValidPosition(x1, y1) && IsValidPosition(x2, y2) && _canSwipe)
        {
            _canSwipe = false;

            PieceManager piece1 = PieceGrid[x1, y1];
            PieceManager piece2 = PieceGrid[x2, y2];

            PieceGrid[x1, y1] = piece2;
            PieceGrid[x2, y2] = piece1;

            piece1.transform.DOMove(piece2.transform.position, 0.5f).OnComplete(() =>
            {
                _canSwipe = true;
                CheckPieceMatch.Instance.CheckMatches(x2, y2);
            });

            piece2.transform.DOMove(piece1.transform.position, 0.5f).OnComplete(() =>
            { 
                CheckPieceMatch.Instance.CheckMatches(x1, y1);
            });

            piece1.SetLocation(x2, y2);
            piece2.SetLocation(x1, y1);
        }
    }
    

    public void DestroyPiece(PieceManager piece)
    {
        Vector2 pos = piece.Location;
        int x = (int)pos.x;
        int y = (int)pos.y;

        PieceGrid[x, y] = null; 
        ObjectPooling.Instance.DeSpawn(piece.gameObject); 
    }

    public PieceManager PieceAtLocation(int x, int y)
    {
        return PieceGrid[x, y];
    }
    private bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
}
