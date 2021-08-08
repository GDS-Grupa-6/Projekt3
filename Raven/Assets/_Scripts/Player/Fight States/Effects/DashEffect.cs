using Raven.Config;
using UnityEngine;

namespace Raven.Player
{
    public class DashEffect : MonoBehaviour
    {
        [SerializeField, Tooltip("Set same value in config")] private float _debugRadius = 4f;

        private PlayerFightStateConfig _config;

        private float _timer;

        public void Initialize(PlayerFightStateConfig p_config)
        {
            _config = p_config;
        }

        public void Update()
        {
            _timer += Time.deltaTime;
            RaycastHit[] colliders = Physics.SphereCastAll(transform.position, _config.EffectRadius,transform.forward);

            if (_timer > _config.EffectTime)
            {
                Destroy(this.gameObject);
            }

            if (colliders.Length > 0)
            {
                foreach (var VARIABLE in colliders)
                {
                    Debug.Log($"Fire effect hit {VARIABLE.collider.gameObject.name}"); //TODO add hit function
                }
            }
        }

#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _debugRadius);
        }
#endif
    }
}

