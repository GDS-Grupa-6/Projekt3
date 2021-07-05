using UnityEngine;

public class BossWepon : MonoBehaviour
{
    [SerializeField] private float _power = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerData playerData = other.GetComponent<PlayerData>();

            if (!playerData.GetComponent<Dash>().playerDashing)
            {
                playerData.TakeDamage(_power);
            }
        }
    }
}
