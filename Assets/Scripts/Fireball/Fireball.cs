using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f; // Speed at which the fireball moves
    public float lifetime = 3f; // Time before the fireball is destroyed
    private float damage = 25f; // Amount of damage to inflict on the enemy
    private Transform target; // Reference to the target enemy

    // Start is called before the first frame update
    void Start()
    {
       // Destroy the fireball after the specified lifetime
        Destroy(gameObject, lifetime); 
    }

    // Update is called once per frame
    void Update()
    {
         // Move the fireball towards the target
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }    
    }

    public void SetTarget(Transform enemyTransform) {
        target = enemyTransform;
    }

    public void SetDamage(float attackDamage) {
        damage = attackDamage;
    }

    public void Launch(Vector3 direction) {
        transform.forward = direction; // Orient the fireball in the launch direction
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the fireball collided with an enemy
        if (collision.gameObject.CompareTag("enemy")) {
            // Apply damage to the enemy
            EnemyManager enemyManager = collision.gameObject.GetComponent<EnemyManager>();

            if (enemyManager != null) {
                enemyManager.ApplyDamage(damage);
            }

            // Destroy the fireball on collision with an enemy
            Destroy(gameObject);
        }
    }
}
