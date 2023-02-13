using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject _panelPrefab = default;
    [SerializeField] private Button _lootButton = default;
    [SerializeField] private Button _exitButton = default;
    [SerializeField] private Button _leftButton = default;
    [SerializeField] private Button _rightButton = default;

    private GameObject _panel;

    public static event Action ExitButtonClicked = delegate { };

    private void OnEnable()
    {
        PrizeBoxCollision.PrizeBoxCollided += OnPrizeBoxCollided;
        EnemyCollision.EnemyCollided += OnEnemyCollided;
        LossController.RestartGame += OnGameRestarted;
    }

    private void Start()
    {
        _lootButton.gameObject.SetActive(false);
        _exitButton.gameObject.SetActive(false);       
    }

    private void OnPrizeBoxCollided()
    {
        _lootButton.gameObject.SetActive(true);
        _lootButton.interactable = true;
        ActivateManageButtons(false);
    }

    private void OnEnemyCollided()
    {
        ActivateManageButtons(false);
    }

    public void OnLootButtonClicked()
    {
        ShowPanel();

        _lootButton.interactable = false;
        _exitButton.interactable = false;

        _exitButton.gameObject.SetActive(true);

        ScrollingController.PanelStoped += OnPanelStoped;
    }

    private void ShowPanel()
    {
        _panel = Instantiate(_panelPrefab, transform, false);
        _panel.SetActive(true);
    }

    private void OnPanelStoped()
    {
        _exitButton.interactable = true;
    }

    public void OnExitButtonClicked()
    {
        _lootButton.gameObject.SetActive(false);
        _exitButton.gameObject.SetActive(false);

        ActivateManageButtons(true);

        ScrollingController.PanelStoped -= OnPanelStoped;
        Destroy(_panel);
        ExitButtonClicked();
    }

    private void OnGameRestarted()
    {
        ActivateManageButtons(true);
    }

    private void ActivateManageButtons(bool interactable)
    {
        _leftButton.interactable = interactable;
        _rightButton.interactable = interactable;
    }

    private void OnDestroy()
    {
        PrizeBoxCollision.PrizeBoxCollided -= OnPrizeBoxCollided;
        EnemyCollision.EnemyCollided -= OnEnemyCollided;
        LossController.RestartGame += OnGameRestarted;
    }
}
