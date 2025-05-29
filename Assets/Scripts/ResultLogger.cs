using System.IO;
using UnityEngine;

public static class ResultLogger
{
    private static string path = "result_log.csv";
    private static bool initialized = false;

    public static void LogAverage(int fps, float averageAccuracy)
    {
        if (!initialized)
        {
            File.WriteAllText(path, "FPS,AverageAccuracy(%)\n");
            initialized = true;
        }

        string line = $"{fps},{averageAccuracy:F1}";
        File.AppendAllText(path, line + "\n");
        Debug.Log("Logged average: " + line);
    }
}
