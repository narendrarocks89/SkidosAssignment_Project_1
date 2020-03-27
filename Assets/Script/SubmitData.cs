using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SubmitData : MonoBehaviour
{
    public TMP_InputField keyText;
    public TMP_InputField ValueText;

    public GameObject ScreenPanel;

    public GameObject SubmitButton;
    public GameObject BackButton;

    public GameObject SubmittedObj;

    void OnEnable()
    {
        Reset();
    }

    private const string keyName = "Key";
        public string KeyName
        {
            get { return PlayerPrefs.GetString(keyName); }
            set { PlayerPrefs.SetString(keyName, value); }
        }
        private const string valueOfKey = "Value";
        public string ValueOfKey
        {
            get { return PlayerPrefs.GetString(valueOfKey); }
            set { PlayerPrefs.SetString(valueOfKey, value); }
        }

    public void Submit()
    {
        KeyName = keyText.text;
        ValueOfKey = ValueText.text;

        ScreenPanel.SetActive(false);
        SubmittedObj.SetActive(true);

        BackButton.SetActive(true);
        SubmitButton.SetActive(false);
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
}
