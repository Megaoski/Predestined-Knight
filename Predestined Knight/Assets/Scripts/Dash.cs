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

            if (currentRollNumber < rollNumber)
            {
                StartCoroutine(AddRolls());
            }

        }

        Debug.Log("ROLLS: " + currentRollNumber);
    }

    void Roll()
    {
        FindObjectOfType<GameManager>().invulnerable = true;
        anim.SetTrigger("isDashing");
    }

    IEnumerator AddRolls()
    {
        while (true)
        {
            //yield return new WaitForSeconds(5);// this will pause for 5 seconds
            currentRollNumber++;
            Debug.Log("Adding roll");
            yield return new WaitForSeconds(3);

        }
    }
}
