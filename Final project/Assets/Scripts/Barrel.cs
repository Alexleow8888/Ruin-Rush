using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private Vector3 Barrel1;
    //Creates a variable for the Barrel


    // Start is called before the first frame update
    void Start()
    {
        Barrel1 = transform.position;
        //Sets the Respawn Point for the Barrel
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Barrel")
        {
            transform.position = Barrel1;
            // Respawns the Barrel
        }
    }
}
