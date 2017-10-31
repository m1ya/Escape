using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBgmPlay : ICommand
{

	public string Tag {
		get { return "bgm_play"; }
	}

	public void Command (Dictionary<string, string> command)
	{
		string fileName = ResourceNameManager.GetBgmName (command ["name"]);
		AudioManager.Instance.PlayBGM (fileName);
	}
}
