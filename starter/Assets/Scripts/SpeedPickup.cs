using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public float speed = 1;
    private readonly float maxY = -5;

    private void Start()
    {
        float xPos = Random.Range(-8.0f, 8.0f);
        this.transform.position = new Vector3(xPos, 8, 3.410729f);
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        if (transform.position.y < maxY)
        {
            Destroy(gameObject);
        }
    }
}
