using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrap : MonoBehaviour
{
    
    bool isFalling = false;
    float downSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isFalling)
        {
            downSpeed += Time.deltaTime / 10;
            transform.position = new Vector3(transform.position.x, transform.position.y - downSpeed, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider hit)
    {


        if (hit.gameObject.tag == "Player")
        {
            //play animation of platform crumbling
            Invoke("Drop", 1f);
        
        }
    }

    private void Drop()
    {
        isFalling = true;
        Destroy(gameObject, 1f);
    }

   
}
