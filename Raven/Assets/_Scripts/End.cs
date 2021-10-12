using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    [SerializeField] private PauseEndPanel _pauseEndPanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _pauseEndPanel.SetCoreTextActive();
        }
    }
}
