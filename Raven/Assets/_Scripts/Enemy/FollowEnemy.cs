using Raven.Config;
using Raven.Manager;
using System.Collections;
using System.Collections.Generic;
using Raven.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Raven.Enemy
{
    public class FollowEnemy : MonoBehaviour
    {
        private EnemyConfig _enemyConfig;
        private Transform _playerTransform;
        private NavMeshAgent _navMesh;
        private Transform _gfxTransform;
        private CoroutinesManager _coroutinesManager;

        private float _timer;
        private List<Vector3> _gfxTarget = new List<Vector3>();
        private Vector3 _currentGfxTarget;
        private int _lastIndex = 0;
        private bool _dodge;
        private float _chargeTimer;
        private bool _charge = true;

        public void Initialization(EnemyConfig p_enemyConfig, Transform p_player, NavMeshAgent p_navMesh,
            Transform p_gfxTransform, CoroutinesManager p_coroutinesManager, PlayerDataManager p_playerDataManager)
        {
            _coroutinesManager = p_coroutinesManager;
            _gfxTransform = p_gfxTransform;
            _navMesh = p_navMesh;
            _enemyConfig = p_enemyConfig;
            _playerTransform = p_player;
            _navMesh.speed = _enemyConfig.MoveSpeed;

            EnemyExplode enemyExplode = _gfxTransform.gameObject.AddComponent<EnemyExplode>();
            enemyExplode.Mother = this.gameObject;
            enemyExplode.Initialization(_enemyConfig, p_playerDataManager);

            _gfxTarget.Add(new Vector3(-0.00754f, 0.00151f, 0));
            _gfxTarget.Add(new Vector3(0.00754f, 0.00151f, 0));
            _gfxTarget.Add(new Vector3(0, 0.00151f, 0));
            _currentGfxTarget = _gfxTarget[0];
        }

        private void Start()
        {
            _coroutinesManager.StartCoroutine(DodgeWaitCoroutine(), this.gameObject);
        }

        private void Update()
        {
            float distance = Vector3.Distance(transform.position, _playerTransform.position);
            Vector3 lookAtVector3 = _playerTransform.position;
            lookAtVector3.y += 1.62f;

            _gfxTransform.LookAt(lookAtVector3);

            if (distance > 8 || (distance <= 8 && !_charge))
            {
                _navMesh.SetDestination(_playerTransform.position);

                if (_dodge)
                {
                    Dodge();
                }
            }
            else
            {
                if (_charge)
                {
                    Charge();
                }
            }
        }

        private void Dodge()
        {
            if (_timer < 0.5f)
            {
                _timer += Time.deltaTime / 2f;
                _gfxTransform.localPosition = Vector3.Lerp(_gfxTransform.localPosition, _currentGfxTarget, _timer);
            }
            else
            {
                if (_lastIndex + 1 < _gfxTarget.Count)
                {
                    _lastIndex++;
                }
                else
                {
                    _lastIndex = 0;
                    _dodge = false;
                    _coroutinesManager.StartCoroutine(DodgeWaitCoroutine(), this.gameObject);
                }
                _currentGfxTarget = _gfxTarget[_lastIndex];
                _timer = 0;
            }
        }

        private void Charge()
        {
            if (_chargeTimer < _enemyConfig.ChargeTime)
            {
                _chargeTimer += Time.deltaTime;
                _gfxTransform.position += _gfxTransform.forward * _enemyConfig.MoveSpeed * _enemyConfig.SpeedModifier * Time.deltaTime;
            }
            else
            {
                _charge = false;
                _chargeTimer = 0;
                ReturnPosition();
                _coroutinesManager.StartCoroutine(ChargeWaitCoroutine(), this.gameObject);
            }
        }

        private void ReturnPosition()
        {
            transform.position = _gfxTransform.position;
            _gfxTransform.position = _gfxTarget[_gfxTarget.Count - 1];
        }

        private IEnumerator DodgeWaitCoroutine()
        {
            float random = Random.Range(1, _enemyConfig.MaxDodgeWaitTime);
            yield return new WaitForSeconds(random);
            _dodge = true;
        }

        private IEnumerator ChargeWaitCoroutine()
        {
            yield return new WaitForSeconds(_enemyConfig.ChargeWaitTime);
            _charge = true;
        }
    }
}

