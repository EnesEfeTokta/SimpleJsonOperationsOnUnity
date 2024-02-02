using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;



[System.Serializable]
public class SetPlayerData
{
    public string playerName;
    public int playerScore;
    public bool isPlayerAlive;
}



[System.Serializable]
public class SetPlayerList
{
    public List<SetPlayerData> players;
}



public class ReplaceJson : MonoBehaviour
{
    public string jsonFileName;
    public List<string> whichPlayerName;
    public List<string> setPlayerValues;
    public List<int> setPlayerScores;
    public List<bool> setPlayerAlives;

    void Start()
    {
        if (jsonFileName != null && whichPlayerName != null && setPlayerValues != null && setPlayerScores != null && setPlayerAlives != null)
        {
            int numberOfPlayers = Mathf.Min(whichPlayerName.Count, setPlayerValues.Count, setPlayerScores.Count, setPlayerAlives.Count);
            for (int i = 0; i < numberOfPlayers; i++)
            {
                UpdatePlayer(whichPlayerName[i], setPlayerValues[i], setPlayerScores[i], setPlayerAlives[i]);
            }
        }
        else
        {
            Debug.LogError("Empty values are not accepted, please fill in the name and other information to modify the Json file.");
        }
    }

    void UpdatePlayer(string oldName, string newName, int newScore, bool newAliveState)
    {
        string filePath = Path.Combine(Application.dataPath, "Resources/JSONData/" + jsonFileName + ".json");
        string jsonContent = System.IO.File.ReadAllText(filePath);

        SetPlayerList playerList = JsonUtility.FromJson<SetPlayerList>(jsonContent);

        SetPlayerData playerToUpdate = playerList.players.Find(player => player.playerName == oldName);
        if (playerToUpdate != null)
        {
            playerToUpdate.playerName = newName;
            playerToUpdate.playerScore = newScore;
            playerToUpdate.isPlayerAlive = newAliveState;

            string updatedJson = JsonUtility.ToJson(playerList);

            System.IO.File.WriteAllText(filePath, updatedJson);

            Debug.Log("Player updated: " + oldName + " -> " + newName + ", Score: " + newScore + ", Alive: " + newAliveState);
        }
        else
        {
            Debug.LogError("No file named " + oldName + " was found. This file may not have been created or you may have misspelled the file name.");
        }
    }
}
