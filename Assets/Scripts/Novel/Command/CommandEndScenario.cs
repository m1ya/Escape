using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CommandEndScenario : ICommand
{
	public string Tag {
		get { return "end"; }
	}

	public void Command (Dictionary<string, string> command)
	{
		Debug.Log ("End Scenario");
	}
}
