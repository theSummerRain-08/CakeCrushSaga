using UnityEngine;
using UnityEngine.Serialization;

public class BoardCreate : MonoBehaviour
{
    [SerializeField] private int width = 5;  
    [SerializeField] private int height = 5; 
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
                
                GameObject boardPiece = (x + y) % 2 == 0 ? whiteBoardPrefab : blackBoardPrefab;
                GameObject newBoardPiece = Instantiate(boardPiece, Vector3.zero, Quaternion.identity, boardHolder);
                
                RectTransform boardRectTransform = newBoardPiece.GetComponent<RectTransform>();
                if (boardRectTransform != null)
                {
                    boardRectTransform.anchoredPosition = new Vector2(posX, posY);
                }
                
                if (piecePrefabs.Length > 0)
                {
                    GameObject randomPiece = Instantiate(piecePrefabs[Random.Range(0, piecePrefabs.Length)], Vector3.zero, Quaternion.identity, pieceHolder);
                    
                    RectTransform pieceRectTransform = randomPiece.GetComponent<RectTransform>();
                    if (pieceRectTransform != null)
                    {
                        pieceRectTransform.anchoredPosition = new Vector2(posX, posY);
                    }
                }
            }
        }
    }
}
