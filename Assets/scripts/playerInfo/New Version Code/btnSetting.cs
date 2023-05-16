using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnSetting : MonoBehaviour
{
    public Button readyButton;
    void Awake()
    {
        readyButton.onClick.AddListener(()=>{
            LobbyManager.instance.SetPlayerReady();
        });
    }
}
