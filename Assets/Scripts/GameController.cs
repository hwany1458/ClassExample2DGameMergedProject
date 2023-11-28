using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    // ----------- Variables
    public TMP_Text displayMessage;
    bool isMobile;  // ����Ͽ��ΰ�
    //public int isSetSpeed = 1;  // ����� ���ǵ���� ������  1 (Coin=item), 2 (Time)

    // ---------------------- Methods

    // Start is called before the first frame update
    void Start()
    {
        // Mobile Device�ΰ�?
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
        // �׽�Ʈ������ ��� ����д�
        if(!isMobile) { 
            //SceneManager.LoadScene("SnakeGameScene");
            SceneManager.LoadScene("SnakeGame4TwoPersonScene");
        } 
        else 
        {
            //Debug.Log("PC���������� �����մϴ�");
            displayMessage.text = "PC���������� �����մϴ�";
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }    
}
