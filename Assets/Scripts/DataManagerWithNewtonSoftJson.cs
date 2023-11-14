using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// newtonsoft JSON ���� ����� ����ϱ� ����
using Newtonsoft.Json;
using System.IO;

public class DataManagerWithNewtonSoftJson : MonoBehaviour
{
    //public int[] testVariable;
    public List<int> testVariable;
    public JsonDataClass jsonData;

    // Start is called before the first frame update
    void Start()
    {
        // Init
        InitData();

        // Convert to JSON
        var result = JsonConvert.SerializeObject(testVariable);
        Debug.Log(result);

        // Object to JSON
        jsonData = new JsonDataClass();
        
        /*
        //jsonData.PrintData();
        string printStringJsonData = ObjectToJson(jsonData);
        //Debug.Log(printStringJsonData);

        // JSON to Object
        var readJsonData = JsonToObject<JsonDataClass>(printStringJsonData);
        readJsonData.PrintData();

        // Save Object to JSON file
        WriteToJsonFile("Test.json", printStringJsonData);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �׽���
    void InitData()
    {
        for(var i = 0; i < 10; i++)
        {
            testVariable.Add(i);
        }
    }

    // Object 2 JSON
    // JsonConvert Ŭ������ SerializeObject() �Լ��� �̿��Ͽ�
    // ������Ʈ�� ���ڿ��� �� JSON �����ͷ� ��ȯ�Ͽ� ��ȯ
    string ObjectToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    // DeserializeObject() �Լ��� �̿��Ͽ�
    // ���ڿ��� �� JSON �����͸� �޾Ƽ� ���ϴ� Ÿ���� ��ü�� ��ȯ
    T JsonToObject<T>(string jsonData)
    {
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    // ���ڿ��� ���� Json �����͸� ���Ϸ� ����
    public void WriteToJsonFile()
    {
        string savingObjectToJson = JsonConvert.SerializeObject(jsonData);
        string savingJsonFileName = Path.Combine(Application.dataPath, "ObjectToJson.json");
        File.WriteAllText(savingJsonFileName, savingObjectToJson);
    }

    // ����� JSON ������ �о ������Ʈ�� ��ȯ
    //JsonDataClass ReadFromJsonFile()
    public void ReadFromJsonFile()
    {
        string jsonFileName = Path.Combine(Application.dataPath, "ObjectToJson.json");
        string readJsonData = File.ReadAllText(jsonFileName);
        JsonDataClass createObjectFromJson = JsonConvert.DeserializeObject<JsonDataClass>(readJsonData);
        createObjectFromJson.PrintData();
    }
} 

[System.Serializable]
public class JsonDataClass
{
    public int intNum;
    public float floatNum;
    public double doubleNum;
    public string str;
    public bool boolVar;
    public int[] intArray;
    public string[] strArray;
    public List<int> intList = new List<int>();
    public Dictionary<string, int> dicGameScore = new Dictionary<string, int>();
    public Dictionary<string, string> dic = new Dictionary<string, string>();
    public Vector3Int vecInt;

    // ������
    public JsonDataClass()
    {
        intNum = 100;
        floatNum = 3.4f;
        doubleNum = 123.456;
        str = "JSON Test String";
        boolVar = true;
        intArray = new int[] { 1, 2, 3, 4, 5, 6, 7 };
        strArray = new string[] { "LG", "KT", "NC", "SSG", "�λ�" };
        for (int i=0; i<5; i++) { intList.Add(i*10);  }
        dicGameScore.Add("LG", 1);
        dicGameScore.Add("KT", 2);
        dicGameScore.Add("NC", 3);
        dic.Add("WK", "Wonkwang University");
        dic.Add("WKDC", "Wonkwang Digital Content");
        dic.Add("WKGC", "Wonkwang Game Content");
        vecInt = new Vector3Int(1, 2, 3);
    }

    public void PrintData()
    {
        Debug.Log($"integer = {intNum}");
        Debug.Log($"float = {floatNum}");
        Debug.Log($"double = {doubleNum}");
        Debug.Log($"str = {str}");
        Debug.Log($"boolVar = {boolVar}");
        for (int i=0; i<intArray.Length; i++) { Debug.Log($"int Array[{i}] = {intArray[i]}"); }
        for (int i = 0; i < strArray.Length; i++) { Debug.Log($"string Array[{i}] = {strArray[i]}"); }
        foreach(var item in intList) {
            Debug.Log($"int List: {item}");
        }
        foreach(var item in dicGameScore) {
            Debug.Log($"int Dictionary: key={item.Key} / value={item.Value}");
        }
        foreach (var item in dic) {
            Debug.Log($"string Dictionary: key={item.Key} / value={item.Value}");
        }
        Debug.Log($"int Vector = {vecInt}");
    }
}
