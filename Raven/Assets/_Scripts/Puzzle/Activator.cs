using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        [SerializeField, ShowIf("_activatorType", ActivatorType.Lever)] private Transform _leverPartTransform;
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
        private float _timer;
        private float _partTimer;
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
                _partDestiny = _leverPartTransform.position + _partShift;
                _partStartPos = _leverPartTransform.position;
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
            else if (_closingDoor)
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

            if (_timer <= _openTime)
            {
                _timer += Time.deltaTime;
                float percent = _timer / _openTime;

                _doorTransform.position = _doorStartPos + _doorShift * percent;
            }
            else
            {
                _openingDoor = false;
                _timer = 0;

                if (_stayOpenForAWhile)
                {
                    StayOpen();
                }
                else if (!_stayOpen)
                {
                    _closingDoor = true;
                }
            }
        }

        private void StayOpen()
        {
            if (_timer < _stayOpenTime)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                _timer = 0;
                _openingDoor = false;
                _closingDoor = true;
            }
        }

        private void CloseDoor()
        {
            if (_timer <= _closeTime)
            {
                _timer += Time.deltaTime;
                float percent = _timer / _closeTime;

                _doorTransform.position = _doorDestiny - _doorShift * percent;
            }
            else
            {
                _timer = 0;

                if (_activatorType == ActivatorType.Torch)
                {
                    _fire.SetActive(false);
                }

                _closingDoor = false;
            }
        }

        private void PushPart()
        {
            if (_closingDoor) return;

            if (_partTimer <= _openTime)
            {
                _partTimer += Time.deltaTime;
                float percent = _partTimer / _openTime;

                _leverPartTransform.position = _partStartPos + _partShift * percent;
            }
            else
            {
                _partTimer = 0;
            }
        }

        private void PartToStarPosition()
        {
            if (_partTimer <= _closeTime)
            {
                _partTimer += Time.deltaTime;
                float percent = _partTimer / _closeTime;

                _leverPartTransform.position = _partDestiny - _partShift * percent;
            }
            else
            {
                _partTimer = 0;
            }
        }

        private void OnTriggerEnter(Collider p_other)
        {
            if (_openingDoor) return;

            if (_activatorType == ActivatorType.Torch && p_other.tag == "FireBullet")
            {
                _closingDoor = false;
                _openingDoor = true;
                _fire.SetActive(true);
            }
            else if (_activatorType == ActivatorType.Lever && p_other.tag == "Player")
            {
                _closingDoor = false;
                _openingDoor = true;
            }
        }

        private void OnTriggerStay(Collider p_other)
        {
            if (_openingDoor) return;

            if (_activatorType == ActivatorType.Lever && p_other.tag == "Player")
            {
                _closingDoor = false;
            }

        }

        private void OnTriggerExit(Collider p_collider)
        {
            if (_closeAfterLeavePart)
                if (_activatorType == ActivatorType.Lever && p_collider.tag == "Player")
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

            if (_activatorType == ActivatorType.Lever && _leverPartTransform != null)
            {
                Gizmos.DrawSphere(_leverPartTransform.position + _partShift, _gizmoRadius);
            }
        }
#endif
    }
}