using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{

    public Animator anim;

    public float attackRate = 2.0f;
    float nextAttackTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!FindObjectOfType<GameManager>().gameOver)
        {
            if (Time.time >= nextAttackTime)
            {
                //attacking = false;

                if (Input.GetMouseButtonDown(0))
                {

                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;

                }


            }

        }

        
    }

    void Attack()
    {
        anim.SetInteger("AttackIndex", Random.Range(0, 3));
        anim.SetTrigger("isAttacking");

        //Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        //foreach(Collider enemy in hitEnemies)
        //{
        //    //Our player attackpoint activates enemy dead animation, then from the proper enemy script we 
        //    // check if dead animation is playing, if so we activate the Dead State. WORKAROUND
        //    Debug.Log("We hit: " + enemy.name);
        //    Animator anim = enemy.gameObject.GetComponent<Animator>();
        //    anim.SetBool("Dead", true);
        //}
    }

    void OnTriggerEnter(Collider coll)
    {
        //iF PLAYER RECEIVES HIT FROM WEAPON KILL IT FOR NOW, LATER THINK ABOUT HP SYSTEM ETC...
        if (coll.CompareTag("Weapon"))
        {
            print("PLAYER HIT");
            FindObjectOfType<GameManager>().EndGame();
        }

    }

    
}
