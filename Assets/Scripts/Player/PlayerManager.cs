using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public float health = 100f;
    public TMP_Text htext;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float damage) {
        health -= damage;

        if (health <= 0) {
            SceneManager.LoadScene("GameOverScene"); // load game over scene
            htext.text = "H 0";
        } else {
            htext.text = "H " + health.ToString();
        }
    }
}
