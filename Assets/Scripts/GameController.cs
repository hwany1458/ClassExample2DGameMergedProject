using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // ----------- Variables
    
    bool isMobile;  // ����Ͽ��ΰ�

    // ---------------------- Methods

    // Start is called before the first frame update
    void Start()
    {
        // Mobile Device�ΰ�?
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
        // �׽�Ʈ������ ��� ����д�
        //if(!isMobile) { 
            //SceneManager.LoadScene("SnakeGameScene");
            SceneManager.LoadScene("SnakeGame4TwoPersonScene");
        /*
        } 
        else 
        {
            Debug.Log("PC���������� �����մϴ�");
        }
        */
    }

    public void CloseGame()
    {
        Application.Quit();
    }    
}
