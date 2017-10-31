using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandItemHide : ICommand
{
	public string Tag {
		get { return "item_hide"; }
	}

	public void Command (Dictionary<string, string> command)
	{
		ImageController.Instance.HideItemImage ();
	}
}
