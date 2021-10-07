using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    public GameObject enemy;
    EnemyController script;

    // Start is called before the first frame update
    void Start()
    {
        script = enemy.GetComponent<EnemyController>();

        if(script == null)
        {
            Debug.Log("PARENT ENEMY NOT ASSGNED TO WEAPON");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        
        if (coll.CompareTag("Parry") && !FindObjectOfType<GameManager>().gameOver)
        {
           print("Parry Stunned");
           script.currentState = EnemyController.State.PARRIED;
           //enemy.c = EnemyController.State.PARRIED;

        }

    }
}
