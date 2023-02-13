using System;
using UnityEngine;

public class LossController : MonoBehaviour
{
    [SerializeField] private GameObject _lossPanel = default;

    public static event Action RestartGame = delegate { };

    private void OnEnable()
    {
        EnemyCollision.EnemyCollided += OnEnemyCollided;
    }

    void Start()
    {
        _lossPanel.SetActive(false);       
    }

    private void OnEnemyCollided()
    {
        _lossPanel.SetActive(true);
    }

    public void OnRestartButtonClicked()
    {
        _lossPanel.SetActive(false);
        RestartGame();
    }

    private void OnDestroy()
    {
        EnemyCollision.EnemyCollided -= OnEnemyCollided;
    }
}
