using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private TextMeshProUGUI playerNameInputText;

    public string playerName { get; private set; }
    public string bestPlayerName { get; private set; }
    public int playerHiScore;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerName()
    {
        playerName = playerNameInputText.text;
    }

    public void SetNewHiScore(int hiScore)
    {
        bestPlayerName = playerName;
        playerHiScore = hiScore;
    }

    [System.Serializable]
    public class SaveData
    {
        public string bestPlayerName;
        public int playerHiScore;
    }

    public void SavePlayerNameAndScore()
    {
        SaveData data = new SaveData();

        data.bestPlayerName = bestPlayerName;
        data.playerHiScore = playerHiScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerNameAndScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.bestPlayerName;
            playerHiScore = data.playerHiScore;
        }
    }
}
