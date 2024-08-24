using UnityEngine;
using UnityEngine.EventSystems;

public class PieceManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public CandyPieceSO pieceData;
    public Vector2 Location /*{ get; private set; }*/;

    public void SetLocation(float x, float y)
    {
        this.Location = new Vector2(x, y);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(this.pieceData.Type);
    }
}