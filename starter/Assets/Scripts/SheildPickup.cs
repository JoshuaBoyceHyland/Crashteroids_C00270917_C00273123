using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Device;
using static Codice.Client.Common.EventTracking.TrackFeatureUseEvent.Features.DesktopGUI.Filters;

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
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
    private Vector3 RandomSpawn()
    {
        Vector3 spawnPos = new Vector3();

        spawnPos.x = Random.Range(-8, 8);
        spawnPos.y = 10;
        spawnPos.z = 0;
        return spawnPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        Ship possibleSHip = other.gameObject.GetComponent<Ship>();
        if (possibleSHip != null)
        {
            possibleSHip.shield.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    
    
   
    
}
