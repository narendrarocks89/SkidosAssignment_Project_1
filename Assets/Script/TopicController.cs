using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Topic Controller class is use to assign data entries to each object and respection function operation.
/// </summary>
public class TopicController : MonoBehaviour
{
    public string topicId;
    public TextMeshProUGUI topicNameText;

    public Levels levelData;

    /// <summary>
    /// Updates the data once the data is been recevied and prefabs are created.
    /// </summary>
    /// <param name="infoData">Choice data.</param>
    public void UpdateData(Levels levelData)
    {
        this.levelData = levelData;
        topicId = levelData.subtopic_id;
        topicNameText.text = levelData.subtopic_name;
    }

    /// <summary>
    /// Button click function.
    /// </summary>
    public void OnClick()
    {
        EventManager.OnTopicClick(levelData);
    }
}
