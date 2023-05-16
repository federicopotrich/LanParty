using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManagerNet : NetworkBehaviour
{
    public static GameManagerNet Instance {get; private set;}

    private void Awake(){
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void startHost(){
        NetworkManager.Singleton.ConnectionApprovalCallback += Net_approval_connection;
        NetworkManager.Singleton.StartHost();
    }
    public void startClient(){
        NetworkManager.Singleton.StartClient();
        Debug.Log(NetworkManager.Singleton.ConnectedClients);
    }
    void Net_approval_connection(NetworkManager.ConnectionApprovalRequest req, NetworkManager.ConnectionApprovalResponse res){
        res.Approved = true;
    }
}