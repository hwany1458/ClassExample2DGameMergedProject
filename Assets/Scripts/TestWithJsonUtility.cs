using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 파일 처리 기능을 추가
using System.IO;

public class TestWithJsonUtility : MonoBehaviour
{
    // 필요한 실질적인 데이터 (객체로부터 제공받음)
    public PlayerData playerData;

    // Start is called before the first frame update
    void Start() 
    {
        AddData();
    }

    // Update is called once per frame
    void Update() {}

    //--------------
    // JsonUtility를 이용하여 오브젝트 데이터를 JSON 데이터로 저장
    // (PlayerController에서 처리하는 게 아니라, DataManager에서 처리하는 게 더 좋음)
    
    // 여기서는 ContextMenu를 사용
    // 컴포넌트의 뒤에 함수를 실행하는 메뉴버튼을 추가
    [ContextMenu("To Json Data")]
    public void WriteToJson()
    {
        // 유니티에 내장된 JsonUtility 사용
        // Json 문자열로 변형하고 싶은 오브젝트를 넘겨줌 (Json 형태로 포맷팅된 문자열이 나옴)        
        // 결과를 문자열로 받음
        string jsonData = JsonUtility.ToJson(playerData, true);

        // Json 데이터를 파일로 저장
        // 현재 실행중인 유니티 프로젝트 경로에 저장 : Application.dataPath 
        string savingFileName = Path.Combine(Application.dataPath, "playerDataJsonUtility.json");
        File.WriteAllText(savingFileName, jsonData);
    }

    [ContextMenu("From Json Data")]
    public void ReadFromJson()
    {
        // 현재 실행중인 유니티 프로젝트 경로에 저장 : Application.dataPath 
        string readFileName = Path.Combine(Application.dataPath, "playerDataJsonUtility.json");

        // JSON 데이터를 가져와서, JSON 데이터로부터 오브젝트를 생성해서 데이터를 써줌
        // 해당경로로부터 문자열 데이터를 읽어서 문자열로 반환
        string jsonData = File.ReadAllText(readFileName);
        // JsonUtility의 FromJson을 통해 JSON으로부터 오브젝트로 Deserialize
        // 오브젝트로 복구시킬 대상을 제네릭으로 명시해야 함
        // (JSON 데이터를 PlayerData 오브젝트로 복구시킴)
        // 출력으로 나온 PlayerData 타입의 오브젝트를 PlayerController가 가졌던 PlayerData로 덮어쓰기
        playerData = JsonUtility.FromJson<PlayerData>(jsonData);
    }

    void AddData()
    {
        playerData.name = "YongHwan";
        playerData.affiliation = "Wonkwang University";
        playerData.gender = "Man";
        playerData.age = 50;
        playerData.lectures.Add("C Langages");
        playerData.lectures.Add("Understanding of Game Engine");
        playerData.lectures.Add("Capstone Design");
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
public class PlayerData
{
    // Player에 필요한 데이터를 명시
    public string name;
    public string affiliation;
    public string gender;
    public int age;
    //public string[] lectures;
    public List<string> lectures;
}
