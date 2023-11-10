using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayerPrefDataManager : MonoBehaviour
{
    public InputField inputName;
    public InputField inputCount;
    public InputField inputScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        PlayerPrefs.SetString("Name", inputName.text);
        PlayerPrefs.SetInt("Count", int.Parse(inputCount.text));
        PlayerPrefs.SetInt("Score", int.Parse(inputScore.text));
    }

    public void Load()
    {
        if(PlayerPrefs.HasKey("Name"))
        {
            string getName = PlayerPrefs.GetString("Name");
            int getCount = PlayerPrefs.GetInt("Count");
            int getScore = PlayerPrefs.GetInt("Score");

            print(getName + " " + getCount + " " + getScore);
        }
    }
}