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

    [System.NonSerialized]public int maxBlockPoints = 100;
    [System.NonSerialized] public int currentBlockPoints;

    private bool blockExhausted = false;

    // Start is called before the first frame update
    void Start()
    {
        dashScript = GetComponent<Dash>();
        currentBlockPoints = maxBlockPoints;

        //StartCoroutine("RegenCoroutine");
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<GameManager>().gameOver)
        {
            CheckBlockPoints();//adjust block points if parry adds too much points

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

            if(currentBlockPoints <= 0)
            {
                blockExhausted = true;
                currentBlockPoints = 0;
            }

            //if(!blocking && currentBlockPoints != maxBlockPoints && !blockExhausted)
            //{                
            //    RegenCoroutine();
            //}
          
        }

        Debug.Log("BLOCKPOINTS: " + currentBlockPoints);
        Debug.Log("EXHAUSTED: " + blockExhausted);
    }

    //IEnumerator RegenCoroutine()
    //{
    //    for (; ; )
    //    {
    //        while(blockExhausted)// if block is in cooldown because player has got to 0 don't regen
    //        {
    //            yield return null;
    //        }

    //        // execute block of code here
    //        RegenBlockBar();
    //        yield return new WaitForSeconds(.5f);
    //    }
    //}

    void RegenBlockBar()
    {
        currentBlockPoints += 1;
    }

    void Blocking()
    {
        anim.SetTrigger("Blocking");
    }

    void StartedBlocking()
    {
        Sword.gameObject.tag = "Block";
        if(currentBlockPoints > 0)
            FindObjectOfType<GameManager>().invulnerable = true;
        //FindObjectOfType<GameManager>().invulnerable = true;
    }

    void EndBlocking()
    {
        Sword.gameObject.tag = "PlayerWeapon";
        FindObjectOfType<GameManager>().invulnerable = false;
        //FindObjectOfType<GameManager>().invulnerable = false;
    }

    void StartedParry()
    {
        Sword.gameObject.tag = "Parry";
        FindObjectOfType<GameManager>().invulnerable = true;
    }

    void EndedParry()
    {
        Sword.gameObject.tag = "Block";
        FindObjectOfType<GameManager>().invulnerable = false;
    }

    void CheckBlockPoints()
    {
        if (currentBlockPoints > maxBlockPoints)
            currentBlockPoints = maxBlockPoints;
    }

    void OnTriggerEnter(Collider coll)
    {
        
        if (coll.CompareTag("Weapon") && blocking && Sword.gameObject.tag == "Block")
        {
            print("BLOCKED");
            currentBlockPoints -= 50;
            //Sword.gameObject.tag = "Untagged";
        }

        if (coll.CompareTag("Weapon") && blocking && Sword.gameObject.tag == "Parry")
        {
            print("PARRIED");
            blockExhausted = false;//if player parries while "shield" is broken make it regen again
            if(currentBlockPoints < maxBlockPoints)
                currentBlockPoints += 50;
            //Sword.gameObject.tag = "Untagged";
        }

        if (coll.CompareTag("Unblockable"))//for traps to insta kill you even blocking
        {
            print("TRAP HITTED YOU");
            FindObjectOfType<GameManager>().invulnerable = false;
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
