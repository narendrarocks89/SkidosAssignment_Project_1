using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Operation Controller class is use to assign data entries to each object and respection function operation.
/// </summary>
public class OperationController : MonoBehaviour
{
    public TextMeshProUGUI operationNameText;
    public List<Operations> operation;

    /// <summary>
    /// Updates the data once the data is been recevied and prefabs are created.
    /// </summary>
    /// <param name="infoData">Choice data.</param>
    public void UpdateData(string operationName, List<Operations> operation)
    {
        this.operation = operation;
        operationNameText.text = operationName;
    }

    /// <summary>
    /// Button click function.
    /// </summary>
    public void OnClick()
    {
        EventManager.OnOperationClick(operation);
    }
}
