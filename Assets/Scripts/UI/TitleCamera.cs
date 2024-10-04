using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour
{
    [SerializeField] float camSpeed;

    void Update()
    {
        transform.position = new Vector3(camSpeed * Time.deltaTime,0, 0);          
    }
}
