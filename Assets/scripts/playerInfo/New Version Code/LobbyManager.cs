using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class LobbyManager : NetworkBehaviour
{
    public static LobbyManager instance { get; private set; }
    private Dictionary<ulong, UtenteReady> playersReadyServerRpcDic;
    public Button btn;
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
                if(inputField.text.Length>10){
                    string playerName = inputField.text.Substring(0, 10);
                    string playerTeam = dropDownField.options[dropDownField.value].text;
                    SetPlayerReadyServerRpc(playerName, playerTeam);
                }else{

                    string playerName = inputField.text;
                    string playerTeam = dropDownField.options[dropDownField.value].text;
                    SetPlayerReadyServerRpc(playerName, playerTeam);
                }
            }
        }
        else
        {
            SetPlayerReadyServerRpc("host", "null");
        }
        btn.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = btn.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text + " √";
        btn.enabled = false;
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetPlayerReadyServerRpc(string namePlayer, string classPlayer, ServerRpcParams serverRpcParams = default)
    {
        UtenteReady u = new UtenteReady();
        u.ready = true;
        u._name = namePlayer;
        u._team = classPlayer;

        playersReadyServerRpcDic[serverRpcParams.Receive.SenderClientId] = u;
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
            GameObject g = Instantiate(new GameObject());

            g.AddComponent<c>();
            g.GetComponent<c>().players = playersReadyServerRpcDic;
            g.name = "dataConnectedPlayers";
            DontDestroyOnLoad(g);
            NetworkManager.Singleton.SceneManager.LoadScene("GameSchoolScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }

}
public class UtenteReady
{
    public bool ready;
    public string _name;
    public string _team;
}

public class c : MonoBehaviour
{
    public Dictionary<ulong, UtenteReady> players;
}