using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActionManager : SingletonMonoBehaviour<ActionManager>
{
	[SerializeField]
	private Image img;

	private bool isEnd = false;

	public void ItemAction (string name, string gName, string[] mes)
	{
		if (name == "Door") {
			DoorAction (ItemManager.Instance.UseItem (name, gName), mes);
		}
	}

	private void DoorAction (bool flg, string[] mes)
	{
		if (flg) {
			StartCoroutine (_DoorOpen ());
		} else {
			MessageManager.Instance.ShowMessages (mes);
			AudioManager.Instance.PlaySE ("door");
		}
	}

	private IEnumerator _DoorOpen ()
	{
		AudioManager.Instance.PlaySE ("openDoor");
		img.enabled = true;
		isEnd = true;
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene ("End");
	}

	void Update ()
	{
		if (isEnd)
			img.color += new Color (0, 0, 0, 1) * Time.deltaTime;
	}

	void Start ()
	{
		img.enabled = false;
	}
	
}
