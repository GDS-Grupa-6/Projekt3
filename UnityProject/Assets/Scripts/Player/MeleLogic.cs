using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleLogic : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private CameraSwitch _cameraSwitch;
    [SerializeField] private float _hitPointRadius = 0.1f;
    [SerializeField] private float _maxSphereRayDistance = 0.1f;
    [SerializeField] private LayerMask _hitLayerMask;
    [SerializeField] [Range(1, 10)] private int _maxStack = 4;

    private int _currentStack;

    private void Start()
    {
        _inputManager.inputSystem.Player.Mele.performed += _ => MeleAttack();
        _currentStack = 0;
    }

    private void MeleAttack()
    {
        RaycastHit hit;

        if (_currentStack + 1 <= _maxStack)
        {
            if (Physics.SphereCast(transform.position, _hitPointRadius, transform.forward, out hit, _maxSphereRayDistance, _hitLayerMask, QueryTriggerInteraction.UseGlobal))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, transform.position + transform.forward * _maxSphereRayDistance);
        Gizmos.DrawWireSphere(transform.position + transform.forward * 1, _hitPointRadius);
    }
}
