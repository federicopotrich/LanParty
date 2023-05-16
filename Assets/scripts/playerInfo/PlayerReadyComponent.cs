using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerReadyComponent : NetworkBehaviour
{
    public static PlayerReadyComponent Instance { get; private set; }
    public Dictionary<ulong, bool> playerReadyDictionaryServerRpc;
    void Awake()
    {
        Instance = this;
        playerReadyDictionaryServerRpc = new Dictionary<ulong, bool>();
    }

    public void SetPlayerReady()
    {
        SetPlayerReadyServerRpc();
    }

    [ServerRpc(RequireOwnership=false)]
    private void SetPlayerReadyServerRpc(ServerRpcParams serverRpcParams = default)
    {
        playerReadyDictionaryServerRpc[serverRpcParams.Receive.SenderClientId] = true;

        bool allClientReady = true;

        foreach (ulong clientID in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if(!playerReadyDictionaryServerRpc.ContainsKey(clientID) || !playerReadyDictionaryServerRpc[clientID])
            {
                allClientReady = false;
                break;
            }
        }

        if (allClientReady)
        {
            Loader.LoadNetwork(Loader.Scene.GameSchoolScene);
        }
    }
}
