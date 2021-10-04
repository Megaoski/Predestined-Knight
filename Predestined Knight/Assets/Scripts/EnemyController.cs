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
    State currentState;

    bool hasAttacked = false;
   
   

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
        HandleStates(currentState);
        
        float distance = Vector3.Distance(target.position, transform.position);

        if (currentState != State.DEAD)
        {

            if (distance <= lookRadius && distance > agent.stoppingDistance)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1h1")
                    && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f
                    && !anim.IsInTransition(0))
                {

                    agent.isStopped = true;

                }
                else
                {
                    currentState = State.WALKING;
                    FaceTarget();
                    agent.isStopped = false;
                    agent.SetDestination(target.position);
                }

                hasAttacked = false;
            }
            if (distance <= lookRadius && distance <= agent.stoppingDistance)//if player is in aggro range and inside attack range, skeleton stops and attacks
            {
                //make that skeleton receives event when attack anim starts
                //and while that bool is false facetarget
                
                FaceTarget();

                if (!hasAttacked)
                {                    
                    agent.isStopped = true;
                    currentState = State.ATTACKING;                    
                    hasAttacked = true;
                }                
               
            }
            if (distance > lookRadius)// if player is out of aggro range keep in place
            {

                agent.isStopped = true;
                currentState = State.IDLE;

            }
        }

        if (AnimatorIsPlaying("Fall1"))//CHECK ID SKELETON IS IN FALLING ANIAMTION TO PUT IT TO DEAD
        {
            agent.isStopped = true;
            currentState = State.DEAD;
        }

        Debug.Log("State: " + currentState);
        Debug.Log("Stopped: " + agent.isStopped);
        Debug.Log("Attacked: " + hasAttacked);
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
                anim.SetBool("Attack", false);
                anim.SetBool("Walking", false);                
                break;
            case State.WALKING:                  
                anim.SetBool("Attack", false);
                anim.SetBool("Walking", true);                                       
                break;
            case State.ATTACKING:
                anim.SetBool("Attack", true);                
                break;
            case State.HURT:

                break;
            case State.DEAD:
                Destroy(gameObject.GetComponent<Collider>());
                break;
        }
    }


    bool AnimatorIsPlaying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).length >
               anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
       

    
}
