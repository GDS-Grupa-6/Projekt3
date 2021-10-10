using Raven.Config;
using Raven.Manager;
using Raven.Player;
using System;
using System.Linq;
using UnityEngine;

namespace Raven.Core
{
    public enum AudioNames { Idle, Inpact, Charge, Shoot, Explosion, Dead, Walk, Dash, FireDash }

    public class AudioManager : IDisposable
    {
        private AudioReferences _references;
        private PlayerMovementManager _playerMovementManager;
        private PlayerStatesManager _playerStatesManager;
        private PlayerDataManager _playerDataManager;

        public AudioManager(AudioReferences p_references, PlayerMovementManager p_playerMovementManager, PlayerStatesManager p_playerStatesManager, PlayerDataManager p_playerDataManager)
        {
            _playerDataManager = p_playerDataManager;
            _playerStatesManager = p_playerStatesManager;
            _playerMovementManager = p_playerMovementManager;
            _references = p_references;

            _playerMovementManager.OnMove += PlayWalk;
            _playerMovementManager.OnDashStart += PlayDash;
            _playerStatesManager.OnShoot += PlayShoot;
            _playerDataManager.OnDead += PlayDead;
            _playerDataManager.OnTakeDamage += PlayDamage;
        }

        public void Dispose()
        {
            _playerMovementManager.OnMove -= PlayWalk;
            _playerMovementManager.OnDashStart -= PlayDash;
            _playerStatesManager.OnShoot -= PlayShoot;
            _playerDataManager.OnDead -= PlayDead;
            _playerDataManager.OnTakeDamage -= PlayDamage;
        }

        public void PlaySound(AudioClipConditions p_clipOptions, AudioSource p_audioSource)
        {
            p_audioSource.Stop();
            p_audioSource.loop = p_clipOptions.Loop;
            p_audioSource.clip = p_clipOptions.AudioClip;
            p_audioSource.volume = p_clipOptions.Volume;
            p_audioSource.Play();
        }

        public AudioClipConditions GetCurrenAudioClipConditions(AudioClipConditions[] p_audioClips, AudioNames p_clipName)
        {
            return p_audioClips.First(x => x.AudioName == p_clipName);
        }

        private void PlayWalk(float p_speed)
        {
            if (p_speed > 0 && !_playerMovementManager.Dash)
            {
                if (_references.PlayerMoveSource.clip != GetCurrenAudioClipConditions(_references.AudioClipConditions,AudioNames.Walk).AudioClip)
                {
                    PlaySound(GetCurrenAudioClipConditions(_references.AudioClipConditions, AudioNames.Walk), _references.PlayerMoveSource);
                }
            }
            else
            {
                if (_references.PlayerMoveSource.clip == GetCurrenAudioClipConditions(_references.AudioClipConditions, AudioNames.Walk).AudioClip)
                {
                    _references.PlayerMoveSource.Stop();
                    _references.PlayerMoveSource.clip = null;
                }
            }
        }

        private void PlayDash(PlayerStateName p_playerStateName)
        {
            if (!_playerMovementManager.Dash)
            {
                return;
            }

            if (p_playerStateName == PlayerStateName.Fire)
            {
                if (_references.PlayerMoveSource.clip != GetCurrenAudioClipConditions(_references.AudioClipConditions, AudioNames.FireDash).AudioClip)
                {
                    PlaySound(GetCurrenAudioClipConditions(_references.AudioClipConditions, AudioNames.FireDash), _references.PlayerMoveSource);
                }
            }
            else
            {
                if (_references.PlayerMoveSource.clip != GetCurrenAudioClipConditions(_references.AudioClipConditions, AudioNames.Dash).AudioClip)
                {
                    PlaySound(GetCurrenAudioClipConditions(_references.AudioClipConditions, AudioNames.Dash), _references.PlayerMoveSource);
                }
            }
        }

        private void PlayShoot()
        {
            PlaySound(GetCurrenAudioClipConditions(_references.AudioClipConditions, AudioNames.Shoot), _references.PlayShootSource);
        }

        private void PlayDamage()
        {
            PlaySound(GetCurrenAudioClipConditions(_references.AudioClipConditions, AudioNames.Inpact), _references.PlayerEffectSource);
        }

        private void PlayDead()
        {
            PlaySound(GetCurrenAudioClipConditions(_references.AudioClipConditions, AudioNames.Dead), _references.PlayerEffectSource);
        }
    }

    [Serializable]
    public class AudioClipConditions
    {
        public AudioNames AudioName;
        public AudioClip AudioClip;
        [Range(0, 1)] public float Volume = 1;
        public bool Loop;
    }
}
