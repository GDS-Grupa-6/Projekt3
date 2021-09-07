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
        [SerializeField, ShowIf("_activatorType", ActivatorType.Lever)] private bool _closeAfterLeavePart;
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

        private bool _closingDoor;
        private bool _openingDoor;
        private float _openTimer;
        private float _closeTimer;
        private float _pushPartTimer;
        private float _backPartTimer;
        private Vector3 _doorDestiny;
        private Vector3 _doorStartPos;
        private Vector3 _partDestiny;
        private Vector3 _partStartPos;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;

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

                if (_activatorType == ActivatorType.Lever)
                {
                    PushPart();
                }
            }

            if (_closingDoor)
            {
                CloseDoor();

                if (_activatorType == ActivatorType.Lever)
                {
                    PartToStarPosition();
                }
            }
        }

        public void OpenDoor()
        {
            if (_closingDoor) return;

            if (_openTimer <= _openTime)
            {
                _openTimer += Time.deltaTime;
                float percent = _openTimer / _openTime;

                _doorTransform.position = _doorStartPos + _doorShift * percent;
            }
            else
            {
                _openingDoor = false;
                _openTimer = 0;

                if (_activatorType != ActivatorType.Lever)
                {
                    if (_stayOpenForAWhile)
                    {
                        StayOpenWhile();
                    }
                    else if (!_stayOpen)
                    {
                        _closingDoor = true;
                    }
                }
            }
        }

        private void StayOpenWhile()
        {
            if (_openTimer < _stayOpenTime)
            {
                _openTimer += Time.deltaTime;
            }
            else
            {
                _openTimer = 0;
                _openingDoor = false;
                _closingDoor = true;
            }
        }

        private void CloseDoor()
        {
            if (_closeTimer <= _closeTime)
            {
                _closeTimer += Time.deltaTime;
                float percent = _closeTimer / _closeTime;

                _doorTransform.position = _doorDestiny - _doorShift * percent;
            }
            else
            {
                _closingDoor = false;

                _closeTimer = 0;

                if (_activatorType == ActivatorType.Torch)
                {
                    _fire.SetActive(false);
                }
            }
        }

        private void PushPart()
        {
            if (_closingDoor) return;

            if (_pushPartTimer <= _openTime)
            {
                _pushPartTimer += Time.deltaTime;
                float percent = _pushPartTimer / _openTime;

                transform.position = _partStartPos + _partShift * percent;
            }
            else
            {
                _pushPartTimer = 0;
            }
        }

        private void PartToStarPosition()
        {
            if (_backPartTimer <= _closeTime)
            {
                _backPartTimer += Time.deltaTime;
                float percent = _backPartTimer / _closeTime;

                transform.position = _partDestiny - _partShift * percent;
            }
            else
            {
                _backPartTimer = 0;
            }
        }

        private void OnTriggerEnter(Collider p_other)
        {
            if (_openingDoor) return;

            if (_activatorType == ActivatorType.Torch && p_other.tag == "FireBullet")
            {
                _openingDoor = true;
                _fire.SetActive(true);
            }
            else if (_activatorType == ActivatorType.Lever && p_other.tag == "Player")
            {
                _openingDoor = true;
            }
        }

        /*    private void OnTriggerStay(Collider p_other)
            {
                if (_openingDoor) return;

                if (_activatorType == ActivatorType.Lever && p_other.tag == "Player")
                {
                    _closingDoor = false;
                }
            }*/

        private void OnTriggerExit(Collider p_collider)
        {
            if (_closeAfterLeavePart && _activatorType == ActivatorType.Lever && p_collider.tag == "Player")
            {
                _openingDoor = false;
                _closingDoor = true;
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