using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrizePanelController : MonoBehaviour
{
    [SerializeField] private PrizesConfiguration _prizesConfiguration;
    [SerializeField] private Image _prizeImage;
    [SerializeField] private TextMeshProUGUI _prizeName;
    [SerializeField] private TextMeshProUGUI _prizeProbability;

    private PrizeData _prizeData;
    private float _probability;

    public static event Action<int> PointsReceived = delegate { };

    private void Start()
    {
        var index = GetPrize();
        _prizeData = _prizesConfiguration.PrizeDatas[index];
        _probability = _prizesConfiguration.Probability[index];

        _prizeImage.sprite = _prizeData.PrizeSprite;
        _prizeName.text = _prizeData.PrizeName;
        _prizeProbability.text = _probability.ToString() + "%";

        var points = _prizeData.ExtraPoints;
        PointsReceived(points);
    }

    private int GetPrize()
    {
        var prob = _prizesConfiguration.Probability;
        var leftValue = 0f;
        var rightValue = prob[0];
        var index = 0;
        float rnd = UnityEngine.Random.Range(1, 100);

        for(int i = 0; i < prob.Length; i++)
        {
            if(i != 0)
            {
                leftValue += prob[i - 1];
                rightValue += prob[i];
            }

            if(rnd > leftValue && rnd <= rightValue)
            {
                index = i;
            }

        }

        return index;
    }
}
