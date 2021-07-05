using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private float _power;

    private void Awake()
    {
        DesactiveWave();
    }

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

    public void ActiveWave()
    {
        this.gameObject.SetActive(true);
    }

    public void DesactiveWave()
    {
        this.gameObject.SetActive(false);
    }
}
