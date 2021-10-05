using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public Animator anim;

    public float blockRate = 2.0f;
    float nextBlockTime = 0f;

    public bool blocking;

    public Transform Sword;

    Dash dashScript;

    // Start is called before the first frame update
    void Start()
    {
        dashScript = GetComponent<Dash>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<GameManager>().gameOver)
        {

            if (Time.time >= nextBlockTime)
            {
                if (Input.GetMouseButtonDown(1) && dashScript.rolling == false)
                {                    
                    Blocking();
                    nextBlockTime = Time.time + 1f / blockRate;
                }

            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Male Sword Block") &&
                 anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f &&
                 !anim.IsInTransition(0))
            {
                
                blocking = true;
            }
            else
            {
                
                blocking = false;
            }

        }

    }

    void Blocking()
    {
        anim.SetTrigger("Blocking");
    }

    void StartedBlocking()
    {
        Sword.gameObject.tag = "Block";
    }

    void EndBlocking()
    {
        Sword.gameObject.tag = "PlayerWeapon";
    }

    void OnTriggerEnter(Collider coll)
    {
        
        if (coll.CompareTag("Weapon") && blocking)
        {
            print("BLOCKED");
            //Sword.gameObject.tag = "Untagged";
        }


    }
}
