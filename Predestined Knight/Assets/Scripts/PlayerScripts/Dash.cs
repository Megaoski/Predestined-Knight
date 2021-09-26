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
                if (Input.GetMouseButtonDown(1) && currentRollNumber > 0)
                {
                    currentRollNumber -= 1;
                    Roll();
                    nextRollTime = Time.time + 1f / rollRate;

                }

                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                {
                    FindObjectOfType<GameManager>().invulnerable = false;
                }

            }

            if (currentRollNumber == 0 && !rollsRegenerated)
            {
                Invoke("AddRolls", 5.0f);
                rollsRegenerated = true;
            }

        }

        Debug.Log("ROLLS: " + currentRollNumber);
    }

    void Roll()
    {
        FindObjectOfType<GameManager>().invulnerable = true;
        anim.SetTrigger("isDashing");
    }

    void AddRolls()
    {
                    
        currentRollNumber = rollNumber;
        rollsRegenerated = false;
        Debug.Log("Adding roll");
            
            
           
    }
}
