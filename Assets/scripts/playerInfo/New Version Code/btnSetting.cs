using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Netcode;
using System;

public class btnSetting : NetworkBehaviour
{
    public Button readyButton;
    void Awake()
    {
        readyButton.onClick.AddListener(()=>{
            LobbyManager.Instance.SetPlayerReady();
        });
    }
}
