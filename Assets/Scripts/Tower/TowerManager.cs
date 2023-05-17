using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerManager : MonoBehaviour
{
    public GameManager game;
    public GameObject firePrefab;
    public GameObject target;

    public float maxHealth = 100f; // Maximum health of the tower
    private float currentHealth; // Current health of the tower

    public float damage = 25f; // Cooldown duration in seconds
    public float damageCooldown = 1f; // Cooldown duration in seconds
    private bool canTakeDamage = true; // Flag to control damage cooldown

    public float attackTimer = 100f;
    public float attackDamage = 25f;

    public float attackCooldown = 1f;
    public float attackRadius = 12f;

    private AudioSource audio;
    public AudioClip fireballShootAudio;

    public TMP_Text ttext;

    void Start() {
        currentHealth = maxHealth; // Set the initial health to the maximum
        game = FindObjectOfType<GameManager>();
        target = GameObject.FindGameObjectWithTag("enemy");
        audio = GetComponent<AudioSource>();
    }

    void Update() {
        // Find all enemies within the attack radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius);

        // Select the closest enemy as the target
        float closestDistance = float.MaxValue;
        GameObject closestEnemy = null;

        foreach (Collider collider in colliders) {
            if (collider.gameObject.CompareTag("enemy")) {
                // Calculate distance between tower and enemy
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                if (distance < closestDistance) {
                    closestDistance = distance;
                    closestEnemy = collider.gameObject;
                }
            }
        }


        // Set the closest enemy as the target
        target = closestEnemy;

        // Attack the target if it exists and the attack cooldown has elapsed
        if (target != null && attackTimer <= 0f)
        {
            AttackTarget();

            // play fireball shoot audio 
            audio.volume = 1f;
            audio.PlayOneShot(fireballShootAudio);
            audio.volume = 0.5f;

            attackTimer = attackCooldown; // Reset the attack cooldown timer
        }

        // Update the attack cooldown timer
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    public void AttackTarget() {
        // Check if a valid target exists
        if (target != null)
        {
            // Instantiate the fire prefab
            GameObject fireInstance = Instantiate(firePrefab, transform.position + Vector3.up * 10, Quaternion.identity);

            // Calculate the direction towards the target
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // Get the fireball component
            Fireball fireball = fireInstance.GetComponent<Fireball>();

            // Set the target and launch the fireball
            fireball.SetTarget(target.transform);
            fireball.SetDamage(attackDamage);
            fireball.Launch(direction);

            // Reset the attack cooldown timer
            attackTimer = attackCooldown;
        }
    } 

    public void ApplyDamage(float damage){
        if (!canTakeDamage) return;

        currentHealth -= damage; 

        if (currentHealth <= 0) {
            ttext.text = "T 0";
            DestroyTower(); // If the tower's health is depleted, destroy it
            game.DecrementTowerCount();
        } else {
            ttext.text = "T " + currentHealth.ToString();
            StartCoroutine(DamageCooldown());
        }
    }

    private void DestroyTower() {
        Destroy(gameObject); // Destroy the tower game object
    }

    private IEnumerator DamageCooldown() {
        canTakeDamage = false; // Disable damage while in cooldown
        yield return new WaitForSeconds(damageCooldown); // Wait for the cooldown duration
        canTakeDamage = true; // Enable damage after cooldown
    }
}