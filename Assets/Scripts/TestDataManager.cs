using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;
using System;
using UnityEngine.UI;

public class TestDataManager : MonoBehaviour
{
    People p1 = new People("이용환", 50);
    List<People> peoples = new List<People>();
    Dictionary<string, People> peopleDic = new Dictionary<string, People>();
    Dictionary<string, int> playerScore = new Dictionary<string, int>();

    public TMP_Text textFromJson;
    List<SnakeGamePlayerScore> snakePlayerScore = new List<SnakeGamePlayerScore>();
    public string pName;
    public string pCount;
    public string pScore;

    // Start is called before the first frame update
    void Start()
    {
        //TestJson();
        /*
        snakePlayerScore.Add(new SnakeGamePlayerScore("YongHwan", DateTime.Now, 10, 130));
        snakePlayerScore.Add(new SnakeGamePlayerScore("HyunSoon", DateTime.Now, 15, 300));
        snakePlayerScore.Add(new SnakeGamePlayerScore("HyunSeo", DateTime.Now, 30, 450));
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TestJson()
    {
        //(1)
        string jData1 = JsonConvert.SerializeObject(p1);
        print(jData1);

        //(2)
        peoples.Add(new People("HyunSoon", 20));
        peoples.Add(new People("HyunSeo", 16));
        peoples.Add(new People("KyoungHwa", 45));

        string jData2 = JsonConvert.SerializeObject(peoples);
        print(jData2);

        //(3)
        peopleDic["p1"] = new People("YongHwan", 50);
        peopleDic["p2"] = new People("HyunSoon", 20);
        peopleDic["p3"] = new People("HyunSeo", 16);

        string jData3 = JsonConvert.SerializeObject(peopleDic);
        print(jData3);

        //(4) 
        playerScore["HyunSeo"] = 95;
        playerScore["HyunSoon"] = 100;
        playerScore["KyoungHwa"] = 80;
        playerScore["YongHwan"] = 85;

        string jData4 = JsonConvert.SerializeObject(playerScore);
        print(jData4);
    }

    public void SaveDataToJson()
    {
        string jData = JsonConvert.SerializeObject(snakePlayerScore);
        string jsonFileName = Path.Combine(Application.dataPath, "SnakeScore.json");
        File.WriteAllText(jsonFileName, jData);
    }

    public void AppendDataToJson()
    {
        // 기존 데이타를 가져온 다음,
        string jsonFileName = Path.Combine(Application.dataPath, "SnakeScore.json");
        string jData = File.ReadAllText(jsonFileName);
        snakePlayerScore = JsonConvert.DeserializeObject<List<SnakeGamePlayerScore>>(jData);

        // 새로운 데이터를 추가해서 저장
        //snakePlayerScore.Add(new SnakeGamePlayerScore("aa", DateTime.Now, 11, 22));

        int pc = Int32.Parse(pCount);
        int ps = Int32.Parse(pScore);
        //int ps = Convert.ToInt32(pScore) + 1;
        snakePlayerScore.Add(new SnakeGamePlayerScore(pName, DateTime.Now, pc, ps));

        string appData = JsonConvert.SerializeObject(snakePlayerScore);
        string appJsonFileName = Path.Combine(Application.dataPath, "SnakeScore.json");
        File.WriteAllText(appJsonFileName, appData);
        
        //print(pName + (pc+1) + (ps+1));
    }

    public void LoadDataFromJson()
    {
        string jsonFileName = Path.Combine(Application.dataPath, "SnakeScore.json");
        string jData = File.ReadAllText(jsonFileName);
        //textFromJson.text = jData;
        print(jData);

        snakePlayerScore = JsonConvert.DeserializeObject<List<SnakeGamePlayerScore>>(jData);
        //print(snakePlayerScore[0].playerName);
        // 마지막 데이터를 뿌려본다
        textFromJson.text = snakePlayerScore[snakePlayerScore.Count-1].playerName +
            " " + snakePlayerScore[snakePlayerScore.Count-1].getCount +
            " " + snakePlayerScore[snakePlayerScore.Count-1].getScore;
    }

    public void ChangedInputName(Text changedString)
    {
        pName = changedString.text;
    }

    public void ChangedInputCount(Text changedString)
    {
        pCount = changedString.text;
    }

    public void ChangedInputScore(Text changedString)
    {
        pScore = changedString.text;
    }
}

public class People
{
    public string name;
    public int age;

    public People(string name, int age)
    {
        this.name = name;
        this.age = age;
    }
}

public class SnakeGamePlayerScore
{
    public string playerName;
    public DateTime playingDate;
    public int getCount;
    public int getScore;

    public SnakeGamePlayerScore(string playerName, DateTime playingDate, int getCount, int getScore)
    {
        this.playerName = playerName;
        this.playingDate = playingDate;
        this.getCount = getCount;
        this.getScore = getScore;
    }
}
