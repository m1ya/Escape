using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandCharacterShow : ICommand {

	public string Tag {
		get { return "chara_show"; }
	}

	public void Command (Dictionary<string, string> command)
	{
		string charaName = command["name"];
		string position = command["pos"];
		string fileName = CharacterManager.GetFileName (charaName);

		if(command.ContainsKey("face"))
			fileName += "_" + command ["face"];

		ImageController.Instance.SetCharacterImage(position, fileName);
	}
}
