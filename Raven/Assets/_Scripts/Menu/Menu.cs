using System;
using JetBrains.Annotations;
using Raven.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Raven.Core
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private CanvasGroup _storyPanel;
        [SerializeField] private TextMeshProUGUI _storyText;
        [SerializeField] private CanvasGroup _playerHud;
        [SerializeField] private GameObject _menuCam;
        [Space(10)]
        [SerializeField] private float _fadeTime = 3;

        private CanvasGroup _menuCanvasGroup;
        private InputManager _inputManager;

        private bool _start;
        private float _time;
        private float _storyTime;
        private bool _storyClose;
        private float _hudTime;

        [Inject]
        public void Construct(InputManager p_inputManager)
        {
            _inputManager = p_inputManager;
        }

        private void Awake()
        {
            _menuCanvasGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            if (_start)
            {
                StartGame();
            }
        }

        public void Button_StartGame()
        {
            _startButton.interactable = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _start = true;
        }

        private void StartGame()
        {
            _time += Time.deltaTime;
            float step = _time / (_fadeTime / 2);
            _menuCanvasGroup.alpha = Mathf.Lerp(1, 0, step);

            if (_menuCanvasGroup.alpha <= 0.2f)
            {
                _storyTime += Time.deltaTime;
                float step1 = _storyTime / _fadeTime;
                _storyPanel.alpha = Mathf.Lerp(0, 1, step1);
            }

            if (_storyPanel.alpha == 1 && !_storyClose)
            {
                _storyClose = true;
                _storyTime = 0;
            }

            if (_storyClose)
            {
                _storyTime += Time.deltaTime;
                float step1 = _storyTime / _fadeTime;
                _storyPanel.alpha = Mathf.Lerp(1, 0, step1);

                if (_storyPanel.alpha < 0.5f)
                {
                    _hudTime += Time.deltaTime;
                    float step2 = _hudTime / _fadeTime;
                    _playerHud.alpha = Mathf.Lerp(0, 1, step2);
                }

                if (_storyPanel.alpha < 0.2f)
                {
                    _menuCam.SetActive(false);
                }

                if (_playerHud.alpha > 0.4)
                {
                    _inputManager.CanInput = true;
                }

                if (_playerHud.alpha == 1)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}

