using Raven.Config;
using Raven.Core;
using Raven.Core.Interfaces;
using Raven.Manager;
using Raven.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Raven.Enemy
{
    public class Kamikaze : IEnemy
    {
        private EnemyConfig _enemyConfig;
        private Transform _playerTransform;
        private NavMeshAgent _navMesh;
        private Transform _gfxTransform;
        private CoroutinesManager _coroutinesManager;
        private GameObject _enemy;

        private float _timer;
        private List<Vector3> _gfxTarget = new List<Vector3>();
        private Vector3 _currentGfxTarget;
        private int _lastIndex = 0;
        private bool _dodge;
        private float _chargeTimer;
        private bool _charge = true;
        private AudioManager _audioManager;
        private AudioClipConditions[] _audioClipConditions;
        private AudioSource[] _audioSource;

        public Kamikaze(EnemyConfig p_enemyConfig, Transform p_player, NavMeshAgent p_navMesh, Transform p_gfxTransform, CoroutinesManager p_coroutinesManager, 
            PlayerDataManager p_playerDataManager, GameObject p_enemy, AudioManager p_audioManager, AudioClipConditions[] p_audioClipConditions, AudioSource[] p_audioSource)
        {
            _audioSource = p_audioSource;
            _audioManager = p_audioManager;
            _audioClipConditions = p_audioClipConditions;
            _enemy = p_enemy;
            _coroutinesManager = p_coroutinesManager;
            _gfxTransform = p_gfxTransform;
            _navMesh = p_navMesh;
            _enemyConfig = p_enemyConfig;
            _playerTransform = p_player;
            _navMesh.speed = _enemyConfig.MoveSpeed;

            _gfxTarget.Add(new Vector3(-0.00754f, 0.00151f, 0));
            _gfxTarget.Add(new Vector3(0.00754f, 0.00151f, 0));
            _gfxTarget.Add(new Vector3(0, 0.00151f, 0));
            _currentGfxTarget = _gfxTarget[0];

            _coroutinesManager.StartCoroutine(DodgeWaitCoroutine(), _enemy);
        }

        public void Behaviour()
        {
            float distance = Vector3.Distance(_enemy.transform.position, _playerTransform.position);
            Vector3 lookAtVector3 = _playerTransform.position;
            lookAtVector3.y += 1.62f;

            _gfxTransform.LookAt(lookAtVector3);

            if (distance > _enemyConfig.ChargeDistance || (distance <= _enemyConfig.ChargeDistance && !_charge))
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
                    _coroutinesManager.StartCoroutine(DodgeWaitCoroutine(), _enemy);
                }
                _currentGfxTarget = _gfxTarget[_lastIndex];
                _timer = 0;
            }
        }

        private void Charge()
        {
            if (_audioSource[0].clip != _audioManager.GetCurrenAudioClipConditions(_audioClipConditions, AudioNames.Charge).AudioClip)
            {
                _audioManager.PlaySound(_audioManager.GetCurrenAudioClipConditions(_audioClipConditions, AudioNames.Charge), _audioSource[0]);
            }

            if (_chargeTimer < _enemyConfig.ChargeTime)
            {
                _chargeTimer += Time.deltaTime;
                _gfxTransform.position += _gfxTransform.forward * _enemyConfig.MoveSpeed * _enemyConfig.ChargeSpeedModifier * Time.deltaTime;
            }
            else
            {
                _audioManager.PlaySound(_audioManager.GetCurrenAudioClipConditions(_audioClipConditions, AudioNames.Idle), _audioSource[0]);
                _charge = false;
                _chargeTimer = 0;
                GfxReturnPosition();
                _coroutinesManager.StartCoroutine(ChargeWaitCoroutine(), _enemy);
            }
        }

        private void GfxReturnPosition()
        {
            _enemy.transform.position = _gfxTransform.position;
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

