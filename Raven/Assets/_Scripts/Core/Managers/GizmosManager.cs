using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raven.Manager
{
    public class GizmosManager : MonoBehaviour
    {
        private bool _drawSphere;
        private Vector3 _center;
        private float _radius;
        private Color _color;

        private void DrawSphere(Vector3 p_center, float p_radius, Color p_color)
        {
            _radius = p_radius;
            _center = p_center;
            _color = p_color;
            _drawSphere = true;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_drawSphere)
            {
                Gizmos.color = _color;
                Gizmos.DrawWireSphere(_center, _radius);
            }
        }
#endif
    }
}

