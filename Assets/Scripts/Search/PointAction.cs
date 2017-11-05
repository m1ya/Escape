using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointAction : SingletonMonoBehaviour<PointAction>
{

	[SerializeField]
	private RawImage backGround;

	[SerializeField]
	private GameObject Objects;

	private Dictionary<string, GameObject> parents = new Dictionary<string, GameObject> ();
	private Dictionary<string, GameObject> actionObj = new Dictionary<string, GameObject> ();
	private Dictionary<string, Texture> bg = new Dictionary<string, Texture> ();

	public void AddParents (string name, GameObject obj)
	{
		parents.Add (name, obj);
	}

	public GameObject GetParents (string name)
	{
		return parents [name];
	}

	public void AddBg (string name, Texture texture)
	{
		bg.Add (name, texture);
	}

	public void AddActionObj (string name, GameObject obj)
	{
		actionObj.Add (name, obj);
	}

	public void SetBgImage (string name)
	{
		backGround.texture = bg [name];
	}

	public void SetActivePoint (string name, bool flg)
	{
		parents [name].SetActive (flg);
	}

	/// <summary>
	/// Spotのデータを更新する
	/// </summary>
	/// <param name="before">Before.</param>
	/// <param name="to">To.</param>
	public void ChangeSpotData (string before, string to)
	{
		if (parents.ContainsKey (to)) {
			SetActivePoint (to, true);
			SetActivePoint (before, false);
			parents [before] = parents [to];
			parents.Remove (to);
		}
		if (bg.ContainsKey (to)) {
			SetBgImage (to);
			bg [before] = bg [to];
			bg.Remove (to);
		}
	}

	// Use this for initialization
	void Start ()
	{
		new CreateStage (Objects);
		RoomManager.Instance.InitRooms ();
		ItemManager.Instance.InitItems ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
