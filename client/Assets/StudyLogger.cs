using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StudyLogger : MonoBehaviour
{
    public static StudyLogger Instance { get; private set; }

    [Header("Logger Settings")]
    [SerializeField] private string logFolderName = "StudyLogs";
    [SerializeField] private string filePrefix = "study";
    [SerializeField] private bool autoFlush = true;
    [SerializeField] private bool logToConsole = true;
    [SerializeField] private LookDurationDetector lookDurationDetector;

    private string sessionId;
    private string participantId = "unknown";
    private string filePath;
    private readonly List<string> buffer = new List<string>();

    private bool initialized = false;

    public static bool IsReady => Instance != null && Instance.initialized;
    public static string CurrentSessionId => IsReady ? Instance.sessionId : "";
    public static string CurrentParticipantId => IsReady ? Instance.participantId : "";
    public static string CurrentFilePath => IsReady ? Instance.filePath : "";
    public GameManager gameManager;

    private const string Header =
        "session_id;participant_id;timestamp_utc;player_turn;event_type;attacker_id;object_id;time_start_ms;time_end_ms;value;success;notes;looking_at_card";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeIfNeeded();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Flush();
        }
    }

    private void OnApplicationQuit()
    {
        Flush();
    }

    public void InitializeIfNeeded()
    {
        if (initialized)
            return;

        string folderPath = Path.Combine(Application.persistentDataPath, logFolderName);
        Directory.CreateDirectory(folderPath);

        int nextStudyNumber = GetNextStudyNumber(folderPath, filePrefix);

        sessionId = nextStudyNumber.ToString(CultureInfo.InvariantCulture);
        // filePath = Path.Combine(folderPath, $"{filePrefix}_{nextStudyNumber}.csv");
        string sceneName = SceneManager.GetActiveScene().name;
        filePath = Path.Combine(folderPath, $"{filePrefix}_{nextStudyNumber}_{sceneName}.csv");

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, Header + Environment.NewLine, Encoding.UTF8);
        }

        initialized = true;

        if (logToConsole)
        {
            Debug.Log($"[StudyLogger] Initialized. File: {filePath}");
        }
    }

    public void SetParticipantIdInternal(string newParticipantId)
    {
        participantId = string.IsNullOrWhiteSpace(newParticipantId) ? "unknown" : newParticipantId.Trim();
    }

    public void Flush()
    {
        if (!initialized || buffer.Count == 0)
            return;

        try
        {
            File.AppendAllLines(filePath, buffer, Encoding.UTF8);
            buffer.Clear();
        }
        catch (Exception ex)
        {
            Debug.LogError($"[StudyLogger] Flush failed: {ex}");
        }
    }

    private void WriteRow(
        int turn,
        string eventType,
        string trialId,
        string objectId,
        double? timeStart,
        double? timeEnd,
        double? value,
        bool? success,
        string notes)
    {
        InitializeIfNeeded();

        string timestampUtc = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);

        string line = string.Join(";",
            Escape(sessionId),
            Escape(participantId),
            Escape(timestampUtc),
            Escape(turn.ToString()),
            Escape(eventType),
            Escape(trialId),
            Escape(objectId),
            timeStart.HasValue ? timeStart.Value.ToString(CultureInfo.InvariantCulture) : "",
            timeEnd.HasValue ? timeEnd.Value.ToString(CultureInfo.InvariantCulture) : "",
            value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : "",
            success.HasValue ? success.Value.ToString().ToLowerInvariant() : "",
            Escape(notes),
            Escape(lookDurationDetector.GetCurrentLookCardID())
        );

        buffer.Add(line);

        if (logToConsole)
        {
            Debug.Log($"[StudyLogger] {line}");
        }

        if (autoFlush)
        {
            Flush();
        }
    }

    private static string Escape(string input)
    {
        if (string.IsNullOrEmpty(input))
            return "";

        string escaped = input.Replace("\"", "\"\"");
        return $"\"{escaped}\"";
    }

    private static int GetNextStudyNumber(string folderPath, string prefix)
    {
        string[] files = Directory.GetFiles(folderPath, $"{prefix}_*.csv");
        int maxNumber = 0;

        string pattern = $"^{Regex.Escape(prefix)}_(\\d+)(?:_.*)?\\.csv$";
        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            Match match = Regex.Match(fileName, pattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                if (int.TryParse(match.Groups[1].Value, NumberStyles.None, CultureInfo.InvariantCulture, out int number))
                {
                    if (number > maxNumber)
                    {
                        maxNumber = number;
                    }
                }
            }
        }

        return maxNumber + 1;
    }

    public static void SetParticipantId(string newParticipantId)
    {
        EnsureInstanceExists();
        Instance.SetParticipantIdInternal(newParticipantId);
    }

    public static void LogEvent(
        string eventType,
        string trialId = "",
        string objectId = "",
        double? value = null,
        bool? success = null,
        string notes = "")
    {
        EnsureInstanceExists();
        if (Instance.gameManager == null)
        {
            Debug.LogError("[StudyLogger] GameManager is not assigned in the Inspector.");
            return;
        }
        Instance.WriteRow(Instance.gameManager.GetPlayerTurn(), eventType, trialId, objectId, null, null, value, success, notes);
    }

    public static void LogDuration(
        string eventType,
        double timeStart,
        double timeEnd,
        string trialId = "",
        string objectId = "",
        double? value = null,
        bool? success = null,
        string notes = "")
    {
        EnsureInstanceExists();
        if (Instance.gameManager == null)
        {
            Debug.LogError("[StudyLogger] GameManager is not assigned in the Inspector.");
            return;
        }
        Instance.WriteRow(Instance.gameManager.GetPlayerTurn(), eventType, trialId, objectId, timeStart, timeEnd, value, success, notes);
    }

    public static void LogTrialStart(string trialId, string notes = "")
    {
        LogEvent("trial_start", trialId: trialId, notes: notes);
    }

    public static void LogTrialEnd(string trialId, int durationMs, bool success, string notes = "")
    {
        LogDuration("trial_end", durationMs,0d, trialId: trialId, success: success, notes: notes);
    }

    public static void SaveNow()
    {
        if (Instance != null)
        {
            Instance.Flush();
        }
    }

    private static void EnsureInstanceExists()
    {
        if (Instance != null)
            return;

        GameObject loggerObject = new GameObject("StudyLogger");
        Instance = loggerObject.AddComponent<StudyLogger>();
    }
}