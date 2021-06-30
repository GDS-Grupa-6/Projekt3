using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GhostObject : MonoBehaviour
{
    [Header("Healing options")]
    [SerializeField] private float _healValue = 10f;
    [SerializeField] private bool _destroyAfterDash;
    [Header("Damage options")]
    [SerializeField] private bool _thisObjectCanHitPlayer;
    [SerializeField] private float _damageValue = 10f;
    [SerializeField] private float _bounceForce = 20f;
    [SerializeField] private float _bounceDistance = 4f;

    private PlayerData _playerData;
    private Dash _dash;
    private CharacterController _player;
    private Vector3 _targetPos;
    private CharacterController _characterController;
    private bool _hit;

    private void Start()
    {
        _characterController = FindObjectOfType<CharacterController>();
        _player = FindObjectOfType<CharacterController>();
        _playerData = FindObjectOfType<PlayerData>();
        _dash = FindObjectOfType<Dash>();
    }

    private void Update()
    {
        if (_hit)
        {
            float step = _bounceForce * Time.deltaTime;
            _player.gameObject.transform.position = Vector3.MoveTowards(_player.gameObject.transform.position, _targetPos, step);

            if (Vector3.Distance(_player.gameObject.transform.position, _targetPos) < 0.1)
            {
                _characterController.enabled = true;
                _hit = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_dash.playerDashing)
            {
                _playerData.Heal(_healValue);

                if (_destroyAfterDash)
                {
                    Destroy(this.gameObject);
                }
            }
            else if (!_dash.playerDashing && _thisObjectCanHitPlayer)
            {
                _characterController.enabled = false;
                // animacja
                _playerData.TakeDamage(_damageValue);
                _targetPos = other.transform.position - other.gameObject.transform.forward * _bounceDistance;
                _hit = true;
            }
        }
    }
}
