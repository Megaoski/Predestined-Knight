using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementScript : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    
    private NavMeshAgent myAgent;

    public Animator anim;
    bool isMovementPressed;

    CombatScript combatScript;

    public Transform cam;

    public Vector3 moveDir;

    private void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        combatScript = GetComponent<CombatScript>();
    }

    void HandleAnimations()
    {
        bool isWalking = anim.GetBool("isWalking");
        bool isRunning = anim.GetBool("isRunning");

        if(isMovementPressed && !isWalking)
        {
            anim.SetBool("isWalking", true);
        }
        else if(!isMovementPressed && isWalking)
        {
            anim.SetBool("isWalking", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!FindObjectOfType<GameManager>().gameOver)
        {
            HandleAnimations();

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


            if (direction.magnitude >= 0.1f && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("Male Attack 1") &&
                !this.anim.GetCurrentAnimatorStateInfo(0).IsName("Male Attack 2") &&
                !this.anim.GetCurrentAnimatorStateInfo(0).IsName("Male Attack 3"))//move player if is not attacking
            {
                isMovementPressed = true;
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else
            {
                isMovementPressed = false;
            }

            //---------------Should do workaround for when using a mobile roulette
            //if(Input.GetMouseButtonDown(0))
            //{
            //    Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit hitInfo;

            //    if(Physics.Raycast(myRay, out hitInfo))
            //    {
            //        transform.position = hitInfo.point;
            //        myAgent.SetDestination(hitInfo.point);
            //    }
            //}
        }
    }
}
