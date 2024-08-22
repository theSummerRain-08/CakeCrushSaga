using UnityEngine;
using UnityEngine.Serialization;

public class BoardCreate : MonoBehaviour
{
    [SerializeField] private int width = 5;  
    [SerializeField] private int height = 5; 
    [SerializeField] private GameObject blackBoardPrefab; 
    [SerializeField] private GameObject writeBoardPrefab;
    [SerializeField] private float offset = 122f;

    private void Start()
    {
        GenerateChessboard();
    }

    private void GenerateChessboard()
    {
        float xOffset = (width % 2 == 0) ? offset/2 : 0f;
        float yOffset = (height % 2 == 0) ? offset/2 : 0f;
        
        int centerX = width / 2;
        int centerY = height / 2;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float posX = (x - centerX) * offset + xOffset;
                float posY = (y - centerY) * offset + yOffset;

                GameObject newPiece = null;
                newPiece = Instantiate((x + y) %2 == 0 ? writeBoardPrefab : blackBoardPrefab, Vector3.zero, Quaternion.identity, this.transform);

                RectTransform rectTransform = newPiece.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector2(posX, posY);
                }
            }
        }
    }
}