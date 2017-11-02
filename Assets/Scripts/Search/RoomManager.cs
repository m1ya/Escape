using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomManager : SingletonMonoBehaviour<RoomManager>
{

	private List<int> mainRoom = new List<int> ();
	private int currentRoom;
	private string zoomPoint;

	[SerializeField]
	private GameObject leftArrow;
	[SerializeField]
	private GameObject rightArrow;
	[SerializeField]
	private GameObject returnArrow;

	/// <summary>
	/// RoomをDicに追加する
	/// </summary>
	/// <param name="roomNum">Room number.</param>
	public void AddRoom (int roomNum)
	{
		mainRoom.Add (roomNum);
	}

	/// <summary>
	/// 左矢印用
	/// </summary>
	private void MoveLeft ()
	{
		int oldRoom = currentRoom;
		currentRoom--;
		if (currentRoom < 0) {
			currentRoom = mainRoom.Count - 1;
		}
		ChangeRoom (oldRoom, currentRoom);
	}

	/// <summary>
	/// 右矢印用
	/// </summary>
	private void MoveRight ()
	{
		int oldRoom = currentRoom;
		currentRoom++;
		if (currentRoom >= mainRoom.Count) {
			currentRoom = 0;
		}
		ChangeRoom (oldRoom, currentRoom);
	}

	/// <summary>
	/// 戻る矢印用
	/// </summary>
	private void ReturnRoom ()
	{
		returnArrow.SetActive (false);
		ChangeRoom (-1, currentRoom);
	}

	/// <summary>
	/// 部屋をFromからToへ移動する
	/// </summary>
	/// <param name="from">From.</param>
	/// <param name="to">To.</param>
	public void ChangeRoom (int from, int to)
	{
		//TODO: Roomの時は左右をだす、zoomの時は後ろを出す
		if (from != -1)
			PointAction.Instance.SetActivePoint (mainRoom [from].ToString (), false);

		//ZoomかMoveか
		if (zoomPoint != null) {
			PointAction.Instance.SetActivePoint (zoomPoint, false);
			zoomPoint = null;
		} 

		PointAction.Instance.SetBgImage (mainRoom [to].ToString ());
		PointAction.Instance.SetActivePoint (mainRoom [to].ToString (), true);

	}

	/// <summary>
	/// 部屋にZoomする
	/// </summary>
	/// <param name="to">To.</param>
	public void ZoomRoom (string to)
	{
		zoomPoint = to;
		PointAction.Instance.SetActivePoint (mainRoom [currentRoom].ToString (), false);
		PointAction.Instance.SetBgImage (to);
		PointAction.Instance.SetActivePoint (zoomPoint, true);
		returnArrow.SetActive (true);
	}

	/// <summary>
	/// Room周りの設定初期化
	/// </summary>
	public void InitRooms ()
	{
		mainRoom.Sort ();

		//一番数字の若いステージのみを表示する
		currentRoom = 0;
		ChangeRoom (-1, currentRoom);

		//戻る矢印を非表示に
		returnArrow.SetActive (false);

		//戻る矢印に機能追加
		EventTrigger trigger = returnArrow.AddComponent<EventTrigger> ();
		var entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ((x) => {
			ReturnRoom ();
		});
		trigger.triggers.Add (entry);

		//部屋が1つだけなら左右の矢印を表示しない
		if (mainRoom.Count == 1) {
			leftArrow.SetActive (false);
			rightArrow.SetActive (false);
			return;
		}

		//左矢印に機能追加
		trigger = leftArrow.AddComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ((x) => {
			MoveLeft ();
		});
		trigger.triggers.Add (entry);

		//右矢印に機能追加
		trigger = rightArrow.AddComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ((x) => {
			MoveRight ();
		});
		trigger.triggers.Add (entry);
	}
}
