using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSePlay : ICommand
{

	public string Tag {
		get { return "se_play"; }
	}

	public void Command (Dictionary<string, string> command)
	{
		string fileName = ResourceNameManager.GetSeName (command ["name"]);
		AudioManager.Instance.PlaySE (fileName);
	}
}
