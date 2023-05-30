using Unity.Netcode;
using UnityEngine;

public class VisibleNetworkObject : MonoBehaviour {
    private void Start() {
        FindObjectOfType<ObjectVisibilityManager>().AddNetworkObject(GetComponent<NetworkObject>());
    }
}