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
	/// Pintsファイルからオブジェクトを生成する
	/// name, mode, fileName
	/// </summary>
	void CreateParent ()
	{
		for (int i = 0; i < pointDatas.lineNum; i++) {
			if (pointDatas.datas [i] [1] == "Move") {
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

			if (pointDatas.datas [i] [2] == "Zoom") {
				string name = pointDatas.datas [i] [9];
				EventTrigger trigger = obj.AddComponent<EventTrigger> ();
				var entry = new EventTrigger.Entry ();
				entry.eventID = EventTriggerType.PointerDown;
				entry.callback.AddListener ((x) => {
					RoomManager.Instance.ZoomRoom (name);
				});
				trigger.triggers.Add (entry);
			}
		}
	}
}
