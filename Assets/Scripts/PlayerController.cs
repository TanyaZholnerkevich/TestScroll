using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab = default;
    [SerializeField] private Transform _startPlayerPos = default;
    [SerializeField] private Transform _leftBoarder = default;
    [SerializeField] private Transform _rightBoarder = default;
    [SerializeField] private float _speed = default;

    public static float PlayerPosY;

    private GameObject _player;

    private void OnEnable()
    {
        LossController.RestartGame += MovePlayerOnStart;
    }

    private void Start()
    {
        PlayerPosY = _startPlayerPos.position.y;
        _player = Instantiate(_playerPrefab);
        _player.transform.position = new Vector2(_startPlayerPos.position.x, _startPlayerPos.position.y);
    }

    public void MovePlayerLeft()
    {
        if(_player.transform.position.x > _leftBoarder.position.x)
        {
            _player.transform.Translate(-_speed, 0, 0);
        }
    }

    public void MovePlayerRight()
    {
        if (_player.transform.position.x < _rightBoarder.position.x)
        {
            _player.transform.Translate(_speed, 0, 0);
        }
    }

    private void MovePlayerOnStart()
    {
        _player.transform.position = new Vector2(_startPlayerPos.position.x, _startPlayerPos.position.y);
    }

    private void OnDestroy()
    {
        LossController.RestartGame -= MovePlayerOnStart;
    }
}
