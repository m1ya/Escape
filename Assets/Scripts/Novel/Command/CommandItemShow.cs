using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandItemShow : ICommand
{

	public string Tag {
		get { return "item_show"; }
	}

	public void Command (Dictionary<string, string> command)
	{
		string itemName = ResourceNameManager.GetItemName (command ["name"]);
		ItemRectTransform itemRectTransform = ResourceNameManager.GetItemRectTransform (command ["name"]);

		ImageController.Instance.SetItemImage (itemName, itemRectTransform);
	}
}
