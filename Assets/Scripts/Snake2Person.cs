using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Snake2Person : MonoBehaviour
{
    public float speedMove = 2.5f; // �̵��ӵ�
    float speedRot = 150f;  // ȸ���ӵ�

    bool isDead = false;  // ��� ���� üũ

    List<Transform> tails = new List<Transform>();

    Transform coin;
    Transform plusItem;
    Transform minusItem;

    Text txtCoin;
    Text txtSpeed;
    Text txtScore;

    /*  SnakeGameManager�� �̵�
    // UI
    GameObject panelOver;
    Text txtTime;

    float startTime;

    // Joystick (����Ͽ����� 2�ο��� �������� �����Ƿ� �����ص� ����)
    GameObject panelStick;
    bool isMobile;
    Joystick stick;
    */


    int coinCnt = 0;

    // Game Controll
    int isSetSpeed = 1;  // 1 (Coin), 2 (Time)
    int oldMin = 0, newMin = 0;
    int score = 0;

    AudioSource[] soundEffect;  // 0 (Get item), 1 (Game over)

    // 2�ο�����
    public GameObject snakeGameManager;
    public int playerNum = 1;  // �� ��ȣ
    string movAxisName;  // �̵����� �̸� ����
    string rotAxisName;  // ȸ������ �̸� ����
    string txtCoinName;
    string txtSpeedName;
    string txtScoreName;

    // ------------------ methods
    private void Awake() 
    { 
        InitGame();
        // SnakeGameManager�� �̵�
        //startTime = Time.time;
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
    // ���� �ʱ�ȭ
    void InitGame()
    {
        // 2�ο�����
        snakeGameManager = GameObject.Find("GameManager");
        movAxisName = "Vertical" + playerNum;
        rotAxisName = "Horizontal" + playerNum;

        coin = GameObject.Find("Coin").transform;
        plusItem = GameObject.Find("Hexgon").transform;
        minusItem = GameObject.Find("Cuboid").transform;

        txtCoinName = "TxtCoin" + playerNum;
        txtSpeedName = "TxtSpeed" + playerNum;
        txtScoreName = "TxtScore" + playerNum;
        txtCoin = GameObject.Find(txtCoinName).GetComponent<Text>();
        txtSpeed = GameObject.Find(txtSpeedName).GetComponent<Text>();
        txtScore = GameObject.Find(txtScoreName).GetComponent<Text>();

        /* SnakeGameManager �� �̵�
        // UI  ����
        panelOver = GameObject.Find("PanelOver");
        panelOver.SetActive(false);

        txtTime = GameObject.Find("TxtTime").GetComponent<Text>();

        //----- ����Ͽ����� 2�ο��� �������� �����Ƿ� �����ص� ����
        // Mobile Device�ΰ�?
        isMobile = Application.platform == RuntimePlatform.Android ||
                   Application.platform == RuntimePlatform.IPhonePlayer;
        // For Testing
        //isMobile = true;

        panelStick = GameObject.Find("PanelStick");
        panelStick.SetActive(isMobile);
        stick = panelStick.transform.GetChild(0).GetComponent<Joystick>();
        */

        // Audio sound ��������
        // ���� �쿡 2���� AudioSound�� �ɷ� ���� - �迭�� �����ϰ� GetComponents()�� ������
        soundEffect = GetComponents<AudioSource>();

    }

    void MoveHead()
    {
        // �̵�
        float amount = speedMove * Time.deltaTime;
        transform.Translate(Vector3.forward * amount);

        // ȸ��
        //amount = Input.GetAxis("Horizontal") * speedRot;
        /* ����Ͽ����� 2�ο��� �������� �����Ƿ� �����ص� ����
        if (!isMobile)
        {
            //amount = Input.GetAxis("Horizontal") * speedRot;
            // 2�ο�����
            amount = Input.GetAxis(rotAxisName) * speedRot;
        }
        else
        {
            amount = stick.Horizontal() * speedRot;
        }
        */

        amount = Input.GetAxis(rotAxisName) * speedRot;
        transform.Rotate(Vector3.up * amount * Time.deltaTime);
    }

    // �浹 ������ ó��
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("�浹��ü=" + collision.gameObject.name);
        switch (collision.gameObject.tag)
        {
            // ��(Snake)�� �浹ü�� �����ΰ��� ����
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
                soundEffect[1].Play();  // ���ӿ��� ����

                //panelOver.SetActive(isDead);  // ���ӿ����Ǿ� ȭ�� ���
                // 2�ο����� ����
                snakeGameManager.SendMessage("GameOver", isDead);
                break;
        }
    }

    // OnCollisionEnter() ���� OnTriggerEnter() �� ����
    // �浹 ���� �� ó�� (IsTrigger checked)
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Ʈ���� �浹��ü :" + other.tag);
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
                soundEffect[1].Play();  // ���ӿ��� ����
                panelOver.SetActive(isDead);  // ���ӿ����Ǿ� ȭ�� ���
                break;
            */
        }
    }

    // Coin �̵� (MoveCoin)
    //void MoveCoin()
    Vector3 MoveCoin()
    {
        // ���� ī��Ʈ�� ����
        coinCnt++;
        soundEffect[0].Play();

        // Coin count�� Speed Setting(coin����)�� ���� �̵� �ӵ�(speedMove)�� ����
        if (((coinCnt % 3) == 0) && (isSetSpeed == 1))
        {
            speedMove += 0.5f;
        }

        // Coin�� �̵��� ���� - ���� ����
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

    // ���� �߰�
    void AddTail()
    {
        // (����) ���� ������Ʈ ����, ��ġ�� pos�� �޾Ƴ�
        GameObject tail = Instantiate(Resources.Load("Tail")) as GameObject;
        Vector3 pos = transform.position;

        // ������ ���� ���ϱ�
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

        // ���� ��
        Color[] colors = { new Color(0, 0.5f, 0, 1), new Color(0, 0.5f, 1, 1) };
        int n = cnt / 3 % 2;
        tail.GetComponent<Renderer>().material.color = colors[n];

        tails.Add(tail.transform);
    }

    // ���� �̵� �� ȸ��
    void MoveTail()
    {
        Transform target = transform;
        foreach (Transform tail in tails)
        {
            Vector3 pos = target.position;
            Quaternion rot = target.rotation;

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

        /* 2�ο����� �̵�
        float span = Time.time - startTime;
        int h = Mathf.FloorToInt(span / 3600);
        int m = Mathf.FloorToInt(span / 60 % 60);
        float s = span % 60;
        */

        //newMin = m;
        // 2�ο����� ����
        newMin = snakeGameManager.GetComponent<SnakeGameManager>().setTimeM;

        // Time �� Speed Setting(time����)�� ���� �̵� �ӵ�(speedMove)�� ����
        if ((oldMin != newMin) && (isSetSpeed == 2))
        {
            speedMove += 0.5f;
            oldMin = newMin;
        }

        // 2�ο����� �̵�
        //txtTime.text = string.Format("Time: {0:0}:{1:0}:{2:00.0}", h, m, s);
    }

    /* 2�ο����� �̵�
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
    */
}
