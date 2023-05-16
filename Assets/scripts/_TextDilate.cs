using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TextDilate : MonoBehaviour
{
    
    public float dilate;
    void Start()
    {
        
    }

    void Update()
    {
        this.transform.position = GameObject.Find("Player").transform.position;
        dilate = Mathf.Clamp(dilate, -1, 0);
        this.GetComponent<TMPro.TextMeshProUGUI>().fontMaterial.SetFloat(TMPro.ShaderUtilities.ID_FaceDilate, dilate);
    }
}
