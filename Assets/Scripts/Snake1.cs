using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Snake1 : MonoBehaviour
{
    public float speedMove = 2.5f; // 이동속도
    float speedRot = 120f;  // 회전속도

    bool isDead = false;  // 사망 여부 체크

    Transform coin;
    Transform plusItem;
    Transform minusItem;

    List<Transform> tails = new List<Transform>();

    // UI
    GameObject panelOver;
    Text txtCoin;
    Text txtTime;
    Text txtSpeed;
    Text txtScore;

    int coinCnt = 0;
    float startTime;

    // Joystick
    GameObject panelStick;
    Joystick stick;
    bool isMobile;

    // Game Controll
    // 속도 셋팅은 설정 화면 같은 곳에서 저장하도록 수정 필요
    int isSetSpeed = 1;  // 1 (Coin), 2 (Time)

    int oldMin = 0, newMin = 0;
    int score = 0;

    AudioSource[] soundEffect;  // 0 (Get item), 1 (Game over)

    // ------------------ methods
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
        if (!isDead) 
        { 
            MoveHead();
            MoveTail();
            SetTime();
        }

    }

    //-------------------- user defined function
    // 게임 초기화
    void InitGame()
    {
        coin = GameObject.Find("Coin").transform;
        plusItem = GameObject.Find("Hexgon").transform;
        minusItem = GameObject.Find("Cuboid").transform;

        // UI  위젯
        panelOver = GameObject.Find("PanelOver");
        panelOver.SetActive(false);

        txtCoin = GameObject.Find("TxtCoin").GetComponent<Text>();
        txtTime = GameObject.Find("TxtTime").GetComponent<Text>();
        txtSpeed = GameObject.Find("TxtSpeed").GetComponent<Text>();
        txtScore = GameObject.Find("TxtScore").GetComponent<Text>();

        // Mobile Device인가?
        isMobile = Application.platform == RuntimePlatform.Android ||
                   Application.platform == RuntimePlatform.IPhonePlayer;
        // For Testing
        //isMobile = true;

        panelStick = GameObject.Find("PanelStick");
        panelStick.SetActive(isMobile);
        stick = panelStick.transform.GetChild(0).GetComponent<Joystick>();

        // Audio sound 가져오기
        // 현재 뱀에 2개의 AudioSound가 걸려 있음 - 배열로 선언하고 GetComponents()로 가져옴
        soundEffect = GetComponents<AudioSource>();
    }

    void MoveHead()
    {
        // 이동
        float amount = speedMove * Time.deltaTime;
        transform.Translate(Vector3.forward * amount);

        // 회전
        //amount = Input.GetAxis("Horizontal") * speedRot;
        if (!isMobile)
        {
            amount = Input.GetAxis("Horizontal") * speedRot;
        }
        else
        {
            amount = stick.Horizontal() * speedRot;
        }
        transform.Rotate(Vector3.up * amount * Time.deltaTime);
    }

    // 충돌 판정와 처리
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("충돌물체=" + collision.gameObject.name);
        switch (collision.gameObject.tag)
        {
            // 뱀(Snake)과 충돌체가 무엇인가에 따라
            /*
            case "Coin":
                GetCoinAndMoveIt();
                //MoveCoin();
                AddTail();
                break;
            case "ItemPlus":
                GetPlusItemAndMoveIt();
                break;
            case "ItemMinus":
                GetMinusItemAndMoveIt();
                break;
            */
            case "Tail":
                //break;
            case "Wall":
                isDead = true;
                soundEffect[1].Play();  // 게임오버 사운드
                panelOver.SetActive(isDead);  // 게임오버되어 화면 띄움
                break;
        }
    }

    // OnCollisionEnter() 에서 OnTriggerEnter() 로 변경
    // 충돌 판정 및 처리 (IsTrigger checked)
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("트리거 충돌물체 :" + other.tag);
        switch(other.tag)
        {
            case "Coin":
                GetCoinAndMoveIt();
                //MoveCoin();
                AddTail();
                break;
            case "ItemPlus":
                GetPlusItemAndMoveIt();
                AddTail();
                break;
            case "ItemMinus":
                GetMinusItemAndMoveIt();
                break;

            /*
            case "Tail":
            //break;
            case "Wall":
                isDead = true;
                soundEffect[1].Play();  // 게임오버 사운드
                panelOver.SetActive(isDead);  // 게임오버되어 화면 띄움
                break;
            */
        }
    }

    // Coin 이동 (MoveCoin)
    //void MoveCoin()
    Vector3 MoveCoin()
    {
        // 코인 카운트를 증가
        coinCnt++;
        soundEffect[0].Play();

        // Coin count와 Speed Setting(coin으로)에 따라 이동 속도(speedMove)를 증가
        if (((coinCnt % 3) == 0) && (isSetSpeed == 1))
        {
            speedMove += 0.5f;
        }

        // Coin을 이동할 범위 - 벽의 안쪽
        float x = Random.Range(-9f, 9f);
        float z = Random.Range(-4f, 4f);

        //coin.position = new Vector3(x, 0, z);
        return new Vector3(x, 0, z);
    }

    void GetCoinAndMoveIt()
    {
        score += 5;

        Vector3 location = MoveCoin();
        coin.position = location;
    }

    void GetPlusItemAndMoveIt()
    {
        score += 20;

        Vector3 location = MoveCoin();
        plusItem.position = location;
    }

    void GetMinusItemAndMoveIt()
    {
        score -= 10;
        Vector3 location = MoveCoin();
        minusItem.position = location;
    }

    // 꼬리 추가
    void AddTail()
    {
        // (붙일) 꼬리 오브젝트 생성, 위치를 pos로 받아냄
        GameObject tail = Instantiate(Resources.Load("Tail")) as GameObject;
        Vector3 pos = transform.position;

        // 꼬리의 개수 구하기
        int cnt = tails.Count;

        if (cnt == 0) 
        { 
            tail.tag = "Untagged"; 
        }
        else 
        { 
            pos = tails[cnt - 1].position; 
        }

        tail.transform.position = pos;

        // 꼬리 색
        Color[] colors = { new Color(0, 0.5f, 0, 1), new Color(0, 0.5f, 1, 1) };
        int n = cnt / 3 % 2;
        tail.GetComponent<Renderer>().material.color = colors[n];

        tails.Add(tail.transform);
    }

    // 꼬리 이동 및 회전
    void MoveTail()
    {
        Transform target = transform;
        foreach (Transform tail in tails)
        {
            Vector3 pos = target.position;
            Quaternion rot = target.rotation;

            /* 꼬리가 붙으면서 속도가 빨라지면, 꼬리가 떨어지는 느낌이 있음
             * 이를 수정하려고 고정(?) 수치를 변경
            tail.position = Vector3.Lerp(tail.position, pos, 4 * Time.deltaTime);
            tail.rotation = Quaternion.Lerp(tail.rotation, rot, 4 * Time.deltaTime);
            */

            tail.position = Vector3.Lerp(tail.position, pos, (speedMove+1) * Time.deltaTime);
            tail.rotation = Quaternion.Lerp(tail.rotation, rot, (speedMove + 1) * Time.deltaTime);

            target = tail;
        }
    }

    // UI Time
    void SetTime()
    {
        txtCoin.text = coinCnt.ToString("Count : 0");
        txtSpeed.text = speedMove.ToString("Speed : 0.0");
        txtScore.text = "Score : " + score.ToString();

        float span = Time.time - startTime;
        int h = Mathf.FloorToInt(span / 3600);
        int m = Mathf.FloorToInt(span / 60 % 60);
        float s = span % 60;
        
        newMin = m;
        
        // Time 과 Speed Setting(time으로)에 따라 이동 속도(speedMove)를 증가
        if ((oldMin != newMin) && (isSetSpeed == 2))
        {
            speedMove += 0.5f;
            oldMin = newMin;
        }

        txtTime.text = string.Format("Time: {0:0}:{1:0}:{2:00.0}", h, m, s);
    }

    // Button Click
    public void OnButtonClick(Button button)
    {
        switch (button.name)
        {
            case "BtnYes" :
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
                break;
            case "BtnNo":
                //Application.Quit();
                SceneManager.LoadScene("MainMenuScene");
                break;
        }
    }

}
