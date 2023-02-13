using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private bool _isPassed;
    private float _playerPosY;

    public static event Action PlayerPassed = delegate { };

    private void Start()
    {
        _isPassed = false;
        _playerPosY = PlayerController.PlayerPosY;
    }

    private void Update()
    {
        var enemyPos = transform.position.y;
        if (enemyPos < _playerPosY && !_isPassed)
        {
            _isPassed = true;
            PlayerPassed();
        }

        if(enemyPos > _playerPosY && _isPassed)
        {
            _isPassed = false;
        }
    }
}
