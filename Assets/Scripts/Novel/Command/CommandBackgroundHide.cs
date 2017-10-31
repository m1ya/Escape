using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBackgroundHide : ICommand {
	public string Tag {
		get { return "bg_hide"; }
	}

	public void Command (Dictionary<string, string> command)
	{
		ImageController.Instance.HideBackgroundImage();
	}
}
