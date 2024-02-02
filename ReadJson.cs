using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



[System.Serializable]
public class PlayerReadData
{
    public string playerName;
    public int playerScore;
    public bool isPlayerAlive;
}



[System.Serializable]
public class PlayerReadList
{
    public List<PlayerReadData> players;
}



public class ReadJson : MonoBehaviour
{
    public string jsonFileName;

    public List<PlayerReadData> playerList = new List<PlayerReadData>();

    void Start()
    {
        if (jsonFileName != null)
        {
            Read();
        }
        else
        {
            Debug.LogError("Please fill in the JSON name you want to read.");
        }
    }

    void Read()
    {
        string filePath = Path.Combine(Application.dataPath, "Resources/JSONData/" + jsonFileName + ".json");
        string jsonContent = System.IO.File.ReadAllText(filePath);

        PlayerReadList tempList = JsonUtility.FromJson<PlayerReadList>(jsonContent);

        playerList.AddRange(tempList.players);
    }
}
