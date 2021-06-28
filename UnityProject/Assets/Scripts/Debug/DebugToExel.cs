using UnityEditor;
using UnityEngine;
using System.Collections;

public class DebugToExel : MonoBehaviour
{
    private int hours;
    private int minutes;
    private int seconds;
    private string gameTime;

#if UNITY_EDITOR

    private void Awake()
    {
        EditorApplication.playModeStateChanged += UpdateReport;
        StartCoroutine(GameTimerCourutine());
    }

    [MenuItem("Debug/Reset report %F1")]
    private static void ResetReport()
    {
        CSVManager.CreateReport();
        Debug.Log("<color=orange>The report has been reset!</color>");
    }

    private void UpdateReport(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            SetGameTime();
            CSVManager.AppendToReport(new string[] { gameTime });
            Debug.Log("<color=green>Report updated!</color>");
        }
    }

    private void SetGameTime()
    {
        StopCoroutine(GameTimerCourutine());
        gameTime = $"{hours}:{minutes}:{seconds}";
    }

    private IEnumerator GameTimerCourutine()
    {
        seconds = 0;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (seconds + 1 == 60)
            {
                if (minutes + 1 == 60)
                {
                    hours++;
                    minutes = 0;
                }
                else
                {
                    minutes++;
                }
                seconds = 0;
            }
            else
            {
                seconds++;
            }
        }
    }
# endif
}
