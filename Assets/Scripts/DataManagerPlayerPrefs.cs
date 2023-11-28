using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataManagerPlayerPrefs : MonoBehaviour
{
    //-------------- Variables
    public InputField inputName;
    public InputField inputCount;
    public InputField inputScore;

    string inputStrName;
    string inputStrCount;
    string inputStrScore;

    public TMP_Text displayMessage;

    //--------------Methods

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Write data into Registry
    public void Save()
    {
        if (CheckData())
        {
            /* 
             * 입력값을 inputField Text에서 String 변수로 변경
            PlayerPrefs.SetString("Name", inputName.text);
            PlayerPrefs.SetInt("Count", int.Parse(inputCount.text));
            PlayerPrefs.SetInt("Score", int.Parse(inputScore.text));
            */
            PlayerPrefs.SetString("Name", inputStrName);
            PlayerPrefs.SetInt("Count", int.Parse(inputStrCount));
            PlayerPrefs.SetInt("Score", int.Parse(inputStrScore));

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

    // Read data from Registry
    public void Load()
    {
        if (PlayerPrefs.HasKey("Name"))
        {
            string getName = PlayerPrefs.GetString("Name");
            int getCount = PlayerPrefs.GetInt("Count");
            int getScore = PlayerPrefs.GetInt("Score");

            //Debug.Log("Read data: " + getName + " " + getCount + " " + getScore);
            displayMessage.text = "Read data: " + getName + " " + getCount + " " + getScore;
        }
        else
        {
            //Debug.Log("[ERROR] There are NO Data");
            displayMessage.text = "[ERROR] There are NO Data";
        }
    }

    //------------- Set 
    public void ChangedInputUserName(Text changedString)
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
