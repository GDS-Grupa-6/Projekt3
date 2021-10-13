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
        [SerializeField] private Animator _playerHud;
        [SerializeField] private Animator _storyPanel;
        [SerializeField] private GameObject _menuCam;

        private InputManager _inputManager;
        private Animator _animator;
        private int _currentPage = 1;

        [Inject]
        public void Construct(InputManager p_inputManager)
        {
            _inputManager = p_inputManager;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _inputManager.CanInput = false;
        }

        private void Update()
        {
            if (_playerHud.GetCurrentAnimatorStateInfo(0).IsTag("End"))
            {           
                _inputManager.CanInput = true;
                this.enabled = false;
            }

            if (_storyPanel.GetCurrentAnimatorStateInfo(0).IsTag("End"))
            {
                _menuCam.SetActive(false);
                _storyPanel.gameObject.SetActive(false);
            }
        }

        public void Button_StartGame()
        {
            _animator.SetTrigger("FadeOut");
            _storyPanel.SetTrigger("FadeIn");
        }

        public void Button_NextPage()
        {
            _storyPanel.SetTrigger("NextPage");
            _currentPage++;

            if (_currentPage == 4)
            {
                _playerHud.SetTrigger("FadeIn");
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        public void Button_ExitGame()
        {
            Application.Quit();
        }
    }
}

