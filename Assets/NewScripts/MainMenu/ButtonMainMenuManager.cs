using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
public class ButtonMainMenuManager : Unity.Netcode.NetworkBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameObject.Find("HostBtn").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{
            Unity.Netcode.NetworkManager.Singleton.ConnectionApprovalCallback += NetworkManager_ConnectionApprovalCallback;
            Unity.Netcode.NetworkManager.Singleton.StartHost();
            GameObject.Find("ContinueBtn").GetComponent<UnityEngine.UI.Button>().enabled = true;
            GameObject.Find("ContinueBtn").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{
                Unity.Netcode.NetworkManager.Singleton.SceneManager.LoadScene("PlayerInfoNet", UnityEngine.SceneManagement.LoadSceneMode.Single);
            });
        });
        GameObject.Find("ClientBtn").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{
            Unity.Netcode.NetworkManager.Singleton.StartClient();
        });
    }

    private void NetworkManager_ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        response.Approved = true;
    }
    
}
