using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// newtonsoft JSON 관련 기능을 사용하기 위해
using Newtonsoft.Json;
using System.IO;
using System;

public class DataManagerNewtonSoftJson : MonoBehaviour
{
    //-------------- Variables
    public InputField inputName;
    public InputField inputCount;
    public InputField inputScore;

    string inputStrName;
    string inputStrCount;
    string inputStrScore;

    public TMP_Text displayMessage;

    // 필요한 실질적인 데이터 (객체로부터 제공받음)
    public SnakeGameSingleUserScoreNewtonSoftJson jsonGameScore;
    //public JsonDataClass jsonData;

    //---------------- Methods
    // Start is called before the first frame update
    void Start()
    {
        // Object to JSON
        jsonGameScore = new SnakeGameSingleUserScoreNewtonSoftJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //----------------- Save button
    public void Save()
    {
        AddData(inputStrName, inputStrCount, inputStrScore);
        WriteToJsonFileNewtonSoftJson();

        //Debug.Log("Oked. saved successfully using NewtonSoft Json");
        displayMessage.text = "Oked. saved successfully using NewtonSoft Json";
        ClearData();
    }

    private void AddData(string n, string c, string s)
    {
        jsonGameScore.playerName = n;
        jsonGameScore.playingDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        jsonGameScore.count = int.Parse(c);
        jsonGameScore.score = int.Parse(s);
    }

    // 문자열로 만든 Json 데이터를 파일로 저장
    private void WriteToJsonFileNewtonSoftJson()
    {
        string savingObjectToJson = JsonConvert.SerializeObject(jsonGameScore);
        string savingJsonFileName = Path.Combine(Application.dataPath, "SnakeSingleUserScoreNewtonSoftJson.json");

        File.WriteAllText(savingJsonFileName, savingObjectToJson);
    }

    
    //--------------- Load button
    public void Load()
    {
        ReadFromJsonFileNewtonSoftJson();
    }

    // 저장된 JSON 파일을 읽어서 오브젝트로 변환
    //JsonDataClass ReadFromJsonFile()
    private void ReadFromJsonFileNewtonSoftJson()
    {
        string jsonFileName = Path.Combine(Application.dataPath, "SnakeSingleUserScoreNewtonSoftJson.json");
        string readJsonData = File.ReadAllText(jsonFileName);

        SnakeGameSingleUserScoreNewtonSoftJson createObjectFromJson = JsonConvert.DeserializeObject<SnakeGameSingleUserScoreNewtonSoftJson>(readJsonData);

        //createObjectFromJson.PrintData();
        string readData = createObjectFromJson.playerName + " " + createObjectFromJson.playingDate + " " + createObjectFromJson.count + " " + createObjectFromJson.score;
        Debug.Log(readData);
        displayMessage.text = "Read from Json File with NewtonSoft: " + readData;
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

[System.Serializable]
public class SnakeGameSingleUserScoreNewtonSoftJson
{
    // Play Game에서 저장할 데이터를 명시
    public string playerName;
    public string playingDate;
    public int count;
    public int score;
}