using System;
using System.Collections.Generic;

/// <summary>
/// Application data received from the server.
/// </summary>
[Serializable]
public class ApplicationData
{
    public List<Information> information;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationData"/> class.
    /// </summary>
    /// <param name="jsonData"></param>
    public ApplicationData(string jsonData)
    {
        JSONObject data = new JSONObject(jsonData);

        //Debug.Log("Data Count: "+ data.Count);

        information = new List<Information>();

        for(int i = 0; i < data.Count; i++)
        {
            Information info = new Information(data[i].ToString());
            information.Add(info);
        }
    }
}

/// <summary>
/// Information class.
/// </summary>
[Serializable]
public class Information
{
    public int id;
    public string createdAt;
    public string name;
    public string avatar;
    public List<Operations> Addition;
    public List<Operations> Geometry;
    public List<Operations> MixedOperations;
    public List<Operations> NumberSense;
    public List<Operations> Subtraction;

    /// <summary>
    /// Initializes a new instance of the <see cref="Information"/> class.
    /// </summary>
    /// <param name="jsonData">Json data.</param>
    public Information (string jsonData)
	{
        JSONObject data = new JSONObject (jsonData);

        id = int.Parse(data.GetField("id").str);
        createdAt = data.GetField("createdAt").str;
        name = data.GetField("name").str;
        avatar = data.GetField("avatar").str;        

        JSONObject additionData = data.GetField("Addition");
        Addition = new List<Operations>();
        OperationHandling(additionData, Addition);

        JSONObject geometryData = data.GetField("Geometry");
        Geometry = new List<Operations>();
        OperationHandling(geometryData, Geometry);

        JSONObject mixedOperationsData = data.GetField("Mixed Operations");
        MixedOperations = new List<Operations>();
        OperationHandling(mixedOperationsData, MixedOperations);

        JSONObject numberSenseData = data.GetField("Number sense");
        NumberSense = new List<Operations>();
        OperationHandling(numberSenseData, NumberSense);

        JSONObject subtractionData = data.GetField("Subtraction");
        Subtraction = new List<Operations>();
        OperationHandling(subtractionData, Subtraction);
    }

    /// <summary>
    /// It handles all operations and fetch all levels in it.
    /// </summary>
    /// <param name="jsondata"></param>
    /// <param name="operations"></param>
    public void OperationHandling(JSONObject jsondata, List<Operations> operations)
    {
        //Debug.Log("Operations Count: " + jsondata.Count + "\n\n" + jsondata.ToString());

        for (int i = 0; i < jsondata.Count; i++)
        {
            Operations op = new Operations(jsondata[i].ToString());
            operations.Add(op);
        }
    }
}

/// <summary>
/// Operations class.
/// </summary>
[Serializable]
public class Operations
{
    public List<Levels> levels;

    /// <summary>
    /// Initializes a new instance of the <see cref="Operations"/> class.
    /// </summary>
    /// <param name="jsonData">Json data.</param>
    public Operations(string jsonData)
    {
        JSONObject data = new JSONObject(jsonData);

        //Debug.Log("Levels Count: " + data.Count + "\n\n" + data.ToString());

        levels = new List<Levels>();

        for (int i = 0; i < data.Count; i++)
        {
            Levels info = new Levels(data[i].ToString());
            levels.Add(info);
        }
    }
}

/// <summary>
/// Contains all information in every level.
/// </summary>
[Serializable]
public class Levels
{
    public string subtopic_id;
    public string subtopic_name;

    /// <summary>
    /// Initializes a new instance of the <see cref="Levels"/> class.
    /// </summary>
    /// <param name="jsonData"></param>
    public Levels(string jsonData)
    {
        JSONObject data = new JSONObject(jsonData);

        //Debug.Log("Inner Level data Count: " + data.Count + "\n\n" + data.ToString());

        subtopic_id = data.GetField("subtopic_id").str;
        subtopic_name = data.GetField("subtopic_name").str;
    }
}