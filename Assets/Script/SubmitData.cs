using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

public class SubmitData : MonoBehaviour
{
    public TMP_InputField keyText;
    public TMP_InputField ValueText;

    public GameObject ScreenPanel;

    public GameObject SubmitButton;
    public GameObject BackButton;

    public GameObject SubmittedObj;

    public class PlayerData
    {
        public string Key;
        public string Value;
    }

    void OnEnable()
    {
        Reset();
    }

    void Start()
    {
        //string loadJson = File.ReadAllText(Application.dataPath + "/Json Files/saveFile.json");
        //playerData = JsonUtility.FromJson<PlayerData>(loadJson);
    }

    public void Submit()
    {
        if (string.IsNullOrEmpty(keyText.text) && string.IsNullOrEmpty(ValueText.text))
            return;

        PlayerData playerData = new PlayerData
        {
            Key = keyText.text,
            Value = ValueText.text
        };
        
        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        SaveSystem.Save(json);
        
        //ScreenPanel.SetActive(false);
        //SubmittedObj.SetActive(true);

        //BackButton.SetActive(true);
        //SubmitButton.SetActive(false);
    }

    public void Back()
    {
        EventManager.ActivateScreen1(true);
        Reset();
    }

    void Reset()
    {
        keyText.text = string.Empty;
        ValueText.text = string.Empty;

        SubmittedObj.SetActive(false);
        ScreenPanel.SetActive(true);
        BackButton.SetActive(false);
        SubmitButton.SetActive(true);
    }

    private void Load()
    {
        string saveString = SaveSystem.Load();
        if(saveString != null)
        {
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(saveString);
            Debug.Log("Key : " + playerData.Key);
            Debug.Log("Value : " + playerData.Value);
        }
        else
        {
            Debug.Log("File is missing.");
        }
    }
}
