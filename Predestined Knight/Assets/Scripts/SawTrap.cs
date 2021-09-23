using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{

    [SerializeField] private GameObject[] Waypoints;
    private int currentWaypointIndex = 0;
  
    [SerializeField] private float speed = 1f;
    [SerializeField] private float rot_speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Vector3.Distance(Waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if(currentWaypointIndex >= Waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        transform.Rotate(360* rot_speed * Time.deltaTime, 0, 0);
        transform.position = Vector3.MoveTowards(transform.position, Waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            //kill player
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
