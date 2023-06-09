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
    public float currentHealth;
    public GameManager game;
    private MoneyManager moneyManager;

    public float killReward = 50f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("tower");
        moneyManager = FindObjectOfType<MoneyManager>();
        agent = GetComponent<NavMeshAgent>();

        // Set stopping distance
        agent.stoppingDistance = 7.0f;

        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.transform.position; // set enemy destination to target
        
        // if the velocity of the agent (enemy) is more than 1, consider it running
        if(agent.velocity.magnitude > 1) {
            // enable running animation
            animator.SetBool("isRunning", true);
            animator.SetBool("isAttacking", false);
        } else {
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", true);

            target.GetComponent<TowerManager>().ApplyDamage(damage);
        }
    }

    public void ApplyDamage(float damage) {
        health -= damage;

        if (health <= 0) {
            // reduce enemies count in the game
            game.enemiesRemain--;

            // apply kill reward / increment money
            moneyManager.AddMoney(killReward);

            // just delete the object
            Destroy(gameObject);
        }
    }
}
