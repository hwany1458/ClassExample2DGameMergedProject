using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ó�� ����� �߰�
using System.IO;

public class TestWithJsonUtility : MonoBehaviour
{
    // �ʿ��� �������� ������ (��ü�κ��� ��������)
    public PlayerData playerData;

    // Start is called before the first frame update
    void Start() 
    {
        AddData();
    }

    // Update is called once per frame
    void Update() {}

    //--------------
    // JsonUtility�� �̿��Ͽ� ������Ʈ �����͸� JSON �����ͷ� ����
    // (PlayerController���� ó���ϴ� �� �ƴ϶�, DataManager���� ó���ϴ� �� �� ����)
    
    // ���⼭�� ContextMenu�� ���
    // ������Ʈ�� �ڿ� �Լ��� �����ϴ� �޴���ư�� �߰�
    [ContextMenu("To Json Data")]
    public void WriteToJson()
    {
        // ����Ƽ�� ����� JsonUtility ���
        // Json ���ڿ��� �����ϰ� ���� ������Ʈ�� �Ѱ��� (Json ���·� �����õ� ���ڿ��� ����)        
        // ����� ���ڿ��� ����
        string jsonData = JsonUtility.ToJson(playerData, true);

        // Json �����͸� ���Ϸ� ����
        // ���� �������� ����Ƽ ������Ʈ ��ο� ���� : Application.dataPath 
        string savingFileName = Path.Combine(Application.dataPath, "playerDataJsonUtility.json");
        File.WriteAllText(savingFileName, jsonData);
    }

    [ContextMenu("From Json Data")]
    public void ReadFromJson()
    {
        // ���� �������� ����Ƽ ������Ʈ ��ο� ���� : Application.dataPath 
        string readFileName = Path.Combine(Application.dataPath, "playerDataJsonUtility.json");

        // JSON �����͸� �����ͼ�, JSON �����ͷκ��� ������Ʈ�� �����ؼ� �����͸� ����
        // �ش��ηκ��� ���ڿ� �����͸� �о ���ڿ��� ��ȯ
        string jsonData = File.ReadAllText(readFileName);
        // JsonUtility�� FromJson�� ���� JSON���κ��� ������Ʈ�� Deserialize
        // ������Ʈ�� ������ų ����� ���׸����� ����ؾ� ��
        // (JSON �����͸� PlayerData ������Ʈ�� ������Ŵ)
        // ������� ���� PlayerData Ÿ���� ������Ʈ�� PlayerController�� ������ PlayerData�� �����
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
public class PlayerData
{
    // Player�� �ʿ��� �����͸� ���
    public string name;
    public string affiliation;
    public string gender;
    public int age;
    //public string[] lectures;
    public List<string> lectures;
}
