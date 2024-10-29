using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameratoPlayer : MonoBehaviour
{
    //Transform the camera to a gameobject on the player
    public Transform camerPos;

    private void Update()
    {
        transform.position = camerPos.position;
    }
    
}
