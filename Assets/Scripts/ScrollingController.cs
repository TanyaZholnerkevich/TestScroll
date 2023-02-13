using System;
using UnityEngine;

public class ScrollingController : MonoBehaviour
{
    private const float PanelSizeIncrease = 1.3f;


    [SerializeField] private GameObject _scrollPanel = default;
    [SerializeField] private int _panelCount = default;
    [SerializeField] private int _offsetX = default;
    [SerializeField] private Transform _finishPos = default;
    [SerializeField] private GameObject _prizePanel = default;
    [SerializeField] private float _speed = 0.7f;
    [SerializeField] private float _speedDecrease = 0.0045f;

    private GameObject[] _panels;
    private GameObject _lastPanel;
    private Vector2[] _panelsPos;

    public static event Action PanelStoped = delegate { };

    void Start()
    {
        _panels = new GameObject[_panelCount];
        _panelsPos = new Vector2[_panelCount];
        SpawnPanels();
    }

    private void SpawnPanels()
    {
        for (int i = 0; i < _panelCount; i++)
        {
            _panels[i] = Instantiate(_scrollPanel, transform, false);
            // what is "false"?

            if(i == 0)
            {
                continue;
            }

            var previousX = _panels[i - 1].transform.localPosition.x;
            var panelSizeX = _scrollPanel.GetComponent<RectTransform>().sizeDelta.x;
            var posY = _panels[i].transform.localPosition.y;

            _panels[i].transform.localPosition = new Vector2(previousX + panelSizeX + _offsetX, posY);
        }
        _lastPanel = _panels[_panelCount - 1];
    }

    private void Update()
    {
        if(_speed < 0)
        {
            _speed = 0;
            GetPanelsPositions();
            var index = GetNearestPanel();
            ShowPrizePanel(index);
            PanelStoped();
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < _panelCount; i++)
        {
            if(_panels[i].transform.localPosition.x < _finishPos.localPosition.x)
            {
                var lastPos = _lastPanel.transform.localPosition.x;
                var panelSizeX = _scrollPanel.GetComponent<RectTransform>().sizeDelta.x;
                _panels[i].transform.localPosition = new Vector2(lastPos + _offsetX + panelSizeX, _panels[i].transform.localPosition.y);
                _lastPanel = _panels[i];
            }
        }

        MovePanels();
    }

    private void MovePanels()
    {
        if(_speed > 0)
        {
            for(int i = 0; i < _panelCount; i++)
            {
                _panels[i].transform.Translate(_speed * (-1), 0, 0);
            }
            _speed -= _speedDecrease;
        }
        
    }

    private void GetPanelsPositions()
    {
        for (int i = 0; i < _panelCount; i++)
        {
            _panelsPos[i] = _panels[i].transform.localPosition;
        }
    }

    private int GetNearestPanel()
    {
        //?
        var minDistance = 300f;
        var index = 0;
        for (int i = 0; i < _panelCount; i++)
        {
            var distance = Math.Abs(_panelsPos[i].x);
            if(distance < minDistance)
            {
                minDistance = distance;
                index = i;
            }
        }

        return index;
    }

    private void ShowPrizePanel(int index)
    {
        _panels[index].GetComponent<RectTransform>().sizeDelta *= PanelSizeIncrease;
        var prizePanel = Instantiate(_prizePanel, transform);
        var posX = _panels[index].transform.localPosition.x;
        var panelSizeY = _panels[index].GetComponent<RectTransform>().sizeDelta.y;
        prizePanel.transform.localPosition = new Vector2(posX, panelSizeY);
    }
}
