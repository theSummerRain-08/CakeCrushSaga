using UnityEngine;
using UnityEngine.EventSystems;

public class PieceSwipe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 startPos;
    private Vector2 endPos;
    private PieceManager pieceManager;

    private void Start()
    {
        pieceManager = GetComponent<PieceManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Không cần xử lý trong drag nếu bạn chỉ quan tâm đến bắt đầu và kết thúc của swipe
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endPos = eventData.position;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        Vector2 swipe = endPos - startPos;
        float x = Mathf.Abs(swipe.x);
        float y = Mathf.Abs(swipe.y);

        if (x > y)
        {
            if (swipe.x > 0)
            {
                OnSwipeRight();
            }
            else
            {
                OnSwipeLeft();
            }
        }
        else
        {
            if (swipe.y > 0)
            {
                OnSwipeUp();
            }
            else
            {
                OnSwipeDown();
            }
        }
    }

    private void OnSwipeRight()
    {
        Vector2 targetLocation = pieceManager.Location + Vector2.right;
        SwapPiece(targetLocation);
    }

    private void OnSwipeLeft()
    {
        Vector2 targetLocation = pieceManager.Location + Vector2.left;
        SwapPiece(targetLocation);
    }

    private void OnSwipeUp()
    {
        Vector2 targetLocation = pieceManager.Location + Vector2.up;
        SwapPiece(targetLocation);
    }

    private void OnSwipeDown()
    {
        Vector2 targetLocation = pieceManager.Location + Vector2.down;
        SwapPiece(targetLocation);
    }

    private void SwapPiece(Vector2 targetLocation)
    {
        // Tìm piece tại targetLocation và hoán đổi vị trí với piece hiện tại
        BoardManager.Instance.SwapPieces(pieceManager.Location, targetLocation);
    }
}
