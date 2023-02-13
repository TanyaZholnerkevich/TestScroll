using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HindranceController : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab = default;
    [SerializeField] private GameObject _prizeBoxPrefab = default;
    [SerializeField] private int _enemyCount = default;
    [SerializeField] private Transform _leftBoarder = default;
    [SerializeField] private Transform _rightBoarder = default;
    [SerializeField] private Transform _startPos = default;
    [SerializeField] private Transform _finishPos = default;

    [SerializeField] private float _startSpeed = default;
    [SerializeField] private float _offsetY = default;

    private GameObject[] _hindrances;
    private GameObject _lastHindrance;
    private float _speed;

    private void OnEnable()
    {
        EnemyCollision.EnemyCollided += OnHindranceCollided;
        PrizeBoxCollision.PrizeBoxCollided += OnHindranceCollided;
        ButtonsController.ExitButtonClicked += OnExitButtonClicked;
        LossController.RestartGame += MoveHindrancesOnStart;
    }

    void Start()
    {
        _hindrances = new GameObject[_enemyCount + 1];
        SpawnHindrances();
        _speed = _startSpeed;
    }

    private void OnHindranceCollided()
    {
        _speed = 0f;
    }

    private void OnExitButtonClicked()
    {
        _speed = _startSpeed;
    }

    private void SpawnHindrances()
    {
        for(int i = 0; i < _enemyCount; i++)
        {
            var posX = Random.Range(_leftBoarder.position.x, _rightBoarder.position.x);
            _hindrances[i] = Instantiate(_enemyPrefab, _startPos);

            _hindrances[i].transform.position = new Vector2(posX, _startPos.position.y + i* _offsetY);
        }
        var prizePosX = Random.Range(_leftBoarder.position.x, _rightBoarder.position.x);
        _hindrances[_enemyCount] = Instantiate(_prizeBoxPrefab, _startPos);
        _hindrances[_enemyCount].transform.position = new Vector2(prizePosX, _startPos.position.y + (_enemyCount * _offsetY));

        _lastHindrance = _hindrances[_enemyCount];
    }

    private void FixedUpdate()
    {
        MoveEnemies();
    }

    private void Update()
    {
        for (int i = 0; i < _enemyCount + 1; i++)
        {
            if(_hindrances[i].transform.position.y < _finishPos.position.y)
            {
                var posX = Random.Range(_leftBoarder.position.x, _rightBoarder.position.x);
                _hindrances[i].transform.position = new Vector2(posX, _lastHindrance.transform.position.y + _offsetY);
                _hindrances[i].SetActive(true);
                _lastHindrance = _hindrances[i];
            }
        }
    }

    private void MoveEnemies()
    {
        for(int i = 0; i < _enemyCount + 1; i++)
        {
            _hindrances[i].transform.Translate(0, -_speed, 0);
        }
    }

    private void MoveHindrancesOnStart()
    {
        for(int i = 0; i < _enemyCount + 1; i++)
        {
            Destroy(_hindrances[i]);
        }
        SpawnHindrances();
        _speed = _startSpeed;
    }

    private void OnDestroy()
    {
        EnemyCollision.EnemyCollided -= OnHindranceCollided;
        PrizeBoxCollision.PrizeBoxCollided -= OnHindranceCollided;
        ButtonsController.ExitButtonClicked -= OnExitButtonClicked;
        LossController.RestartGame -= MoveHindrancesOnStart;
    }
}
