using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MeleLogic : MonoBehaviour
{
    public InputManager inputManager;

    [SerializeField] private CameraSwitch _cameraSwitch;
    [Header("Attacks settings")]
    [SerializeField] private Vector2 _rayCastOffset;
    public MeleStates[] meleStates;

    [HideInInspector] public bool canStartNextSequence;
    [HideInInspector] public MeleStates currentState;
    [HideInInspector] public int comboPoints;

    private Animator _animator;
    private Vector3 _rayPos;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        currentState = meleStates[0];
        canStartNextSequence = true;
        comboPoints = 0;
    }

    private void Update()
    {
        if (inputManager.PlayerAttacked() && canStartNextSequence)
        {
            canStartNextSequence = false;
            _animator.SetTrigger("Attack");
        }
    }

    private void MeleAttack()
    {
        _rayPos = new Vector3(transform.position.x + _rayCastOffset.x, transform.position.y + _rayCastOffset.y, transform.position.z);
        RaycastHit hit;
        if (Physics.Raycast(_rayPos, transform.forward, out hit, currentState.range))
        {
            Debug.Log(hit.transform.gameObject.name);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (currentState == null)
        {
            currentState = meleStates[0];
        }

        _rayPos = new Vector3(transform.position.x + _rayCastOffset.x, transform.position.y + _rayCastOffset.y, transform.position.z);
        Debug.DrawLine(_rayPos, _rayPos + transform.forward * currentState.range);
    }
#endif

}
