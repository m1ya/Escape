using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointAction : SingletonMonoBehaviour<PointAction>
{

	private GameObject searchCanvas;
	[SerializeField]
	private RawImage backGround;

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

	// Use this for initialization
	void Start ()
	{
		searchCanvas = this.gameObject;

		new CreateStage (searchCanvas);
		RoomManager.Instance.InitRooms ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
