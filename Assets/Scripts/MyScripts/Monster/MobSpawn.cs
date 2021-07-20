using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour
{
    GameObject player; 

    GameObject[] dog;
    private const int dogCount = 3;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        dog = new GameObject[dogCount];

        for (int i = 0; i < dogCount; i++)
        {
            dog[i] = Managers.GetResourceManager.Instantiate("MyPrefabs/Dog");

            dog[i].name = "dog" + i.ToString();
            dog[i].transform.position = new Vector3(1 * ((i % (dogCount - 1)) - 1), 0f, 4f);
            dog[i].transform.LookAt(player.transform);
            dog[i].transform.parent = transform;
        }

    }

}
