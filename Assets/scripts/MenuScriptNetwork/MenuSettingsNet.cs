using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MenuSettingsNet : MonoBehaviour
{

    public Button btnCreateGame, btnJoinGame;
    public GameManagerNet gm;

    // Start is called before the first frame update
    private void Awake()
    {
        
        btnCreateGame.onClick.AddListener(()=>{
            gm.startHost();
            Loader.LoadNetwork(Loader.Scene.PlayerInfoNet);
            Hide();
        });
        btnJoinGame.onClick.AddListener(()=>{
            gm.startClient();
            //Hide();
        });
    }

    // Update is called once per frame
    void Hide()
    {
        gameObject.SetActive(false);
    }
}
