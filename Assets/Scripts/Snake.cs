using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    float speedMove = 3f; // �̵��ӵ�
    float speedRot = 120f;  // ȸ���ӵ�

    bool isDead = false;  // ��� ���� üũ

    Transform coin;

    List<Transform> tails = new List<Transform>();

    // UI
    GameObject panelOver;
    Text txtCoin;
    Text txtTime;
    int coinCnt = 0;
    float startTime;

    // Joystick
    GameObject panelStick;
    Joystick stick;
    bool isMobile;

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
    void MoveHead()
    {
        // �̵�
        float amount = speedMove * Time.deltaTime;
        transform.Translate(Vector3.forward * amount);

        // ȸ��
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

    // �浹 ������ ó��
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("�浹��ü=" + collision.gameObject.name);
        switch (collision.gameObject.tag)
        {
            case "Coin":
                MoveCoin();
                AddTail();
                break;
            case "Tail":
                //break;
            case "Wall":
                isDead = true;
                panelOver.SetActive(isDead);
                break;
        }
    }

    // Coin �̵�
    void MoveCoin()
    {
        coinCnt++;

        // Coin�� �̵��� ���� - ���� ����
        float x = Random.Range(-9f, 9f);
        float z = Random.Range(-4f, 4f);

        coin.position = new Vector3(x, 0, z);
    }

    // ���� �ʱ�ȭ
    void InitGame()
    {
        coin = GameObject.Find("Coin").transform;

        // UI  ����
        panelOver = GameObject.Find("PanelOver");
        panelOver.SetActive(false);

        txtCoin = GameObject.Find("TxtCoin").GetComponent<Text>();
        txtTime = GameObject.Find("TxtTime").GetComponent<Text>();

        // Mobile Device�ΰ�?
        isMobile = Application.platform == RuntimePlatform.Android ||
                   Application.platform == RuntimePlatform.IPhonePlayer;
        // For Testing
        //isMobile = true;

        panelStick = GameObject.Find("PanelStick");
        panelStick.SetActive(isMobile);
        stick = panelStick.transform.GetChild(0).GetComponent<Joystick>();
    }

    // ���� �߰�
    void AddTail()
    {
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

            tail.position = Vector3.Lerp(tail.position, pos, 4 * Time.deltaTime);
            tail.rotation = Quaternion.Lerp(tail.rotation, rot, 4 * Time.deltaTime);

            target = tail;
        }
    }

    // UI Time
    void SetTime()
    {
        txtCoin.text = coinCnt.ToString("Coin : 0");

        float span = Time.time - startTime;
        int h = Mathf.FloorToInt(span / 3600);
        int m = Mathf.FloorToInt(span / 60 % 60);
        float s = span % 60;

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
                Application.Quit();
                break;
        }
    }

}
