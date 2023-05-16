using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class ChangeSceneButton : NetworkBehaviour
{
    [SerializeField] private Button changeSceneButton;

    private void Update()
    {
        if (NetworkManager.Singleton.IsServer && NetworkManager.Singleton.LocalClientId == NetworkManager.ServerClientId)
        {
            changeSceneButton.onClick.AddListener(ChangeScene);
        }
        else
        {
            changeSceneButton.interactable = false;
        }
    }

    private void ChangeScene()
    {
        StartCoroutine(ChangeSceneCoroutine());
    }

    private IEnumerator ChangeSceneCoroutine()
    {
        if (NetworkManager.Singleton.IsServer && NetworkManager.Singleton.LocalClientId == NetworkManager.ServerClientId)
        {
            // Cambia scena solo se siamo l'host
            NetworkManager.Singleton.SceneManager.LoadScene("PlayerInfoNet", LoadSceneMode.Single);
            yield return new WaitForSeconds(1f);

            // Attendi il caricamento della scena
            while (NetworkManager.Singleton.IsListening)
            {
                yield return null;
            }
        }
    }
}
