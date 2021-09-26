using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTrap : MonoBehaviour
{

    public Transform launchPoint;
    public GameObject SpearPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //when to shoot
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    void Shoot()
    {
       Instantiate(SpearPrefab, launchPoint.transform.position, launchPoint.transform.rotation);
       
    }
}
