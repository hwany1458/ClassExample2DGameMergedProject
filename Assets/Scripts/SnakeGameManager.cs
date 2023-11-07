using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SnakeGameManager : MonoBehaviour
{

    // UI
    GameObject panelOver;
    //Text txtCoin;
    Text txtTime;
    //Text txtSpeed;
    //Text txtScore;

    float startTime;

    /*
     * ����Ͽ����� 2�ο��� �������� �����Ƿ� �����ص� ����
    // Joystick
    GameObject panelStick;
    Joystick stick;
    bool isMobile;
    */

    // 1�и��� �ӵ��� �������� ���� �޾Ƽ�, Snake ��ũ��Ʈ�� �Ѱ��ֱ� ����
    public int setTimeM;

    //------------------- Methods

    private void Awake()
    {
        InitGame();
        startTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetTime();
    }

    //-------------------- user defined function
    // ���� �ʱ�ȭ
    void InitGame()
    {

        // UI ����
        panelOver = GameObject.Find("PanelOver");
        panelOver.SetActive(false);

        txtTime = GameObject.Find("TxtTime").GetComponent<Text>();

        //txtCoin = GameObject.Find("TxtCoin").GetComponent<Text>();
        //txtSpeed = GameObject.Find("TxtSpeed").GetComponent<Text>();
        //txtScore = GameObject.Find("TxtScore").GetComponent<Text>();

        /*
         * ����Ͽ����� 2�ο��� �������� �����Ƿ� �����ص� ����
        // Mobile Device�ΰ�?
        isMobile = Application.platform == RuntimePlatform.Android ||
                   Application.platform == RuntimePlatform.IPhonePlayer;
        // For Testing
        //isMobile = true;

        panelStick = GameObject.Find("PanelStick");
        panelStick.SetActive(isMobile);
        stick = panelStick.transform.GetChild(0).GetComponent<Joystick>();
        */
    }

    // UI Time
    void SetTime()
    {
        float span = Time.time - startTime;
        int h = Mathf.FloorToInt(span / 3600);
        int m = Mathf.FloorToInt(span / 60 % 60);
        float s = span % 60;

        setTimeM = m;

        /*
        txtCoin.text = coinCnt.ToString("Count : 0");
        txtSpeed.text = speedMove.ToString("Speed : 0.0");
        txtScore.text = "Score : " + score.ToString();

        newMin = m;

        // Time �� Speed Setting(time����)�� ���� �̵� �ӵ�(speedMove)�� ����
        if ((oldMin != newMin) && (isSetSpeed == 2))
        {
            speedMove += 0.5f;
            oldMin = newMin;
        }
        */

        txtTime.text = string.Format("Time: {0:0}:{1:0}:{2:00.0}", h, m, s);
    }

    // Button Click
    public void OnButtonClick(Button button)
    {
        switch (button.name)
        {
            case "BtnYes":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "BtnNo":
                //Application.Quit();
                SceneManager.LoadScene("MainMenuScene");
                break;
        }
    }

    public void GameOver(bool isDead)
    {
        panelOver.SetActive(isDead);
    }

}
