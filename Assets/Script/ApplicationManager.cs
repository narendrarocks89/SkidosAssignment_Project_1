using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

/// <summary>
/// Application manager class handles the UI updates, button handling and API calling...
/// </summary>
public class ApplicationManager : MonoBehaviour
{
	[SerializeField]
	private GameObject playBtn;
	[SerializeField]
	private GameObject loadingObj;
	[SerializeField]
	private GameObject noDataFoundObj;
	[SerializeField]
	private Information information;

    public UnityEngine.UI.Slider progressBar;

    // Use this for initialization
    void Start ()
	{

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
		StartCoroutine (GetMyData ());
		playBtn.SetActive (false);
        progressBar.gameObject.SetActive(true);
		loadingObj.SetActive (true);
	}

	/// <summary>
	/// Datas recevied and processed with the desired result.
	/// </summary>
	void DataRecevied ()
	{
		loadingObj.SetActive (false);
	}

	/// <summary>
	/// Gets the data from the API.
	/// </summary>
	/// <returns>The my data.</returns>
	private IEnumerator GetMyData ()
	{
		using (WWW www = new WWW(Constants.TEST_API)) {
            StartCoroutine(ShowProgress(www));
			yield return www;

            string result = "";
			if (string.IsNullOrEmpty (www.error)) {				
				noDataFoundObj.SetActive (false);
                result = www.text;  //text of success
                Debug.Log(result);
				information = new Information (result);
				DataRecevied ();
			} else {
				result = www.error;  //error
				noDataFoundObj.SetActive (true);
			}
		}
    }   

    IEnumerator GetData()
    {
        UnityWebRequest www = UnityWebRequest.Get(Constants.TEST_API);

        yield return www.SendWebRequest();

        Debug.Log(www.downloadProgress);
        while (!www.isDone)
        {
            Debug.Log(www.downloadProgress);
            progressBar.value = www.downloadProgress;
        }
        Debug.Log(www.downloadProgress);

        progressBar.gameObject.SetActive(false);

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }

    }

    private IEnumerator ShowProgress(WWW www)
    {
        Debug.Log(www.isDone);
        while (!www.isDone)
        {
            Debug.Log(string.Format("Downloaded {0:P1}", www.progress));
            progressBar.value = www.progress;
            yield return new WaitForSeconds(.1f);
        }
        Debug.Log("Done");
        progressBar.gameObject.SetActive(false);
    }
}