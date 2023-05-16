using Unity.Netcode;
using UnityEngine;

public class ServerVision : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(NetworkManager.Singleton.IsServer && NetworkManager.Singleton.LocalClientId == NetworkManager.ServerClientId){
            this.gameObject.SetActive(false);
        }
    }
}
