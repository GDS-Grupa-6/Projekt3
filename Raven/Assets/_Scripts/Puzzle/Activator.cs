using System.Collections;
using NaughtyAttributes;
using UnityEngine;

namespace Raven.Puzzle
{
    public enum ActivatorType { Torch, Lever }

    [RequireComponent(typeof(Collider))]
    public class Activator : MonoBehaviour
    {
        [SerializeField] private ActivatorType _activatorType;
        [Space]
        [SerializeField, ShowIf("_activatorType", ActivatorType.Lever)] private Vector3 _partShift;
        [Space]
        [SerializeField, HideIf("_activatorType", ActivatorType.Lever)] private GameObject _fire;
        [Space]
        [SerializeField] private Transform _doorTransform;
        [SerializeField] private Vector3 _doorShift;
        [Space]
        [SerializeField] private float _openTime;
        [SerializeField] private float _closeTime;
        [SerializeField] private bool _stayOpenForAWhile;
        [SerializeField, ShowIf("_stayOpenForAWhile")] private float _stayOpenTime;
        [SerializeField, HideIf("_stayOpenForAWhile")] private bool _stayOpen;
        [Space]
        [InfoBox("Transforms must be assigned", EInfoBoxType.Warning)]
        [SerializeField, Range(0.1f, 2), Tooltip("It's radius of gizmos spheres")] private float _gizmoRadius = 1f;
        [Space]
        [SerializeField, HideIf("_activatorType", ActivatorType.Lever)] private AudioSource _torchAudioSource;
        [SerializeField] private AudioClip _openSound;
        [SerializeField] private AudioClip _closeSound;
        [SerializeField] private AudioSource _doorAudioSource;

        private bool _closingDoor;
        private bool _openingDoor;
        private bool _doorAreOpened;
        private bool _doorAreClosed = true;
        private bool _play;

        private AudioSource _audioSource;

        private float _timer;
        private float _stayOpenTimer;

        private Vector3 _doorDestiny;
        private Vector3 _doorStartPos;
        private Vector3 _partDestiny;
        private Vector3 _partStartPos;

        private float _percent;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
            _audioSource = GetComponent<AudioSource>();

            if (_activatorType == ActivatorType.Torch)
            {
                _fire.SetActive(false);
            }
            else
            {
                _partDestiny = transform.position + _partShift;
                _partStartPos = transform.position;
            }

            _doorDestiny = _doorTransform.position + _doorShift;
            _doorStartPos = _doorTransform.position;
        }

        private void Update()
        {
            if (_openingDoor)
            {
                OpenDoor();
            }

            if (_closingDoor)
            {
                CloseDoor();
            }
        }

        public void OpenDoor()
        {
            if (_closingDoor) return;

            _doorAreClosed = false;

            if (_timer <= _openTime && !_doorAreOpened)
            {
                _percent = _timer / _openTime;
                _timer += Time.deltaTime;
                
                _doorTransform.position = _doorStartPos + _doorShift * _percent;

                if (_activatorType == ActivatorType.Lever)
                {
                    transform.position = _partStartPos + _partShift * _percent;
                }
            }
            else
            {
                _doorAreOpened = true;
                _timer = _closeTime - _percent * _closeTime;

                if (_stayOpenForAWhile)
                {
                    StayOpenWhile();
                    _doorAudioSource.Stop();
                }
                else if (!_stayOpen && _activatorType == ActivatorType.Torch)
                {
                    _openingDoor = false;
                    _doorAreOpened = false;
                    _closingDoor = true;
                }
                else
                {
                    _openingDoor = false;
                    _doorAudioSource.Stop();
                }
            }
        }

        private void StayOpenWhile()
        {
            if (_stayOpenTimer < _stayOpenTime)
            {
                _stayOpenTimer += Time.deltaTime;
            }
            else
            {
                _openingDoor = false;
                _doorAreOpened = false;
                _closingDoor = true;
                _stayOpenTimer = 0;
                PlaySound(_closeSound);
            }
        }

        private void CloseDoor()
        {
            _doorAreOpened = false;

            if (_timer <= _closeTime)
            {
                _percent = _timer / _closeTime;
                _timer += Time.deltaTime;

                _doorTransform.position = _doorDestiny - _doorShift * _percent;

                if (_activatorType == ActivatorType.Lever)
                {
                    transform.position = _partDestiny - _partShift * _percent;
                }
            }
            else
            {
                _closingDoor = false;
                _doorAreClosed = true;

                _timer = _openTime - _percent * _openTime;

                if (_activatorType == ActivatorType.Torch)
                {
                    _fire.SetActive(false);
                    _torchAudioSource.Stop();
                }

                _doorAudioSource.Stop();
            }
        }

        private void OnTriggerEnter(Collider p_other)
        {
            if (_openingDoor) return;

            if (_activatorType == ActivatorType.Torch && p_other.tag == "FireBullet")
            {
                _openingDoor = true;
                _fire.SetActive(true);
                _torchAudioSource.Play();
                PlaySound(_openSound);
            }
            else if (_activatorType == ActivatorType.Lever && p_other.tag == "Player")
            {
                _closingDoor = false;

                if (!_doorAreClosed)
                {
                    _timer = _openTime - _percent * _openTime;
                }
                else
                {
                    _timer = 0;
                }
                
                _openingDoor = true;
                PlaySound(_openSound);
            }
        }

        private void OnTriggerExit(Collider p_collider)
        {
            if (!_stayOpen && !_closingDoor && _activatorType == ActivatorType.Lever && p_collider.tag == "Player")
            {
                if (_stayOpenForAWhile)
                {
                    StartCoroutine(CloseDelayCoroutine());
                    return;
                }

                _openingDoor = false;
                _timer = _closeTime - _percent * _closeTime;
                _closingDoor = true;
                PlaySound(_closeSound);
            }
        }

        private IEnumerator CloseDelayCoroutine()
        {
            _openingDoor = false;
            yield return new WaitForSeconds(_stayOpenTime);
            _timer = _closeTime - _percent * _closeTime;
            _closingDoor = true;
            PlaySound(_closeSound);
        }

        private void PlaySound(AudioClip p_audioClip)
        {
            if (_play)
            {
                _doorAudioSource.Stop();
                _doorAudioSource.clip = p_audioClip;
                _doorAudioSource.Play();
                _play = false;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            if (_doorTransform != null)
            {
                Gizmos.DrawSphere(_doorTransform.position + _doorShift, _gizmoRadius);
            }

            if (_activatorType == ActivatorType.Lever)
            {
                Gizmos.DrawSphere(transform.position + _partShift, _gizmoRadius);
            }
        }
#endif
    }
}