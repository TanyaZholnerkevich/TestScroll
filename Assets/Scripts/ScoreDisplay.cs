using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText = default;

    private int _score;

    private void OnEnable()
    {
        ScoreCounter.PlayerPassed += ChangeScore;
        LossController.RestartGame += ResetScore;
        PrizePanelController.PointsReceived += AddPoints;
    }

    void Start()
    {
        _score = 0;
        _scoreText.text = _score.ToString();
    }

    private void ChangeScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
    }

    private void ResetScore()
    {
        _score = 0;
        _scoreText.text = _score.ToString();
    }

    private void AddPoints(int points)
    {
        _score += points;
        _scoreText.text = _score.ToString();
    }

    private void OnDestroy()
    {
        ScoreCounter.PlayerPassed -= ChangeScore;
        LossController.RestartGame -= ResetScore;
        PrizePanelController.PointsReceived -= AddPoints;
    }
}
