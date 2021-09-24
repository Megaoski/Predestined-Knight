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
            RotateTile(15, 0.1f, 0.2f);

            Invoke("Drop", 1f);

        }
    }

    private void Drop()
    {
        isFalling = true;
        Destroy(gameObject, 1f);
    }

    void RotateTile(float angle, float inTime, float repeatTime)
    {
        bool turnRight = false;
        float rotationSpeed = angle / inTime;
        Quaternion startRotation = transform.rotation;

        float deltaAngle = 0;

        if(deltaAngle < angle && !turnRight)
        {
            deltaAngle += rotationSpeed * Time.deltaTime;
            deltaAngle = Mathf.Min(deltaAngle, angle);

            transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, Vector3.right);
            turnRight = true;
        }
        //no accede aqui, quizas hemos de ponerlo dentro del if de arriba
        if (deltaAngle == angle && turnRight)
        {
            deltaAngle += rotationSpeed * Time.deltaTime;
            deltaAngle = Mathf.Min(deltaAngle, angle);

            transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, Vector3.left);
            turnRight = false;
        }
    }


}


