using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
	private Dictionary<string, string> gItems = new Dictionary<string, string> ();

	/// <summary>
	/// 持っていないItemだったらDicに追加
	/// </summary>
	/// <param name="name">Name.</param>
	public void AddGItems (string name)
	{
		if (!gItems.ContainsKey (name))
			gItems.Add (name, "kari");
	}
}
