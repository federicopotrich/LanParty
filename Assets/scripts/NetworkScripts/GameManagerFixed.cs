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
                NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneManager_OnLoadEventCompleted;
            }   
            b = false;
        }
    }
    private void SceneManager_OnLoadEventCompleted(string sceneName, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        int ctr = 0;
        string[] visibleLayers;
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            visibleLayers = new string[2]{"Default", ""};
            if (NetworkManager.IsClient)
            {
                // Spawn del player
                Transform playerTransform = Instantiate(playerPrefab);
                playerTransform.GetComponent<PlayerGameManager>().name = GameObject.Find("dataConnectedPlayers").GetComponent<c>().players[clientId]._name;
                playerTransform.GetComponent<PlayerGameManager>().team = GameObject.Find("dataConnectedPlayers").GetComponent<c>().players[clientId]._team;

                //playerTransform.gameObject.layer = 12+ctr;
                
                playerTransform.Find("CanvasStoria").gameObject.layer = 12+ctr;
                playerTransform.Find("CanvasItaliano").gameObject.layer = 12+ctr;
                playerTransform.Find("Points").gameObject.layer = 12+ctr;
                visibleLayers[1] = "Player"+(1+ctr);

                int cullingMask = 0; // Variabile per memorizzare la Culling Mask

                // Itera attraverso gli array dei nomi dei layer visibili
                for (int i = 0; i < visibleLayers.Length; i++)
                {
                    int layer = LayerMask.NameToLayer(visibleLayers[i]); // Ottieni l'indice del layer dal suo nome
                    cullingMask |= 1 << layer; // Imposta il bit corrispondente all'indice del layer
                }
                playerTransform.Find("MainCameraPlayer").gameObject.GetComponent<Camera>().cullingMask = cullingMask;
                // Spawn del player object
                playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
                Debug.Log("DIO NEgro");
            }
            ctr++;
        }
    }

    
}
