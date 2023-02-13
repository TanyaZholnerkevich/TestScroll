using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "PrizeData")]
public class PrizeData : ScriptableObject
{
    [SerializeField] private Sprite _prizeSprite;
    [SerializeField] private string _prizeName;
    [SerializeField] private int _extraPoints;

    public Sprite PrizeSprite => _prizeSprite;
    public string PrizeName => _prizeName;
    public int ExtraPoints => _extraPoints;
}
