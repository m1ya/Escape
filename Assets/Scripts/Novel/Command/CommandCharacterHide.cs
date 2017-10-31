using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandCharacterHide : ICommand {

	public string Tag {
		get { return "chara_hide"; }
	}

	public void Command (Dictionary<string, string> command)
	{
		string charaName = command["name"];
		string fileName = CharacterManager.GetFileName (charaName);

		ImageController.Instance.HideCharacterImage(fileName);
	}
}
