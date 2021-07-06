using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _targetFrames = 30;

    private void Awake()
    {
        Application.targetFrameRate = _targetFrames;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
