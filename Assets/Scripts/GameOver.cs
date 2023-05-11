using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // unlock cursor
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RenderRestart() {
        SceneManager.LoadScene("PlayScene");
    }

    public void RenderQuit() {
        SceneManager.LoadScene("MainMenuScene");
    }
}
