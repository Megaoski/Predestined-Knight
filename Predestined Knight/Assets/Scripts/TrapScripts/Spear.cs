using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

        rb.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
