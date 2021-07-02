using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerData))]
public class MeleLogic : MonoBehaviour
{
    public InputManager inputManager;
    [SerializeField] private CameraSwitch _cameraSwitch;
    [SerializeField] private Slider _comboBar;
    [SerializeField] private TextMeshProUGUI _comboBarText;
    [Header("Attacks settings")]
    [SerializeField] private int _pointsNeededToExtraAttack;
    [SerializeField] private Vector2 _rayCastOffset;
    public MeleStates[] meleStates;

    [HideInInspector] public bool canStartNextSequence;
    [HideInInspector] public MeleStates currentState;

    private int _comboPoints;
    private Animator _animator;
    private Vector3 _rayPos;
    private PlayerData _playerData;

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        _animator = GetComponent<Animator>();
        currentState = meleStates[0];
        canStartNextSequence = true;
        _comboBar.maxValue = _pointsNeededToExtraAttack;
        ResetPoints();
    }

    private void Update()
    {
        if (inputManager.PlayerAttacked() && canStartNextSequence && !_animator.GetBool("ChangePos") && !_playerData.hitTaken)
        {
            canStartNextSequence = false;
            _animator.SetTrigger("Attack");
        }
    }

    public void MeleAttack()
    {
        _rayPos = new Vector3(transform.position.x + _rayCastOffset.x, transform.position.y + _rayCastOffset.y, transform.position.z);
        RaycastHit hit;
        if (Physics.Raycast(_rayPos, transform.forward, out hit, currentState.range))
        {
            if (hit.transform.gameObject.tag == "Boss" ||
                hit.transform.gameObject.tag == "Bush")
            {
                if (hit.transform.gameObject.tag == "Boss")
                {
                    hit.transform.gameObject.GetComponent<BossData>().TakeDamage(currentState.power);
                }
                else if (hit.transform.gameObject.tag == "Bush")
                {
                    hit.transform.gameObject.GetComponent<Bush>().TakeDamage(currentState.power);
                }
                AddPoints();
            }
        }
    }

    private void AddPoints()
    {
        if (_comboPoints + currentState.pointsForAttack <= _pointsNeededToExtraAttack)
        {
            _comboPoints += currentState.pointsForAttack;
        }
        else
        {
            _comboPoints = _pointsNeededToExtraAttack;
        }

        _comboBar.value = _comboPoints;
        _comboBarText.SetText($"{_comboPoints}/{_pointsNeededToExtraAttack}");
    }

    private void ResetPoints()
    {
        _comboPoints = 0;
        _comboBar.value = _comboPoints;
        _comboBarText.SetText($"{_comboPoints}/{_pointsNeededToExtraAttack}");
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
