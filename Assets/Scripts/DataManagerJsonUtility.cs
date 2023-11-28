using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ���� ó�� ����� �߰�
using System.IO;
using System;  // DateTime

public class DataManagerJsonUtility : MonoBehaviour
{
    //-------------- Variables
    public InputField inputName;
    public InputField inputCount;
    public InputField inputScore;

    string inputStrName;
    string inputStrCount;
    string inputStrScore;

    public TMP_Text displayMessage;

    // �ʿ��� �������� ������ (��ü�κ��� ��������)
    public SnakeGameSingleUserScore gameScore;

    //-------------- Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //---------------- Save button
    public void Save()
    {
        if (CheckData())
        {
            AddData(inputStrName, inputStrCount, inputStrScore);
            WriteToJsonFileJsonUtility();

            //Debug.Log("Oked. saved successfully");
            displayMessage.text = "Oked. saved successfully";
            ClearData();
        }
        else
        {
            //Debug.Log("[ERROR] User input is NOT enough to save");
            displayMessage.text = "[ERROR] User input is NOT enough to save";
        }
    }

    private void AddData(string n, string c, string s)
    {
        gameScore.playerName = n;
        gameScore.playingDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        gameScore.count = int.Parse(c);
        gameScore.score = int.Parse(s);
    }

    private void WriteToJsonFileJsonUtility()
    {
        // ����Ƽ�� ����� JsonUtility ���
        // Json ���ڿ��� �����ϰ� ���� ������Ʈ�� �Ѱ��� (Json ���·� �����õ� ���ڿ��� ����)        
        // ����� ���ڿ��� ����
        string jsonData = JsonUtility.ToJson(gameScore, true);

        // Json �����͸� ���Ϸ� ����
        // ���� �������� ����Ƽ ������Ʈ ��ο� ���� : Application.dataPath 
        string savingFileName = Path.Combine(Application.dataPath, "SnakeSingleUserScoreJsonUtility.json");
        File.WriteAllText(savingFileName, jsonData);

        //Debug.Log("Oked. Game score is saved to Json file successfully");
        displayMessage.text = "Oked. Game score is saved to Json file successfully";
        ClearData();
    }

    //-------------- Load button
    public void Load()
    {
        ReadFromJsonFileJsonUtility();
    }

    private void ReadFromJsonFileJsonUtility()
    {
        // ���� �������� ����Ƽ ������Ʈ ��ο� ���� : Application.dataPath 
        string readFileName = Path.Combine(Application.dataPath, "SnakeSingleUserScoreJsonUtility.json");

        // JSON �����͸� �����ͼ�, JSON �����ͷκ��� ������Ʈ�� �����ؼ� �����͸� ����
        // �ش��ηκ��� ���ڿ� �����͸� �о ���ڿ��� ��ȯ
        string jsonData = File.ReadAllText(readFileName);

        // JsonUtility�� FromJson�� ���� JSON���κ��� ������Ʈ�� Deserialize
        // ������Ʈ�� ������ų ����� ���׸����� ����ؾ� ��
        // (JSON �����͸� PlayerData ������Ʈ�� ������Ŵ)
        // ������� ���� PlayerData Ÿ���� ������Ʈ�� PlayerController�� ������ PlayerData�� �����
        gameScore = JsonUtility.FromJson<SnakeGameSingleUserScore>(jsonData);

        //Debug.Log("Read from Json: " + gameScore.playerName + " " + gameScore.count + " " + gameScore.score);
        displayMessage.text = "Read from Json: " + gameScore.playerName + " " + gameScore.playingDate + " " + gameScore.count + " " + gameScore.score;
    }

    //----------------------- Set
    public void ChangedInputName(Text changedString)
    {
        inputStrName = changedString.text;
    }

    public void ChangedInputCount(Text changedString)
    {
        inputStrCount = changedString.text;
    }

    public void ChangedInputScore(Text changedString)
    {
        inputStrScore = changedString.text;
    }

    //------------- Check whether inputField has value or not
    private bool CheckData()
    {
        if ((inputStrName != null) && (inputStrCount != null) && (inputStrScore != null) &&
            (inputStrName != "") && (inputStrCount != "") && (inputStrScore != ""))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ClearData()
    {
        inputName.text = "";
        inputCount.text = "";
        inputScore.text = "";
    }
}


// PlayerController �� PlayerData �� �и�
// (1) �ڵ� ���Ἲ
// �����͸� ������ �ִ� �����̳�(Data Container)��
// �����͸� ������ ������ ���𰡸� �����ϴ� ��Ʈ�ѷ�(Controller)�� �и��Ǿ� �־�� �ڵ尡 ��������
// (2)
// (����) PlayerController�� MonoBehaviour���� ��ӹ޴µ�,
// PlayerController ��ũ��Ʈ�� Serializeȭ �ؼ� JSON ���Ϸ� �����ϰ� �Ǹ�
// Player ���� �Ӹ� �ƴ϶� ���ʿ��� MonoBehaviour �������� �Բ� ����� �� ����
// Player ������ ������ �ܼ� �����̳� Ŭ������ ����

// Player ������ ������ �ܼ� �����̳� Ŭ������ ����
// Serialize �Ǿ� ���� �ʱ� ������ �ν����Ϳ� ��Ÿ���� ���� 
[System.Serializable]
public class SnakeGameSingleUserScore
{
    // Play Game���� ������ �����͸� ���
    public string playerName;
    public string playingDate;
    public int count;
    public int score;
}