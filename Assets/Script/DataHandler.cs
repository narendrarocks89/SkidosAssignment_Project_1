using System;
using System.Collections.Generic;

/// <summary>
/// Information class.
/// </summary>
[Serializable]
public class Information
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Information"/> class.
	/// </summary>
	/// <param name="jsonData">Json data.</param>
	public Information (string jsonData)
	{
		JSONObject data = new JSONObject (jsonData);
	}
}