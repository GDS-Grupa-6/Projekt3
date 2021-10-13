using Raven.Input;
using Raven.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResetPoint : MonoBehaviour
{
    private InputManager _inputManager;
    private PlayerDataManager _playerDataManager;

    [SerializeField] private int _damage = 10;
    
    private AudioSource _audiosource;

    public Vector3 ResetPosition { get; set; }
    public Quaternion ResetRotation { get; set; }
    public Transform PlayerTransform { get; set; }
    public Animator ResetPanel { get; set; }

    private bool _active;


    [Inject]
    public void Construct(InputManager p_inputManager, PlayerDataManager p_playerDataManager)
    {
        _inputManager = p_inputManager;
        _playerDataManager = p_playerDataManager;
    }

    private void Awake()
    {
        _audiosource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ResetPanel.SetTrigger("FadeIn");
            _inputManager.CanInput = false;
            _playerDataManager.TakeDamage(_damage);
            _audiosource.Play();
            _active = true;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        _active = false;
    }

    private void Update()
    {
        if (PlayerTransform.position == null || !_active)
        {
            return;
        }

        if (ResetPanel.GetCurrentAnimatorStateInfo(0).IsTag("FadeOut"))
        {
            PlayerTransform.position = ResetPosition;
            PlayerTransform.rotation = ResetRotation;
        }
    }
}
