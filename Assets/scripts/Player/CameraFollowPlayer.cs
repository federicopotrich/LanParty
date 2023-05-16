using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraFollowPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    

    // Update is called once per frame
    void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }
    public void FollowPlayer(Transform tr){
        _camera.Follow = tr;
    }
}
