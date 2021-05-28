using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_BarController : MonoBehaviour
{
    GameObject sdPlayer;
    
    void Start()
    {
        this.sdPlayer = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(sdPlayer);


    }

    // Update is called once per frame
    void Update()
    {
        if (sdPlayer != null)
        {
            transform.position = sdPlayer.transform.position;
            transform.position += new Vector3(0f,1.4f,0f);
        }
        transform.rotation = Camera.main.transform.rotation;
    }
}
