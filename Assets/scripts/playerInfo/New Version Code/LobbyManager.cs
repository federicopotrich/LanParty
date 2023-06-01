using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using Unity.Collections;

public class LobbyManager : NetworkBehaviour
{
    public static LobbyManager Instance { get; private set; }
    
    private Dictionary<ulong, UtenteReady> playersReadyServerRpcDic;
    public Button btn;

    private void Awake()
    {
        Instance = this;
        playersReadyServerRpcDic = new Dictionary<ulong, UtenteReady>();
    }

    public void SetPlayerReady()
    {
        if (!NetworkManager.Singleton.IsHost)
        {
            TMP_InputField inputField = FindObjectOfType<TMP_InputField>();
            TMP_Dropdown dropDownField = FindObjectOfType<TMP_Dropdown>();

            if(inputField.text == "" ){
                SetPlayerReadyServerRpc("UknownName", "1A");
            }
            if (inputField != null && inputField.text != "" && dropDownField != null)
            {
                string playerName = inputField.text.Length > 10 ? inputField.text.Substring(0, 10) : inputField.text;
                string playerTeam = dropDownField.options[dropDownField.value].text;
                SetPlayerReadyServerRpc(playerName, playerTeam);
            }
        }
        else
        {
            SetPlayerReadyServerRpc("host", "null");
        }

        btn.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text += " √";
        btn.enabled = false;
    }

    [ClientRpc]
    public void FClientRpc(ClientRpcParams param)
    {
        GameObject g = new GameObject();
        g.AddComponent<C>();
        g.GetComponent<C>().Players = playersReadyServerRpcDic;
        g.name = "dataConnectedPlayers";
        DontDestroyOnLoad(g);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetPlayerReadyServerRpc(string playerName, string playerTeam, ServerRpcParams serverRpcParams = default)
    {
        UtenteReady u = new UtenteReady();
        u.ready = true;
        u._name = playerName;
        u._team = playerTeam;
        playersReadyServerRpcDic.Add(serverRpcParams.Receive.SenderClientId, u);

        bool allClientsReady = true;

        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (!playersReadyServerRpcDic.ContainsKey(clientId) || !playersReadyServerRpcDic[clientId].ready)
            {
                allClientsReady = false;
                break;
            }
        }

        if (allClientsReady)
        {
            foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
            {
                FClientRpc(new ClientRpcParams
                {
                    Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { clientId } }
                });
            }
            NetworkManager.Singleton.SceneManager.LoadScene("GameSchoolScene", LoadSceneMode.Single);

        }
    }
}

public class UtenteReady
{
    public bool ready;
    public string _name;
    public string _team;
}

public class C : MonoBehaviour
{
    public Dictionary<ulong, UtenteReady> Players;
}
