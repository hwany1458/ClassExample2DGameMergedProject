using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // ----------- Variables
    
    bool isMobile;  // 모바일용인가

    // ---------------------- Methods

    // Start is called before the first frame update
    void Start()
    {
        // Mobile Device인가?
        isMobile = Application.platform == RuntimePlatform.Android ||
                   Application.platform == RuntimePlatform.IPhonePlayer;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSnakeGame()
    {
        //SceneManager.LoadScene("SnakeGameScene");
        SceneManager.LoadScene("SnakeGameScene1");
    }

    public void OpenOwlGame()
    {
        SceneManager.LoadScene("JumpingOwlGameScene");
    }

    public void OpenRPSGame()
    {
        SceneManager.LoadScene("RockPaperScissorsGameScene");
    }

    public void OpenSnakeTwoGame()
    {
        // 테스트에서는 잠깐 열어둔다
        //if(!isMobile) { 
            //SceneManager.LoadScene("SnakeGameScene");
            SceneManager.LoadScene("SnakeGame4TwoPersonScene");
        /*
        } 
        else 
        {
            Debug.Log("PC버전에서만 동작합니다");
        }
        */
    }

    public void CloseGame()
    {
        Application.Quit();
    }    
}
