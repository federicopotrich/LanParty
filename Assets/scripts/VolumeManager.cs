using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    void Start(){
        this.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume", 0.5f);
    }
    public void changeVolume(){
        float volume = this.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("volume", volume);
    }
}
