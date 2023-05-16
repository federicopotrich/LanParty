using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_musicManager : MonoBehaviour
{

    public AudioSource music;
    public AudioSource fsx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        music.volume = PlayerPrefs.GetFloat("volume", 0.5f);
        fsx.volume = PlayerPrefs.GetFloat("volume", 0.5f);
    }
}
