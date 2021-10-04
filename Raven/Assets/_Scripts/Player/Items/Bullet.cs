using Raven.Config;
using Raven.Manager;
using Raven.Player;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerStatesManager _playerStatesManager;
    private PlayerDataManager _playerDataManager;
    private EnemyConfig _enemyConfig;
    private float _speed;
    private GameObject _thisEnemy;

    private float _lifeTimer;

    public void Initialization(PlayerStatesManager p_playerStatesManager)
    {
        _playerStatesManager = p_playerStatesManager;
        _speed = _playerStatesManager.CurrentConfig.BulletSpeed;
    }

    public void Initialization(float p_bulletSpeed, PlayerDataManager p_playerDataManager, EnemyConfig p_enemyConfig, GameObject p_thisEnemy)
    {
        _thisEnemy = p_thisEnemy;
        _enemyConfig = p_enemyConfig;
        _playerDataManager = p_playerDataManager;
        _speed = p_bulletSpeed;
    }

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;

        if (_lifeTimer >= 4)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectible" || other.tag == "Laver") return;

        if (_playerStatesManager != null && other.tag == "Enemy")
        {
            other.GetComponentInParent<EnemyController>().TakeDamage(_playerStatesManager.CurrentConfig.bulletPower);
        }
        else if (other.tag == "Player")
        {
            _playerDataManager.TakeDamage(_enemyConfig.Power);
        }
        else if (other.tag == "Enemy" && other.gameObject != _thisEnemy)
        {
            other.GetComponentInParent<EnemyController>().TakeDamage(_enemyConfig.Power);
        }

        if (_playerStatesManager == null && other.gameObject == _thisEnemy) return;

        Destroy(this.gameObject);
    }
}
