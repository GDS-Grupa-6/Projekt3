using Raven.Manager;
using Raven.Player;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerStatesManager _playerStatesManager;

    private bool _stop;

    public void Initialization(PlayerStatesManager p_playerStatesManager)
    {
        _playerStatesManager = p_playerStatesManager;
    }

    void Update()
    {
        transform.position += transform.forward * _playerStatesManager.CurrentConfig.BulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        _stop = true;

        if (other.tag == "Enemy")
        {
            other.GetComponentInParent<EnemyManager>().TakeDamage(_playerStatesManager.CurrentConfig.bulletPower);
        }

        Destroy(this.gameObject);
    }
}
