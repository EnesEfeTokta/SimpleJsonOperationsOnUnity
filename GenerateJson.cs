using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Mono.Cecil.Cil;
using JetBrains.Annotations;



[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int playerScore;
    public bool isPlayerAlive;
}



[System.Serializable]
public class PlayerList
{
    public List<PlayerData> players;
}



public class GenerateJson : MonoBehaviour
{
    public bool createResourcesJSONDataFolder = true;

    public string setJsonName;
    public List<string> predefinedPlayerNames;
    public List<int> predefinedPlayerScores;
    public List<bool> predefinedPlayerAliveStates;

    void Start()
    {
        if (createResourcesJSONDataFolder)
        {
            CreateNestedFolders();
        }

        if (predefinedPlayerNames != null && predefinedPlayerScores != null && predefinedPlayerAliveStates != null && setJsonName != null)
        {
            Create();
        }
        else
        {
            Debug.LogError("Empty value is not accepted, please specify name and create values to create Json file.");
        }
    }

    void Create()
    {
        PlayerList playerList = new PlayerList();
        playerList.players = new List<PlayerData>();

        int numberOfPlayers = Mathf.Min(predefinedPlayerNames.Count, predefinedPlayerScores.Count, predefinedPlayerAliveStates.Count);

        for (int i = 0; i < numberOfPlayers; i++)
        {
            playerList.players.Add(new PlayerData
            {
                playerName = predefinedPlayerNames[i],
                playerScore = predefinedPlayerScores[i],
                isPlayerAlive = predefinedPlayerAliveStates[i]
            });
        }

        string json = JsonUtility.ToJson(playerList);

        string filePath = Path.Combine(Application.dataPath, "Resources/JSONData/" + setJsonName + ".json");
        System.IO.File.WriteAllText(filePath, json);

        Debug.Log(setJsonName + " JSON file named JSON was successfully created in Resources/JSONData!");
    }



    [MenuItem("Custom Tools/Create Nested Folders")]
    static void CreateNestedFolders()
    {
        string rootFolder = "Assets";
        string subFolder1 = "Resources";
        string subFolder2 = "JSONData";

        string rootFolderPath = Path.Combine(rootFolder, subFolder1, subFolder2);

        if (!AssetDatabase.IsValidFolder(rootFolderPath))
        {
            AssetDatabase.CreateFolder(rootFolder, subFolder1);
            AssetDatabase.CreateFolder(Path.Combine(rootFolder, subFolder1), subFolder2);
            Debug.Log("Folders have been created: " + rootFolderPath);
        }
        else
        {
            Debug.LogWarning("The desired folders exist. No need to create a new one: " + rootFolderPath);
        }
    }
}
