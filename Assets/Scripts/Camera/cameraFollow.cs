using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject player; //Объект за которым следует камера
    Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        gameObject.transform.position = new Vector3(0f, player.transform.position.y + offset.y, -10f);
    }
}
