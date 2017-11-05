using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateStage : MonoBehaviour
{

	private CSVData pointDatas;
	private GameObject searchCanvas;

	public CreateStage (GameObject canvas)
	{
		searchCanvas = canvas;

		//画面を作る
		SetData ("RegisterPoints");
		CreateParent ();

		//画面に表示されるアイテムを作る
		SetData ("RegisterItems");
		CreateActionObj ();
	}

	//ポイントファイルをセットする
	public void SetData (string fileName)
	{
		pointDatas = CSVReader.ReadCsv ("Points/" + fileName);
	}

	/// <summary>
	/// Pointsファイルからオブジェクトを生成する
	/// name, mode, fileName
	/// </summary>
	void CreateParent ()
	{
		for (int i = 0; i < pointDatas.lineNum; i++) {
			if (pointDatas.datas [i] [1] == "Room") {
				RoomManager.Instance.AddRoom (int.Parse (pointDatas.datas [i] [0]));
			}
			GameObject obj = new GameObject (pointDatas.datas [i] [0]);
			obj.transform.SetParent (searchCanvas.transform);
			obj.transform.localPosition = Vector3.zero;
			PointAction.Instance.AddParents (pointDatas.datas [i] [0], obj);
			obj.SetActive (false);
			Texture texture = Resources.Load ("Images/Backgrounds/" + pointDatas.datas [i] [2]) as Texture;
			if (texture == null) {
				Debug.LogError ("そのようなファイルは存在しません：" + pointDatas.datas [i] [2]);
			}
			PointAction.Instance.AddBg (pointDatas.datas [i] [0], texture);
		}
	}

	/// <summary>
	/// アクションがあるオブジェクトを生成する
	/// name, parent, mode, fileName, width, height, x, y, z, modeの引数[]
	/// </summary>
	void CreateActionObj ()
	{
		for (int i = 0; i < pointDatas.lineNum; i++) {
			GameObject obj = new GameObject (pointDatas.datas [i] [0]);

			RawImage img = obj.AddComponent<RawImage> ();
			if (pointDatas.datas [i] [3] == "") {
				img.color = new Color (0, 0, 0, 0);
			} else {
				Texture texture = Resources.Load ("Images/ActionObjs/" + pointDatas.datas [i] [3]) as Texture;
				if (texture == null) {
					Debug.LogError ("そのようなファイルは存在しません：" + pointDatas.datas [i] [3]);
				}
				img.texture = texture;
			}

			obj.transform.SetParent (PointAction.Instance.GetParents (pointDatas.datas [i] [1]).transform);
			RectTransform rectTransform = obj.GetComponent<RectTransform> ();
			if (pointDatas.datas [i] [4] != "" && pointDatas.datas [i] [5] != "")
				rectTransform.sizeDelta = new Vector2 (int.Parse (pointDatas.datas [i] [4]), int.Parse (pointDatas.datas [i] [5]));
			if (pointDatas.datas [i] [6] != "" && pointDatas.datas [i] [7] != "" && pointDatas.datas [i] [8] != "")
				rectTransform.localPosition = new Vector3 (int.Parse (pointDatas.datas [i] [6]), int.Parse (pointDatas.datas [i] [7]), int.Parse (pointDatas.datas [i] [8]));
			PointAction.Instance.AddActionObj (pointDatas.datas [i] [0], obj);

			if (pointDatas.datas [i] [2] == "None") {
				//何もしない
			} else if (pointDatas.datas [i] [2] == "Zoom") {
				string name = pointDatas.datas [i] [9];
				EventTrigger trigger = obj.AddComponent<EventTrigger> ();
				var entry = new EventTrigger.Entry ();
				entry.eventID = EventTriggerType.PointerDown;
				entry.callback.AddListener ((x) => {
					RoomManager.Instance.ZoomRoom (name);
				});
				trigger.triggers.Add (entry);
			} else if (pointDatas.datas [i] [2] == "Mes") {
				int length = pointDatas.datas [i].Length - 9;
				string[] mes = new string[length];
				for (int j = 9; j - 9 < length; j++) {
					mes [j - 9] = pointDatas.datas [i] [j];
				}
				EventTrigger trigger = obj.AddComponent<EventTrigger> ();
				var entry = new EventTrigger.Entry ();
				entry.eventID = EventTriggerType.PointerDown;
				entry.callback.AddListener ((x) => {
					MessageManager.Instance.ShowMessages (mes);
				});
				trigger.triggers.Add (entry);
			} else if (pointDatas.datas [i] [2] == "RMes") {
				string name = pointDatas.datas [i] [0];
				int rCount = int.Parse (pointDatas.datas [i] [9]);
				MessageManager.Instance.AddRMes (name);
				string rMes = pointDatas.datas [i] [pointDatas.datas [i].Length - 1];
				int length = pointDatas.datas [i].Length - 10;
				string[] mes = new string[length - 1];
				for (int j = 10; j - 10 < length - 1; j++) {
					mes [j - 10] = pointDatas.datas [i] [j];
				}
				EventTrigger trigger = obj.AddComponent<EventTrigger> ();
				var entry = new EventTrigger.Entry ();
				entry.eventID = EventTriggerType.PointerDown;
				entry.callback.AddListener ((x) => {
					if (MessageManager.Instance.CheckCount (name, rCount)) {
						MessageManager.Instance.ShowMessages (new string[]{ rMes });
					} else {
						MessageManager.Instance.ShowMessages (mes);
					}
				});
				trigger.triggers.Add (entry);
			} else if (pointDatas.datas [i] [2] == "IRock") {
				string name = pointDatas.datas [i] [0];
				int length = pointDatas.datas [i].Length - 10;
				int[] nums = new int[length];
				int correctNum = int.Parse (pointDatas.datas [i] [9]);
				GameObject text = new GameObject ("Text");
				Text textComponent = text.AddComponent<Text> ();
				textComponent.font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
				textComponent.fontSize = (int)(rectTransform.sizeDelta.x / 2);
				textComponent.alignment = TextAnchor.MiddleCenter;
				text.transform.SetParent (obj.transform);
				text.GetComponent<RectTransform> ().localPosition = new Vector3 (0, 0, 0);
				RockManager.Instance.AddIRocks (name, text, correctNum, int.Parse (pointDatas.datas [i] [10]));
				for (int j = 10; j - 10 < length; j++) {
					nums [j - 10] = int.Parse (pointDatas.datas [i] [j]);
				}
				EventTrigger trigger = obj.AddComponent<EventTrigger> ();
				var entry = new EventTrigger.Entry ();
				entry.eventID = EventTriggerType.PointerDown;
				entry.callback.AddListener ((x) => {
					AudioManager.Instance.PlaySE ("button");
					RockManager.Instance.ChangeNumber (name, nums);
				});
				trigger.triggers.Add (entry);
			} else if (pointDatas.datas [i] [2] == "CRock") {
				string name = pointDatas.datas [i] [0];
				int length = pointDatas.datas [i].Length - 10;
				Color[] colors = new Color[length];
				Color correctColor = ColorManager.Instance.StringToColor (pointDatas.datas [i] [9]);
				GameObject imgObj = new GameObject ("RawImage");
				RawImage rawImage = imgObj.AddComponent<RawImage> ();
				rawImage.transform.SetParent (obj.transform);
				rawImage.GetComponent<RectTransform> ().localPosition = new Vector3 (0, 0, 0);
				rawImage.GetComponent<RectTransform> ().sizeDelta = rectTransform.sizeDelta / 2;
				RockManager.Instance.AddCRocks (name, imgObj, correctColor, ColorManager.Instance.StringToColor (pointDatas.datas [i] [10]));
				for (int j = 10; j - 10 < length; j++) {
					colors [j - 10] = ColorManager.Instance.StringToColor (pointDatas.datas [i] [j]);
				}
				EventTrigger trigger = obj.AddComponent<EventTrigger> ();
				var entry = new EventTrigger.Entry ();
				entry.eventID = EventTriggerType.PointerDown;
				entry.callback.AddListener ((x) => {
					AudioManager.Instance.PlaySE ("button");
					RockManager.Instance.ChangeColor (name, colors);
				});
				trigger.triggers.Add (entry);
			} else if (pointDatas.datas [i] [2] == "GItem") {
				string name = pointDatas.datas [i] [0];
				string mes = pointDatas.datas [i] [9];
				ItemManager.Instance.AddGItems (name);
				EventTrigger trigger = obj.AddComponent<EventTrigger> ();
				var entry = new EventTrigger.Entry ();
				entry.eventID = EventTriggerType.PointerDown;
				entry.callback.AddListener ((x) => {
					ItemManager.Instance.GetItem (name, mes);
					Destroy (obj);
				});
				trigger.triggers.Add (entry);
			} else if (pointDatas.datas [i] [2] == "UItem") {
				string name = pointDatas.datas [i] [0];
				string gName = pointDatas.datas [i] [9];
				int length = pointDatas.datas [i].Length - 10;
				string[] mes = new string[length];
				for (int j = 10; j - 10 < length; j++) {
					mes [j - 10] = pointDatas.datas [i] [j];
				}
				ItemManager.Instance.AddUItems (name);
				EventTrigger trigger = obj.AddComponent<EventTrigger> ();
				var entry = new EventTrigger.Entry ();
				entry.eventID = EventTriggerType.PointerDown;
				entry.callback.AddListener ((x) => {
					ActionManager.Instance.ItemAction (name, gName, mes);
				});
				trigger.triggers.Add (entry);
			} else if (pointDatas.datas [i] [2] == "Open") {
				string bName = pointDatas.datas [i] [9];
				string aName = pointDatas.datas [i] [10];
				int length = pointDatas.datas [i].Length - 11;
				string[] datas = new string[length];
				for (int j = 11; j - 11 < length; j++) {
					datas [j - 11] = pointDatas.datas [i] [j];
				}
				EventTrigger trigger = obj.AddComponent<EventTrigger> ();
				var entry = new EventTrigger.Entry ();
				entry.eventID = EventTriggerType.PointerDown;
				entry.callback.AddListener ((x) => {
					if (RockManager.Instance.CheckRocks (datas)) {
						AudioManager.Instance.PlaySE ("open");
						PointAction.Instance.ChangeSpotData (bName, aName);
					} else {
						AudioManager.Instance.PlaySE ("cancel");
					}
				});
				trigger.triggers.Add (entry);
			} else {
				Debug.LogError ("そのようなモードは存在しません：" + pointDatas.datas [i] [2]);
			}
		}
	}
}
