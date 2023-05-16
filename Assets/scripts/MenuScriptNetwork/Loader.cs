using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
public class Loader : MonoBehaviour
{
    public enum Scene{
        GameSchoolScene,
        PalestraScene,
        MainMenu,
        PlayerInfoNet
    }
    private static Scene targetScene;

    public static void Load(Scene targetScene){
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Loader.targetScene.ToString());
    }
    public static void LoadNetwork(Scene targetScene){
        //Debug.Log(targetScene.ToString());
        NetworkManager.Singleton.SceneManager.LoadScene(targetScene.ToString(), LoadSceneMode.Single);
    }
    public static void LoaderCallback(){
        SceneManager.LoadScene(targetScene.ToString());
    }
    
}
