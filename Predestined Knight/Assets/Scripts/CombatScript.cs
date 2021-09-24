using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{

    public Animator anim;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

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

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies)
        {
            Debug.Log("We hit" + enemy.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
