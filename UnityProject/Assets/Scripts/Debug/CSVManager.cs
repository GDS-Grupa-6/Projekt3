using System.IO;
using UnityEngine;

public class CSVManager
{
    private static string reportFolderName = "Report";
    private static string reportFileName = "report.csv";
    private static string reportSeparator = ";";
    private static string timeStampHeader = "Play date";
    private static string[] reportHeaders = new string[2] { "Lp.", "Play time" };

    public static void AppendToReport(string[] strings)
    {
        VerifyDirectory();
        VerifyFile();

        using (StreamWriter sw = File.AppendText(GetFilePath()))
        {
            string finaleString = "";
            for (int i = 0; i < strings.Length; i++)
            {
                if (finaleString != "")
                {
                    finaleString += reportSeparator;
                }
                finaleString += strings[i];
            }
            finaleString += reportSeparator + GetTimeStemp();
            sw.WriteLine(finaleString);
        }
    }

    public static void CreateReport()
    {
        VerifyDirectory();
        using (StreamWriter sw = File.CreateText(GetFilePath()))
        {
            string finaleString = "";
            for (int i = 0; i < reportHeaders.Length; i++)
            {
                if (finaleString != "")
                {
                    finaleString += reportSeparator;
                }
                finaleString += reportHeaders[i];
            }
            finaleString += reportSeparator + timeStampHeader;
            sw.WriteLine(finaleString);
        }
    }

    private static void VerifyDirectory()
    {
        string dir = GetDirectoryPath();
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

    private static void VerifyFile()
    {
        string file = GetFilePath();
        if (!File.Exists(file))
        {
            CreateReport();
        }
    }

    private static string GetDirectoryPath()
    {
        return Application.dataPath + "/" + reportFolderName;
    }

    private static string GetFilePath()
    {
        return GetDirectoryPath() + "/" + reportFileName;
    }

    private static string GetTimeStemp()
    {
        return System.DateTime.Now.ToString();
    }
}
