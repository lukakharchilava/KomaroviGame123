using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    [Header("Zombie damage and health")]
    public float zombieHealth = 100f;
    public float presentHealth;
    public float givedamage = 5f;


    [Header("Zombie things")]
    public NavMeshAgent ZombieAgent;
    public Transform Lookpoint;
    public Camera AttackingRaycastArea;
    public Transform PlayerBody;
    public LayerMask Playerlayer;

    [Header("Zombie Guarding var")]
    public GameObject[] walkpoints;
    int currentZombiePosition = 0;
    public float ZombieSpeed = 0;
    float walkingpointRadius = 2;

    [Header("Zombie Attacking Var")]
    public float timeBtwAttack;
    bool previouslyAttack;
    [Header("Zombie Animations")]
    public Animator anim;


    [Header("Zombie mood/ states")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInvionRadius;
    public bool playerInAttackingRadius;
    public Transform Ray1;
    public Transform Ray2;


    private void Awake()
    {
       presentHealth = zombieHealth;
       ZombieAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInvionRadius = Physics.CheckSphere(transform.position, visionRadius, Playerlayer);
        playerInAttackingRadius = Physics.CheckSphere(transform.position, attackingRadius, Playerlayer);

        if (!playerInvionRadius && !playerInAttackingRadius)
            Guard();
        if(playerInvionRadius && !playerInAttackingRadius)
        {
            pursuepPlayer();
        }
        if (playerInvionRadius && playerInAttackingRadius)
        {
            AttackPlayer();
        }
    }

    private void Guard()
    {
        if (Vector3.Distance(walkpoints[currentZombiePosition].transform.position, transform.position) < walkingpointRadius)
        {
            currentZombiePosition = Random.Range(0, walkpoints.Length);
            if(currentZombiePosition >= walkpoints.Length)
            {
                currentZombiePosition = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, walkpoints[currentZombiePosition].transform.position, Time.deltaTime * ZombieSpeed);

        transform.LookAt(walkpoints[currentZombiePosition].transform.position);
    }
    private void pursuepPlayer()
    {
        if (ZombieAgent.SetDestination(PlayerBody.position))
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", true);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", false);
        }
        else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", true);
        }
    }
    private void AttackPlayer()
    {
        ZombieAgent.SetDestination(transform.position);
        transform.LookAt(Lookpoint);
        if (!previouslyAttack)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(Ray1.position, Vector3.forward, out hitInfo, attackingRadius))
            {
                Debug.Log("attacking" + hitInfo.transform.name);

                MovementScript playerBody = hitInfo.transform.GetComponent<MovementScript>();

                if(playerBody != null)
                {
                    playerBody.PlayerHitDamage(givedamage);
                }
                anim.SetBool("Walking", false);
                anim.SetBool("Running", false);
                anim.SetBool("Attacking", true);
                anim.SetBool("Died", false);

            }
                

            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);
        }
    }
    private void ActiveAttacking()
    {
        previouslyAttack = false;
    }
    public void zombieHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;

        if(presentHealth <= 0)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", true);
            ZombieDie();
        }
        
    }
    private void ZombieDie()
    {
        ZombieAgent.SetDestination(transform.position);
        ZombieSpeed = 0f;
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInAttackingRadius = false;
        playerInvionRadius = false;
        Object.Destroy(gameObject, 5.0f);
    }
}
