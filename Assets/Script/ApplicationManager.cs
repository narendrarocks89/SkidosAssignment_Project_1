using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Enum for all types of screen.
/// </summary>
public enum GameScreen { None, Main, User, Operations, Level, Topic}

/// <summary>
/// Application manager class handles the UI updates, button handling and API calling...
/// </summary>
public class ApplicationManager : MonoBehaviour
{
    #region public variables
    public GameObject playBtn;
    public GameObject backBtn;
    public GameObject loadingObj;
    public GameObject noDataFoundObj;
    public GameObject scrollViewObj;

    public GameObject levelContentPrefab;
    public GameObject levelContainer;

    public GameObject operationsContentPrefab;
    public GameObject operationsContainer;

    public GameObject topicContentPrefab;
    public GameObject topicContainer;

    public GameObject userContentPrefab;
    public GameObject userContainer;

    public TextMeshProUGUI titleText;

    public GameScreen currentScreen = GameScreen.None;
    #endregion

    [SerializeField]
	private ApplicationData applicationData;

    private void OnEnable()
    {
        EventManager.UserClickEvent += OnUserClick;
        EventManager.OperationClickEvent += OnOperationClick;
        EventManager.LevelClickEvent += OnLevelClick;
        EventManager.TopicClickEvent += OnTopicClick;
        EventManager.ChangeTitleEvent += ChangeTitle;
    }

    private void OnDisable()
    {
        EventManager.UserClickEvent -= OnUserClick;
        EventManager.OperationClickEvent -= OnOperationClick;
        EventManager.LevelClickEvent -= OnLevelClick;
        EventManager.TopicClickEvent -= OnTopicClick;
        EventManager.ChangeTitleEvent -= ChangeTitle;
    }

    private void OnUserClick(Information information)
    {
        scrollViewObj.GetComponent<ScrollRect>().content = operationsContainer.GetComponent<RectTransform>();
        currentScreen = GameScreen.Operations;
        backBtn.SetActive(true);
        ChangeTitle("Select Operation", 260);

        if (information.Addition.Count > 0)
        {
            GameObject operationObj = Instantiate(operationsContentPrefab, operationsContainer.transform.position, Quaternion.identity, operationsContainer.transform);
            operationObj.name = "Addition";
            operationObj.GetComponent<OperationController>().UpdateData(operationObj.name, information.Addition);
        }

        if (information.Geometry.Count > 0)
        {
            GameObject operationObj = Instantiate(operationsContentPrefab, operationsContainer.transform.position, Quaternion.identity, operationsContainer.transform);
            operationObj.name = "Geometry";
            operationObj.GetComponent<OperationController>().UpdateData(operationObj.name, information.Geometry);
        }

        if (information.MixedOperations.Count > 0)
        {
            GameObject operationObj = Instantiate(operationsContentPrefab, operationsContainer.transform.position, Quaternion.identity, operationsContainer.transform);
            operationObj.name = "Mixed Operations";
            operationObj.GetComponent<OperationController>().UpdateData(operationObj.name, information.MixedOperations);
        }

        if (information.NumberSense.Count > 0)
        {
            GameObject operationObj = Instantiate(operationsContentPrefab, operationsContainer.transform.position, Quaternion.identity, operationsContainer.transform);
            operationObj.name = "Number sense";
            operationObj.GetComponent<OperationController>().UpdateData(operationObj.name, information.NumberSense);
        }

        if (information.Subtraction.Count > 0)
        {
            GameObject operationObj = Instantiate(operationsContentPrefab, operationsContainer.transform.position, Quaternion.identity, operationsContainer.transform);
            operationObj.name = "Subtraction";
            operationObj.GetComponent<OperationController>().UpdateData(operationObj.name, information.Subtraction);
        }

        levelContainer.SetActive(false);
        operationsContainer.SetActive(true);
        topicContainer.SetActive(false);
        userContainer.SetActive(false);
    }

    private void OnOperationClick(List<Operations> Operation)
    {
        currentScreen = GameScreen.Level;
        scrollViewObj.GetComponent<ScrollRect>().content = levelContainer.GetComponent<RectTransform>();
        ChangeTitle("Select Level", 360);

        for (int i = 0; i< Operation.Count; i++)
        {
            GameObject opObj = Instantiate(levelContentPrefab, levelContainer.transform.position, Quaternion.identity, levelContainer.transform);
            opObj.name = "Level " + (i + 1).ToString();
            opObj.GetComponent<LevelController>().UpdateData(opObj.name, Operation[i].levels);
        }

        levelContainer.SetActive(true);
        operationsContainer.SetActive(false);
        topicContainer.SetActive(false);
        userContainer.SetActive(false);
    }

    private void OnLevelClick(List<Levels> levelData)
    {
        currentScreen = GameScreen.Topic;
        scrollViewObj.GetComponent<ScrollRect>().content = topicContainer.GetComponent<RectTransform>();
        ChangeTitle("Select Topic", 360);

        for (int i = 0; i < levelData.Count; i++)
        {
            GameObject levelObj = Instantiate(topicContentPrefab, topicContainer.transform.position, Quaternion.identity, topicContainer.transform);
            levelObj.name = levelData[i].subtopic_id;
            levelObj.GetComponent<TopicController>().UpdateData(levelData[i]);
        }

        levelContainer.SetActive(false);
        operationsContainer.SetActive(false);
        topicContainer.SetActive(true);
        userContainer.SetActive(false);
    }

    private void OnTopicClick(Levels level)
    {
        EventManager.ActivateScreen2(true);
    }

    void Start()
    {
        currentScreen = GameScreen.Main;
    }

    void Update ()
	{
		#region To Quit the application
		if (Input.GetKeyDown (KeyCode.Escape)) {
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#else
			Application.Quit ();
			#endif
		}
		#endregion
	}

	/// <summary>
	/// Play button action to called from UI button.
	/// </summary>
	public void PlayButton ()
	{
		StartCoroutine (GetData ());
		playBtn.SetActive (false);
		loadingObj.SetActive (true);
	}

    /// <summary>
    /// Back Btton functionality.
    /// </summary>
    public void BackButton()
    {
        switch (currentScreen)
        {
            case GameScreen.Operations:
                operationsContainer.SetActive(false);
                userContainer.SetActive(true);
                currentScreen = GameScreen.User;
                ResetDataContainer(operationsContainer);
                backBtn.SetActive(false);
                scrollViewObj.GetComponent<ScrollRect>().content = userContainer.GetComponent<RectTransform>();
                ChangeTitle("Select User", 360);
                break;
            case GameScreen.Level:
                levelContainer.SetActive(false);
                operationsContainer.SetActive(true);
                currentScreen = GameScreen.Operations;
                ResetDataContainer(levelContainer);
                scrollViewObj.GetComponent<ScrollRect>().content = operationsContainer.GetComponent<RectTransform>();
                ChangeTitle("Select Operation", 280);
                break;
            case GameScreen.Topic:
                levelContainer.SetActive(true);
                topicContainer.SetActive(false);
                currentScreen = GameScreen.Level;
                ResetDataContainer(topicContainer);
                scrollViewObj.GetComponent<ScrollRect>().content = levelContainer.GetComponent<RectTransform>();
                ChangeTitle("Select Level", 360);
                break;

        }
    }

	/// <summary>
	/// Datas recevied and processed with the desired result.
	/// </summary>
	void DataRecevied ()
	{
        currentScreen = GameScreen.User;

        loadingObj.SetActive (false);
        scrollViewObj.SetActive(true);

        // this will create the prefabs as per the number of data received from API...
        foreach (Information infoData in applicationData.information)
        {
            GameObject userObj = Instantiate(userContentPrefab, userContainer.transform.position, Quaternion.identity, userContainer.transform);
            userObj.name = infoData.name;
            userObj.GetComponent<UserController>().UpdateData(infoData);
        }
    }

    /// <summary>
    /// Gets the data from the API.
    /// </summary>
    /// <returns>The my data.</returns>
    IEnumerator GetData()
    {
        UnityWebRequest www = UnityWebRequest.Get(Constants.TEST_API);
        yield return www.SendWebRequest();
        
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            noDataFoundObj.SetActive(true);
        }
        else
        {
            noDataFoundObj.SetActive(false);
            applicationData = new ApplicationData(www.downloadHandler.text);
            DataRecevied();
        }
    }

    /// <summary>
    /// Resets the data container and remove all entries before filling new data.
    /// </summary>
    void ResetDataContainer(GameObject prefabContainer)
    {
        foreach (Transform child in prefabContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Update Title text font name and size.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="size"></param>
    void ChangeTitle(string name, int size = 250)
    {
        titleText.text = name;
        titleText.fontSize = size;
    }
}