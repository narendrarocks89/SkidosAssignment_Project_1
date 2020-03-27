using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// User Controller class is use to assign data entries to each object and respection function operation.
/// </summary>
public class UserController : MonoBehaviour
{
    public RawImage userImage;
    public TextMeshProUGUI nameText;

    public Information userData;

    /// <summary>
    /// Updates the data once the data is been recevied and prefabs are created.
    /// </summary>
    /// <param name="userData">Choice data.</param>
    public void UpdateData(Information userData)
    {
        this.userData = userData;
        nameText.text = userData.name;
        StartCoroutine(GetTexture(userData.avatar));
    }

    IEnumerator GetTexture(string avatarURL)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(avatarURL);
        yield return www.SendWebRequest();

        Texture myTexture = DownloadHandlerTexture.GetContent(www) as Texture;
        userImage.texture = myTexture;
    }

    /// <summary>
    /// Button click function.
    /// </summary>
    public void OnClick()
    {
        EventManager.OnUserClick(userData);
    }
}