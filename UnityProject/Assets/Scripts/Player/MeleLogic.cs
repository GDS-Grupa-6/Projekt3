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
    public CameraSwitch cameraSwitch;
    [SerializeField] private Slider _comboBar;
    [SerializeField] private TextMeshProUGUI _comboBarText;
    [Header("Attack point settings")]
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _targetLayers;
    [Header("Attacks settings")]
    [SerializeField] private int _pointsNeededToExtraAttack;
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
        if (inputManager.PlayerAttacked() && canStartNextSequence && !_animator.GetBool("ChangePos") && !_playerData.hitTaken && !cameraSwitch.playerIsInShootPose)
        {
            canStartNextSequence = false;
            _animator.SetTrigger("Attack");
        }
    }

    public void MeleAttack()
    {
        Collider[] hitTargets = Physics.OverlapSphere(_attackPoint.position, _attackRange, _targetLayers);

        if (hitTargets.Length == 1)
        {
            transform.LookAt(new Vector3(hitTargets[0].transform.position.x, transform.position.y, hitTargets[0].transform.position.z));
        }

        foreach (var item in hitTargets)
        {
            if (item.tag == "Boss")
            {
                item.GetComponent<BossData>().TakeDamage(currentState.power);
            }
            else if (item.tag == "Bush")
            {
                item.GetComponent<Bush>().TakeDamage(currentState.power);
            }
            AddPoints();
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
        if (_attackPoint == null)
        {
            return;
        }

        Gizmos.DrawSphere(_attackPoint.position, _attackRange);
    }
#endif

}
