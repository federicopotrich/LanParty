using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class SettingPlayerSpawn : MonoBehaviour
{
    public GameObject playerPref;
    bool b = true;
    void Update()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameSchoolScene" && b){
            NetworkManager.Singleton.NetworkConfig.PlayerPrefab = playerPref;
            GameObject playerObject = NetworkObject.Instantiate(playerPref, Vector3.zero, Quaternion.identity);
            b = false;
        }
    }
}