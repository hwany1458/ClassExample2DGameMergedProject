using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ó�� ����� �߰�
using System.IO;


public class TestPlayerController : MonoBehaviour
{
    // �ʿ��� �������� ������ (��ü�κ��� ��������)
    public TestPlayerData playerData;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {}

    //--------------
    // JsonUtility�� �̿��Ͽ� ������Ʈ �����͸� JSON �����ͷ� ����
    // (PlayerController���� ó���ϴ� �� �ƴ϶�, DataManager���� ó���ϴ� �� �� ����)
    // ���⼭�� ContextMenu�� ���
    // ������Ʈ�� �ڿ� �Լ��� �����ϴ� �޴���ư�� �߰�
    [ContextMenu("To Json Data")]
    void SavePlayerDataToJson()
    {
        // ����Ƽ�� ����� JsonUtility ���
        // Json ���ڿ��� �����ϰ� ���� ������Ʈ�� �Ѱ��� (Json ���·� �����õ� ���ڿ��� ����)        
        // ����� ���ڿ��� ����
        string jsonData = JsonUtility.ToJson(playerData, true);
        // Json �����͸� ���Ϸ� ����
        // ���� �������� ����Ƽ ������Ʈ ��ο� ���� : Application.dataPath 
        string path = Path.Combine(Application.dataPath, "playerData.json");
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    void LoadPlayerDataFromJson()
    {
        // ���� �������� ����Ƽ ������Ʈ ��ο� ���� : Application.dataPath 
        string path = Path.Combine(Application.dataPath, "playerData.json");
        // JSON �����͸� �����ͼ�, JSON �����ͷκ��� ������Ʈ�� �����ؼ� �����͸� ����
        // �ش��ηκ��� ���ڿ� �����͸� �о ���ڿ��� ��ȯ
        string jsonData = File.ReadAllText(path);
        // JsonUtility�� FromJson�� ���� JSON���κ��� ������Ʈ�� Deserialize
        // ������Ʈ�� ������ų ����� ���׸����� ����ؾ� ��
        // (JSON �����͸� PlayerData ������Ʈ�� ������Ŵ)
        // ������� ���� PlayerData Ÿ���� ������Ʈ�� PlayerController�� ������ PlayerData�� �����
        playerData = JsonUtility.FromJson<TestPlayerData>(jsonData);
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
public class TestPlayerData
{
    // Player�� �ʿ��� �����͸� ���
    public string name;
    public string affiliation;
    public string gender;
    public int age;
    public string[] lectures;
}
