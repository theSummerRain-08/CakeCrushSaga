using UnityEngine;

public class BoardCreate : MonoBehaviour
{
    [SerializeField] private GameObject blackBoardPrefab;
    [SerializeField] private GameObject whiteBoardPrefab;
    [SerializeField] private Transform boardHolder;
    [SerializeField] private Transform pieceHolder;
    [SerializeField] private float offset = 122f;
    [SerializeField] private GameObject[] piecePrefabs;

    private void Start()
    {
        GenerateChessboard();
    }

    private void GenerateChessboard()
    {
        int width = BoardManager.Instance.width;
        int height = BoardManager.Instance.height;
        
        float xOffset = (width % 2 == 0) ? offset / 2 : 0f;
        float yOffset = (height % 2 == 0) ? offset / 2 : 0f;

        int centerX = width / 2;
        int centerY = height / 2;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float posX = (x - centerX) * offset + xOffset;
                float posY = (y - centerY) * offset + yOffset;
                Vector2 position = new Vector2(posX, posY);
                
                SpawnBoard(x, y, position);
                SpawnPiece(x, y, position);
            }
        }
    }

    private void SpawnBoard(float x, float y, Vector2 position)
    {
        GameObject boardPiece = (x + y) % 2 == 0 ? whiteBoardPrefab : blackBoardPrefab;
        GameObject newBoardPiece = Instantiate(boardPiece, Vector3.zero, Quaternion.identity, boardHolder);

        RectTransform boardRectTransform = newBoardPiece.GetComponent<RectTransform>();
        if (boardRectTransform != null)
        {
            boardRectTransform.anchoredPosition = position;
        }
    }

    private void SpawnPiece(float x, float y, Vector2 position)
    {
        if (piecePrefabs.Length <= 0) return;

        GameObject randomPiecePrefab = piecePrefabs[Random.Range(0, piecePrefabs.Length)];
        GameObject randomPiece = ObjectPooling.Instance.GetObjectFromPool(randomPiecePrefab, Vector3.zero, Quaternion.identity, pieceHolder);

        RectTransform pieceRectTransform = randomPiece.GetComponent<RectTransform>();
        PieceManager pieceManager = randomPiece.GetComponent<PieceManager>();

        if (pieceRectTransform != null)
        {
            pieceRectTransform.anchoredPosition = position;
        }

        if (pieceManager != null)
        {
            pieceManager.SetLocation(x, y);
            BoardManager.Instance.AddPieceToGrid(pieceManager, new Vector2(x, y));
        }
    }

}
