using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CryptoBehaviour : MonoBehaviour
{
    public enum CryptoState
    {
        IDLE,
        RUN,
        JUMP,
        KICK
    }

    [Header("Line of Sight")]
    public bool HasLOS;
    public GameObject player;
    private NavMeshAgent agent;
    private Animator animator;

    [Header("Attack")]
    public float attackDistance;
    public PlayerBehaviour playerBehaviour;
    public float damageDelay = 1f;
    public bool isAttacking = false;
    public float kickForce = 0.001f;
    public float distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();    
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(HasLOS){
            agent.SetDestination(player.transform.position);
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if(distanceToPlayer < attackDistance && !isAttacking){
                animator.SetInteger("AnimState", (int) CryptoState.KICK); 
                transform.LookAt(transform.position - player.transform.forward);
                DoKickDamage();
                isAttacking = true;
                
                if(agent.isOnOffMeshLink){
                    animator.SetInteger("AnimState", (int) CryptoState.JUMP);
                }     
            }
            else if(distanceToPlayer > attackDistance ){
                animator.SetInteger("AnimState", (int) CryptoState.RUN); 
                isAttacking = false;
            }
        }
        else{
            animator.SetInteger("AnimState", (int) CryptoState.IDLE); 
        }

        

    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            HasLOS = true;
            player = other.transform.gameObject;
        }
    }

    private void DoKickDamage(){
        //yield return new WaitForSeconds(damageDelay);
        playerBehaviour.TakeDamage(20);

        StartCoroutine(kickBack());


    }

    private IEnumerator kickBack(){
        yield return new WaitForSeconds(0.5f);
        var direction = Vector3.Normalize(player.transform.position - transform.position);
        playerBehaviour.controller.SimpleMove(direction* kickForce);
        StopCoroutine(kickBack());
    }

    // private void OnTriggerExit(Collider other) {
    //     if(other.gameObject.CompareTag("Player")){
    //         HasLOS = false;
    //     }
    // }

    // private void OnTriggerStay(Collider other) {
    //     if(other.gameObject.CompareTag("Player")){
    //         player = other.transform.gameObject;
    //     }
        
    // }

}
