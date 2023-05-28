using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using System.IO;

public class LobbyManager : NetworkBehaviour
{
    public static LobbyManager instance { get; private set; }
    private Dictionary<ulong, UtenteReady> playersReadyServerRpcDic;
    private void Awake()
    {
        instance = this;
        playersReadyServerRpcDic = new Dictionary<ulong, UtenteReady>();
    }

    public void SetPlayerReady()
    {
        if (!NetworkManager.Singleton.IsHost)
        {
            TMP_InputField inputField = GameObject.FindObjectOfType<TMP_InputField>();
            TMP_Dropdown dropDownField = GameObject.FindObjectOfType<TMP_Dropdown>();

            if (inputField != null && inputField.text != "" && dropDownField != null)
            {
                string playerName = inputField.text;
                string playerTeam = dropDownField.options[dropDownField.value].text;
                playersReadyServerRpcDic[NetworkManager.Singleton.LocalClientId].name = playerName;
                playersReadyServerRpcDic[NetworkManager.Singleton.LocalClientId].team = playerTeam;
                SetPlayerReadyServerRpc();
            }
        }
        else
        {
            SetPlayerReadyServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetPlayerReadyServerRpc(ServerRpcParams serverRpcParams = default)
    {
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/data.txt", true);
        UtenteReady u = new UtenteReady();
        u.ready = true;

        playersReadyServerRpcDic[serverRpcParams.Receive.SenderClientId].ready = u.ready;
        bool allClientReady = true;

        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (!playersReadyServerRpcDic.ContainsKey(clientId) || !playersReadyServerRpcDic[clientId].ready)
            {
                allClientReady = false;
                break;
            }
        }

        if (allClientReady)
        {
            foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
            {
                string playerName = playersReadyServerRpcDic[clientId].name;
                // Salva il nome del giocatore o fai qualsiasi altra operazione necessaria
                sw.WriteLine(clientId+";"+playerName+";"+playersReadyServerRpcDic[clientId].team);
            }

            sw.Close();
            NetworkManager.Singleton.SceneManager.LoadScene("GameSchoolScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }


}
public class UtenteReady
{
    public bool ready;
    public string name;
    public string team;
}

public class c : MonoBehaviour
{
    public Dictionary<ulong, UtenteReady> players;
}