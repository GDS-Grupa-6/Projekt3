using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MeleLogic : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private CameraSwitch _cameraSwitch;
    [Header("Attacks settings")]
    [SerializeField] private Vector2 _rayCastOffset;
    [SerializeField] private MeleStates[] _meleStates;

    private Animator _animator;
    private MeleStates _currentState;
    private Vector3 _rayPos;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _inputManager.inputSystem.Player.Mele.performed += _ => MeleAttack();
        _currentState = _meleStates[0];
    }

    private void MeleAttack()
    {
        _rayPos = new Vector3(transform.position.x + _rayCastOffset.x, transform.position.y + _rayCastOffset.y, transform.position.z);
        RaycastHit hit;
        if (Physics.Raycast(_rayPos, transform.forward, out hit, _currentState.range))
        {
            Debug.Log(hit.transform.gameObject.name);
        }
    }

    private void OnDrawGizmos()
    {
        _currentState = _meleStates[0];
        _rayPos = new Vector3(transform.position.x + _rayCastOffset.x, transform.position.y + _rayCastOffset.y, transform.position.z);
        Debug.DrawLine(_rayPos, _rayPos + transform.forward * _currentState.range);
    }
}
