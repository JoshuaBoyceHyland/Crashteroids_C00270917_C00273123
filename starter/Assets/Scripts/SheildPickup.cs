using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Device;

public class SheildPickup : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        transform.position = RandomSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckForOffScreen();
    }

    private void Move()
    {
        Vector3 movement = new Vector3(0, 3, 0);
        transform.position -= movement * Time.deltaTime;
          
    }

    private void CheckForOffScreen()
    {
        if (transform.position.y < -3)
        {
            Destroy(gameObject);
        }
    }
    private Vector3 RandomSpawn()
    {
        Vector3 spawnPos = new Vector3();

        spawnPos.x = Random.Range(-8, 8);
        spawnPos.y = 10;
        spawnPos.z = 3.410729f;


        return spawnPos;
    }
}
