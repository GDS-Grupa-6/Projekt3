using Raven.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;

public class PauseEndPanel : MonoBehaviour
{
    [SerializeField] private GameObject _coreText;
    [SerializeField] private GameObject _pauseText;
    [SerializeField] private GameObject _reastartButton;
    [SerializeField] private GameObject _menu;

    private CanvasGroup _canvasGroup;
    private InputManager _inputManager;

    private bool _panelActive;

    [Inject]
    public void Construct(InputManager p_inputManager)
    {
        _inputManager = p_inputManager;

        _canvasGroup = GetComponent<CanvasGroup>();

        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _pauseText.SetActive(false);
        _coreText.SetActive(false);
        _reastartButton.SetActive(true);
    }

    private void Update()
    {
        if (_inputManager.EscTrigerred() && !_panelActive)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _panelActive = true;
            _pauseText.SetActive(true);
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            Time.timeScale = 0;
        }
    }

    public void BUTTON_Reastart()
    {
        Time.timeScale = 1;

        if (!_menu.activeSelf)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _pauseText.SetActive(false); 
        _panelActive = false;
    }

    public void BUTTON_Exit()
    {
        Application.Quit();
    }

    public void SetCoreTextActive()
    {
        _reastartButton.SetActive(false);
        _coreText.SetActive(true);
    }
}
