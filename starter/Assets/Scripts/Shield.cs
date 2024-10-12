using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.GetComponent<Asteroid>() != null)
        {
            Destroy(other.gameObject);  
            gameObject.SetActive(false);
        }
    }
}
