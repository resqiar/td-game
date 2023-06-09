using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class GameManager : MonoBehaviour
{
    public int enemiesRemain = 0;
    public int round = 0;
    public GameObject[] spawns;
    public GameObject[] enemies;

    public TMP_Text waveText;

    private int towerCount; // Number of towers in the game

    void Start()
    {
        // Find all objects with the "tower" tag and count them
        GameObject[] towers = GameObject.FindGameObjectsWithTag("tower");
        towerCount = towers.Length;   
    }

    void FixedUpdate()
    {
        if (enemiesRemain == 0) {
            round++;
            waveText.text = "Wave " + round.ToString();
            SetWave(round);
        }
    }

    void SetWave(int round) {
        // spawn enemies based on the amount of round 
        for (int i = 0; i < round * 2;  i++) {
            // randomly take 1 spawn point to spawn the enemy
            GameObject spawnPoint = spawns[Random.Range(0, spawns.Length)];
            // randomly take 1 enemy to be spawned
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];

            // spawn
            GameObject spawned = Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);

            spawned.GetComponent<EnemyManager>().game = GetComponent<GameManager>();

            enemiesRemain++;
        }
    }

    // Method to decrement tower count
    public void DecrementTowerCount() {
        towerCount--;
        
        if (towerCount <= 0) {
            SceneManager.LoadScene("GameOverScene"); // load game over scene
        }
    }
}
