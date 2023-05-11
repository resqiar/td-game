using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public GameObject target;
    public Animator animator;
    private NavMeshAgent agent;
    public float damage = 20f;
    public float health = 200f;
    public GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.transform.position; // set enemy destination to target
        
        // if the velocity of the agent (enemy) is more than 1, consider it running
        if(agent.velocity.magnitude > 1) {
            // enable running animation
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target) {
            // apply damage to target
            target.GetComponent<PlayerManager>().ApplyDamage(damage);
        }
    }

    public void ApplyDamage(float damage) {
        health -= damage;

        if (health <= 0) {
            // reduce enemies count in the game
            game.enemiesRemain--;

            // just delete the object
            Destroy(gameObject);
        }
    }
}
