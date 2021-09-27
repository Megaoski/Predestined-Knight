using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{

    private enum State
    {
        IDLE,
        WALKING,
        ATTACKING,
        HURT,
        DEAD
    };

    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;

    public Animator anim;
    private State currentState;
    
    
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        
        anim.SetBool("Walking", true);
        currentState = State.IDLE;
    }

    // Update is called once per frame
    void Update()
    {                
        float distance = Vector3.Distance(target.position, transform.position);


        if(distance <= lookRadius && distance > agent.stoppingDistance)
        {

            agent.isStopped = false;
            currentState = State.WALKING;
            agent.SetDestination(target.position);
            FaceTarget();
                        
        }
        else if(distance <= lookRadius && distance <= agent.stoppingDistance)
        {
            
            FaceTarget();
            currentState = State.ATTACKING;
           
        }
        else if(distance > lookRadius)// if player is out of aggro range
        {
            agent.isStopped = true;
            currentState = State.IDLE;
        }

        HandleStates(currentState);
        Debug.Log("State: " + currentState);
       
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void HandleStates(State state)
    {

        switch (state)
        {
            case State.IDLE:             
                anim.SetBool("Walking", false);
                break;
            case State.WALKING:                              
                anim.SetBool("Walking", true);                
                break;
            case State.ATTACKING:                
                anim.SetTrigger("Attack");                
                break;
            case State.HURT:

                break;
            case State.DEAD:

                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
