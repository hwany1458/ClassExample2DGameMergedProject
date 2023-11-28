using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    // ----------- Variables
    public TMP_Text displayMessage;
    bool isMobile;  // 모바일용인가
    //public int isSetSpeed = 1;  // 뱀게임 스피드향상 설정값  1 (Coin=item), 2 (Time)

    // ---------------------- Methods

    // Start is called before the first frame update
    void Start()
    {
        // Mobile Device인가?
        isMobile = Application.platform == RuntimePlatform.Android ||
                   Application.platform == RuntimePlatform.IPhonePlayer;
        displayMessage.text = "Start the game, enjoy it ";
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
        if(!isMobile) { 
            //SceneManager.LoadScene("SnakeGameScene");
            SceneManager.LoadScene("SnakeGame4TwoPersonScene");
        } 
        else 
        {
            //Debug.Log("PC버전에서만 동작합니다");
            displayMessage.text = "PC버전에서만 동작합니다";
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }    
}
