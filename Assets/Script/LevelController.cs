using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Level Controller class is use to assign data entries to each object and respection function operation.
/// </summary>
public class LevelController : MonoBehaviour
{
    public TextMeshProUGUI levelNameText;
    public List<Levels> levelData;

    /// <summary>
    /// Updates the data once the data is been recevied and prefabs are created.
    /// </summary>
    /// <param name="infoData">Choice data.</param>
    public void UpdateData(string levelName, List<Levels> levelData)
    {
        this.levelData = levelData;
        levelNameText.text = levelName;
    }

    /// <summary>
    /// Button click function.
    /// </summary>
    public void OnClick()
    {
        EventManager.OnLevelClick(levelData);
    }
}
