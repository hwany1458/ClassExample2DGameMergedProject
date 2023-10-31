using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSnakeGame()
    {
        SceneManager.LoadScene("SnakeGameScene");
    }

    public void OpenOwlGame()
    {
        SceneManager.LoadScene("JumpingOwlGameScene");
    }

    public void OpenRPSGame()
    {
        SceneManager.LoadScene("RockPaperScissorsGameScene");
    }

    public void CloseGame()
    {
        Application.Quit();
    }    
}
