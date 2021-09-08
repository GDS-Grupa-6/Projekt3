using Raven.Manager;
using Raven.Player;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerStatesManager _playerStatesManager;

    private float _lifeTimer;

    public void Initialization(PlayerStatesManager p_playerStatesManager)
    {
        _playerStatesManager = p_playerStatesManager;
    }

    void Update()
    {
        transform.position += transform.forward * _playerStatesManager.CurrentConfig.BulletSpeed * Time.deltaTime;

        if (_lifeTimer >= _playerStatesManager.CurrentConfig.BulletLifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectible" || other.tag == "Laver") return;

        if (other.tag == "Enemy")
        {
            other.GetComponentInParent<EnemyController>().TakeDamage(_playerStatesManager.CurrentConfig.bulletPower);
        }

        Destroy(this.gameObject);
    }
}
