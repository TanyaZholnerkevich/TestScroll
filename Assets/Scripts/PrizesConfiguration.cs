using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "PrizesConfig")]
public class PrizesConfiguration : ScriptableObject
{
    [SerializeField] private PrizeData[] _prizes;
    [SerializeField] private float[] _probability;

    public PrizeData[] PrizeDatas => _prizes;
    public float[] Probability => _probability;
}
