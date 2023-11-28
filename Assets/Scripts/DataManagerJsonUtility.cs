using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 파일 처리 기능을 추가
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

    // 필요한 실질적인 데이터 (객체로부터 제공받음)
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
        // 유니티에 내장된 JsonUtility 사용
        // Json 문자열로 변형하고 싶은 오브젝트를 넘겨줌 (Json 형태로 포맷팅된 문자열이 나옴)        
        // 결과를 문자열로 받음
        string jsonData = JsonUtility.ToJson(gameScore, true);

        // Json 데이터를 파일로 저장
        // 현재 실행중인 유니티 프로젝트 경로에 저장 : Application.dataPath 
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
        // 현재 실행중인 유니티 프로젝트 경로에 저장 : Application.dataPath 
        string readFileName = Path.Combine(Application.dataPath, "SnakeSingleUserScoreJsonUtility.json");

        // JSON 데이터를 가져와서, JSON 데이터로부터 오브젝트를 생성해서 데이터를 써줌
        // 해당경로로부터 문자열 데이터를 읽어서 문자열로 반환
        string jsonData = File.ReadAllText(readFileName);

        // JsonUtility의 FromJson을 통해 JSON으로부터 오브젝트로 Deserialize
        // 오브젝트로 복구시킬 대상을 제네릭으로 명시해야 함
        // (JSON 데이터를 PlayerData 오브젝트로 복구시킴)
        // 출력으로 나온 PlayerData 타입의 오브젝트를 PlayerController가 가졌던 PlayerData로 덮어쓰기
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


// PlayerController 와 PlayerData 를 분리
// (1) 코드 간결성
// 데이터를 가지고 있는 컨테이너(Data Container)와
// 데이터를 가지고 실제로 무언가를 실행하는 컨트롤러(Controller)를 분리되어 있어야 코드가 간결해짐
// (2)
// (위의) PlayerController는 MonoBehaviour에서 상속받는데,
// PlayerController 스크립트를 Serialize화 해서 JSON 파일로 저장하게 되면
// Player 정보 뿐만 아니라 불필요한 MonoBehaviour 정보까지 함께 저장될 수 있음
// Player 정보만 저장할 단순 컨테이너 클래스를 생성

// Player 정보만 저장할 단순 컨테이너 클래스를 생성
// Serialize 되어 있지 않기 때문에 인스팩터에 나타나지 않음 
[System.Serializable]
public class SnakeGameSingleUserScore
{
    // Play Game에서 저장할 데이터를 명시
    public string playerName;
    public string playingDate;
    public int count;
    public int score;
}