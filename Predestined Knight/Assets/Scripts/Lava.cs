using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            //kill player
            FindObjectOfType<GameManager>().EndGame();
        }
    }
    

}
