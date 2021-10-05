using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    MovementScript moveScript;
    
    public Animator anim;

    public float rollRate = 2.0f;
    float nextRollTime = 0f;
    public uint rollNumber;
    private uint currentRollNumber;

    private bool rollsRegenerated = false;

    public bool rolling = false;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<MovementScript>();
        currentRollNumber = rollNumber;
    }

    // Update is called once per frame
    void Update()
    {
                

        if (!FindObjectOfType<GameManager>().gameOver)
        {
            if (Time.time >= nextRollTime)
            {
                if (Input.GetKeyDown("space") && currentRollNumber > 0)
                {                    
                    currentRollNumber -= 1;
                    Roll();
                    nextRollTime = Time.time + 1f / rollRate;
                }

                
                //if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                //{
                //    FindObjectOfType<GameManager>().invulnerable = false;
                //}

                
            }

            if (currentRollNumber == 0 && !rollsRegenerated)
            {
                Invoke("AddRolls", 5.0f);
                rollsRegenerated = true;
            }

            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Male Sword Roll") &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f &&
                !anim.IsInTransition(0))
            {
                rolling = true;
            }
            else
            {
                rolling = false;
            }
                   
        }

        Debug.Log("ROLLS: " + currentRollNumber);
        Debug.Log("INVULNERABILITY: " + FindObjectOfType<GameManager>().invulnerable);
        Debug.Log("ROLLING: " + rolling);
    }

    void Roll()
    {
        //FindObjectOfType<GameManager>().invulnerable = true;
        anim.SetTrigger("isDashing");
    }

    void SetInvulnerable()
    {
        FindObjectOfType<GameManager>().invulnerable = true;
    }

    void SetVulnerable()
    {
        FindObjectOfType<GameManager>().invulnerable = false;
    }

    void AddRolls()
    {
                    
        currentRollNumber = rollNumber;
        rollsRegenerated = false;
        Debug.Log("Adding roll");
            
            
           
    }
}
