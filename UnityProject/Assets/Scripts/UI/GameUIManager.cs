using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private GameObject _pausePanel;

    private bool _pausePanelIsActive;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _pausePanel.SetActive(false);
    }

    void Start()
    {
        _inputManager.inputSystem.Panels.OnOffPausePanel.performed += _ => OnOffPausePanel();
    }

    private void OnOffPausePanel()
    {
        if (!_pausePanelIsActive)
        {
            _pausePanelIsActive = true;
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
            _pausePanelIsActive = false;
        }
    }

    public void ResetScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Reasume()
    {
        OnOffPausePanel();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
