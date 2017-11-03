using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
	private Dictionary<string, bool> gItems = new Dictionary<string, bool> ();
	private Dictionary<string, bool> uItems = new Dictionary<string, bool> ();

	/// <summary>
	/// 持っていないItemだったらDicに追加
	/// </summary>
	/// <param name="name">Name.</param>
	public void AddGItems (string name)
	{
		if (!gItems.ContainsKey (name))
			gItems.Add (name, false);
	}

	public void AddUItems (string name)
	{
		if (!uItems.ContainsKey (name))
			uItems.Add (name, false);
	}

	public void GetItem (string name)
	{
		if (gItems.ContainsKey (name))
			gItems [name] = true;
	}

	public bool UseItem (string name, string gName)
	{
		if (uItems.ContainsKey (name) && gItems [gName] == true)
			uItems [name] = true;
		return uItems [name];
	}
}
