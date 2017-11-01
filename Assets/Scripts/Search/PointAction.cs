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

	private List<int> mainRoom = new List<int> ();
	private int currentRoom;
	private string zoomPoint;

	public void AddParents (string name, GameObject obj)
	{
		parents.Add (name, obj);
	}

	public void AddRoom (int roomNum)
	{
		mainRoom.Add (roomNum);
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

	private void SetBgImage (string name)
	{
		backGround.texture = bg [name];
	}

	public void MoveLeft ()
	{
		int oldRoom = currentRoom;
		currentRoom--;
		if (currentRoom < 0) {
			currentRoom = mainRoom.Count - 1;
		}
		ChangeRoom (oldRoom, currentRoom);
	}

	public void MoveRight ()
	{
		int oldRoom = currentRoom;
		currentRoom++;
		if (currentRoom >= mainRoom.Count) {
			currentRoom = 0;
		}
		ChangeRoom (oldRoom, currentRoom);
	}

	public void ReturnRoom ()
	{
		ChangeRoom (-1, currentRoom);
	}

	private void ChangeRoom (int from, int to)
	{
		//TODO: Roomの時は左右をだす、zoomの時は後ろを出す
		if (from != -1)
			parents [mainRoom [from].ToString ()].SetActive (false);
		if (zoomPoint != null) {
			parents [zoomPoint].SetActive (false);
			zoomPoint = null;
		}

		SetBgImage (mainRoom [to].ToString ());
		parents [mainRoom [to].ToString ()].SetActive (true);
	}

	public void ZoomRoom (string to)
	{
		zoomPoint = to;
		parents [mainRoom [currentRoom].ToString ()].SetActive (false);
		SetBgImage (to);
		parents [to].SetActive (true);
	}


	// Use this for initialization
	void Start ()
	{
		searchCanvas = this.gameObject;

		new CreateStage (searchCanvas);
		mainRoom.Sort ();

		//一番数字の若いステージのみを表示する
		currentRoom = 0;
		ChangeRoom (-1, currentRoom);


	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
