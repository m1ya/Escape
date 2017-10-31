using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBackgroundShow : ICommand
{

	public string Tag {
		get { return "bg_show"; }
	}

	public void Command (Dictionary<string, string> command)
	{
		string bgName = ResourceNameManager.GetBgName (command ["name"]);

		ImageController.Instance.SetBackgroundImage (bgName);
	}
}
