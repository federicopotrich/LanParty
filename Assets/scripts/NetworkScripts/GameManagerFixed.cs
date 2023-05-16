using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Linq;

public class GameManagerFixed : NetworkBehaviour
{
    private List<Camera> allCameras = new List<Camera>();
    // Questa è un'istanza singleton del GameManager
    public static GameManagerFixed Instance { get; private set; }
    bool b = true;
    [SerializeField] private Transform playerPrefab;

    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameSchoolScene" && b == true)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                //GameObject.Find("Camera").SetActive(true);
                NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneManager_OnLoadEventCompleted;
            }
            b = false;
        }
    }
    private void SceneManager_OnLoadEventCompleted(string sceneName, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {

            if (NetworkManager.IsClient)
            {
                // Spawn del player
                Transform playerTransform = Instantiate(playerPrefab);
                playerTransform.GetComponent<PlayerGameManager>().name = GameObject.Find("dataConnectedPlayers").GetComponent<c>().players[clientId]._name;
                playerTransform.GetComponent<PlayerGameManager>().team = GameObject.Find("dataConnectedPlayers").GetComponent<c>().players[clientId]._team;
                // Spawn del player object
                playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
            }
        }
    }
}