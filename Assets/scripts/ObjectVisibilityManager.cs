using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ObjectVisibilityManager : NetworkBehaviour {
    private List<NetworkObject> _networkObjects;

    public void AddNetworkObject(NetworkObject networkObject) {
        _networkObjects.Add(networkObject);
    }

    private void Start() {
        _networkObjects = new List<NetworkObject>();
    }

    private void FixedUpdate() {
        if (!IsHost) return;

        foreach (var client in NetworkManager.Singleton.ConnectedClients) {
            if (client.Key == NetworkManager.LocalClientId) continue;

            foreach (var networkObject in _networkObjects) {
                var clientTransform = client.Value.PlayerObject.transform;
                bool isVisible = Vector3.Distance(clientTransform.position, networkObject.transform.position) < 5;

                if (networkObject.IsNetworkVisibleTo(client.Key) != isVisible) {
                    if (isVisible){
                        networkObject.NetworkShow(client.Key);
                    } else {
                        networkObject.NetworkHide(client.Key);
                    }
                }
            }
        }
    }
}