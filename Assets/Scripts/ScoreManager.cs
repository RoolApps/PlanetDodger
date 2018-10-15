using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class ScoreManager {

    private static int[] ApiKey = new int[] { 97, 113, 70, 50, 88, 75, 110, 53, 71, 105, 56, 105, 86, 113, 117, 109, 80, 53, 117, 99, 66, 97, 105, 108, 106, 65, 89, 111, 72, 112, 54, 110, 55, 48, 71, 98, 72, 118, 113, 77 };
    private const string ReadRecordsUrl = "https://9uzw1jhkae.execute-api.us-east-1.amazonaws.com/default/planetdodger-read-records?difficulty={0}";
    private const string PostRecordUrl = "https://9uzw1jhkae.execute-api.us-east-1.amazonaws.com/default/planetdodger-post-record";

    [Serializable]
    public class Scores
    {
        public Score[] Items;
    }

    [Serializable]
    public class Score
    {
        public int score;
        public string name;
        public string difficulty;
    }

    public static void GetScores(MonoBehaviour behaviour, String difficultyName, Action<Scores> callback)
    {
        behaviour.StartCoroutine(GetScores(difficultyName, callback));
    }

    public static void SubmitScore(MonoBehaviour behaviour, int score)
    {
        behaviour.StartCoroutine(SubmitScore(score));
    }

    private static void SetHeaders(UnityWebRequest request)
    {
        request.SetRequestHeader("content-type", "application/json");
        request.SetRequestHeader("x-api-key", new string(ApiKey.Select(i => Convert.ToChar(i)).ToArray()));
    }

    private static IEnumerator GetScores(string difficultyName, Action<Scores> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(string.Format(ReadRecordsUrl, difficultyName)))
        {
            SetHeaders(www);
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                var scores = JsonUtility.FromJson<Scores>(www.downloadHandler.text);
                callback(scores);
            }
        }
    }

    private static IEnumerator SubmitScore(int score)
    {
        var result = new Score
        {
            score = score,
            name = GameSettings.Current.Nickname,
            difficulty = GameDifficulty.Settings.DifficultyString
        };
        var jsonData = JsonUtility.ToJson(result);
        var bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
        using (UnityWebRequest www = UnityWebRequest.Put(PostRecordUrl, bytes))
        {
            www.method = "POST";
            SetHeaders(www);
            yield return www.Send();
        }
    }
}
