using UnityEngine;


[CreateAssetMenu(fileName = "NewData", menuName = "ScriptableObjects/PieceData", order = 1)]
public class CandyPieceSO : ScriptableObject
{
    [SerializeField] private CandyPieceType type;
    [SerializeField] private Sprite img;
    
    public CandyPieceType Type => type;
    public Sprite Img => img;
}
