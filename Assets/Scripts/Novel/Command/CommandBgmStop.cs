using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBgmStop : ICommand {

	public string Tag {
		get { return "bgm_stop"; }
	}

	public void Command (Dictionary<string, string> command)
	{
		AudioManager.Instance.FadeOutBGM ();
	}
}
