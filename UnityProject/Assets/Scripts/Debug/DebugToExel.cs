using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class DebugToExel
{
    static DebugToExel()
    {
        EditorApplication.playModeStateChanged += UpdateReport;
    }

    [MenuItem("Debug/Reset report %F1")]
    private static void ResetReport()
    {
        CSVManager.CreateReport();
        Debug.Log("<color=orange>The report has been reset!</color>");
    }

    private static void UpdateReport(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            CSVManager.AppendToReport(new string[] { "1", "666" });
            Debug.Log("<color=green>Report updated!</color>");
        }
    }
}
