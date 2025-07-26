using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private string _playerName;
    public string PlayerName
    {
        get { return _playerName; }
        set { _playerName = value; }
    }
    private int _playerGold;
    public int PlayerGold
    {
        get { return _playerGold; }
        set { _playerGold = value; }
    }
    private Dictionary<string, int> _playerGameScore = new Dictionary<string, int>();
    public Dictionary<string, int> PlayerGameScore
    {
        get { return _playerGameScore; }
        set { _playerGameScore = value; }
    }
    private Dictionary<string, int> _playerSprite = new Dictionary<string, int>();
    public Dictionary<string, int> PlayerSprite
    {
        get { return _playerSprite; }
        set { _playerSprite = value; }
    }
    private string SavePath => Path.Combine(Application.persistentDataPath, "playerData.json");


    void Awake()
    {
        _playerName = "Player1"; 
        _playerGold = 0; 
        _playerGameScore.Add("Run", 0);
        _playerGameScore.Add("BlockStack", 0);
    }

    public void AddGameScore(string gameName, int score)
    {
        if (_playerGameScore.ContainsKey(gameName))
        {
            _playerGameScore[gameName] += score;
        }
        else
        {
            _playerGameScore[gameName] = score;
        }
    }

    public void ChangeEffectSprite(string spriteName, int spriteId)
    {
        if (_playerSprite.ContainsKey(spriteName))
        {
            _playerSprite[spriteName] = spriteId;
        }
        else
        {
            _playerSprite.Add(spriteName, spriteId);
        }
    }

    public void ChangeObjectSprite(string spriteName, int spriteId)
    {
        if (_playerSprite.ContainsKey(spriteName))
        {
            _playerSprite[spriteName] = spriteId;
        }
        else
        {
            _playerSprite.Add(spriteName, spriteId);
        }
    }

    public void ChangeSprite(string spriteName, int spriteId)
    {
        if (_playerSprite.ContainsKey(spriteName))
        {
            _playerSprite[spriteName] = spriteId;
        }
        else
        {
            _playerSprite.Add(spriteName, spriteId);
        }
    }
    public void ViewScore()
    {
        foreach (var score in _playerGameScore)
        {
            Debug.Log($"Game: {score.Key}, Score: {score.Value}");
        }
    }

    public void SavePlayerData()
    {
        var saveData = new PlayerSaveData()
        {
            playerName = _playerName,
            playerGold = _playerGold,
            playerGameScoreList = _playerGameScore
                .Select(kvp => new PlayerScoreEntry { key = kvp.Key, value = kvp.Value })
                .ToList()
        };

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(SavePath, json);
        Debug.Log($"플레이어 데이터 저장됨: {SavePath}");
    }

    public void LoadPlayerData()
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("저장 파일 없음");
            return;
        }

        string json = File.ReadAllText(SavePath);
        var loadedData = JsonUtility.FromJson<PlayerSaveData>(json);

        _playerName = loadedData.playerName;
        _playerGold = loadedData.playerGold;
        _playerGameScore = loadedData.playerGameScoreList
            .ToDictionary(entry => entry.key, entry => entry.value);
    }
}
