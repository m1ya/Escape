using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
	[SerializeField]
	private RawImage itemImage;

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

	public void GetItem (string name, string mes)
	{
		if (gItems.ContainsKey (name)) {
			string[] mess = new string[1]{ mes };
			AudioManager.Instance.PlaySE ("get");
			MessageManager.Instance.ShowMessages (mess);
			Texture texture = Resources.Load ("Images/ActionObjs/" + name) as Texture;
			if (texture != null) {
				itemImage.texture = texture;
				itemImage.enabled = true;
			}
			gItems [name] = true;
		}
	}

	/// <summary>
	/// nameにgNameを使用する
	/// </summary>
	/// <returns><c>true</c>, if item was used, <c>false</c> otherwise.</returns>
	/// <param name="name">Name.</param>
	/// <param name="gName">G name.</param>
	public bool UseItem (string name, string gName)
	{
		if (gItems.ContainsKey (gName) == false)
			return false;
		if (uItems.ContainsKey (name) && gItems [gName] == true)
			uItems [name] = true;
		return uItems [name];
	}

	/// <summary>
	/// 初期化する
	/// </summary>
	public void InitItems ()
	{
		itemImage.enabled = false;
		//アイテム画像に機能追加
		EventTrigger trigger = itemImage.gameObject.AddComponent<EventTrigger> ();
		var entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ((x) => {
			OnClickItemImage ();
		});
		trigger.triggers.Add (entry);
	}

	/// <summary>
	/// アイテム画像を押した時
	/// </summary>
	private void OnClickItemImage ()
	{
		itemImage.enabled = false;
	}
}
