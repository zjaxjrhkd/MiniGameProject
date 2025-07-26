using System.Collections.Generic;

[System.Serializable]
public class PlayerSaveData
{
    public string playerName;
    public int playerGold;
    public List<PlayerScoreEntry> playerGameScoreList = new List<PlayerScoreEntry>();
}