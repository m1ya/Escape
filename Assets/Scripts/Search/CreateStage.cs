using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateStage : MonoBehaviour
{

	//name, parent, mode, fileName, width, height, x, y, z
	private CSVData pointDatas;
	private GameObject searchCanvas;

	public CreateStage (GameObject canvas)
	{
		searchCanvas = canvas;

		SetPointData ("RegisterPoints");
		CreateParent ();
		CreateActionObj ();
	}

	//ポイントファイルをセットする
	public void SetPointData (string fileName)
	{
		pointDatas = CSVReader.ReadCsv ("Points/" + fileName);
	}

	//Zoom | Move となる親オブジェクトを生成する
	void CreateParent ()
	{
		for (int i = 0; i < pointDatas.lineNum; i++) {
			if (pointDatas.datas [i] [2] == "Move" || pointDatas.datas [i] [2] == "Zoom") {
				if (pointDatas.datas [i] [2] == "Move") {
					PointAction.Instance.AddRoom (int.Parse (pointDatas.datas [i] [0]));
				}
				GameObject obj = new GameObject (pointDatas.datas [i] [0]);
				obj.transform.SetParent (searchCanvas.transform);
				obj.transform.localPosition = Vector3.zero;
				PointAction.Instance.AddParents (pointDatas.datas [i] [0], obj);
				obj.SetActive (false);
				Texture texture = Resources.Load ("Images/Backgrounds/" + pointDatas.datas [i] [3]) as Texture;
				if (texture == null) {
					Debug.LogError ("そのようなファイルは存在しません：" + pointDatas.datas [i] [3]);
				}
				PointAction.Instance.AddBg (pointDatas.datas [i] [0], texture);
			}
		}
	}

	// Check などのアクションがあるオブジェクトを生成する
	void CreateActionObj ()
	{
		for (int i = 0; i < pointDatas.lineNum; i++) {
			if (pointDatas.datas [i] [2] == "Check" || pointDatas.datas [i] [2] == "Zoom") {
				string name = pointDatas.datas [i] [0];
				GameObject obj = new GameObject (name);
				RawImage img = obj.AddComponent<RawImage> ();
				Texture texture = Resources.Load ("Images/ActionObjs/" + pointDatas.datas [i] [3]) as Texture;
				if (texture == null) {
					Debug.LogError ("そのようなファイルは存在しません：" + pointDatas.datas [i] [3]);
				}
				img.texture = texture;
				obj.transform.SetParent (PointAction.Instance.GetParents (pointDatas.datas [i] [1]).transform);
				RectTransform rectTransform = obj.GetComponent<RectTransform> ();
				if (pointDatas.datas [i] [4] != "" && pointDatas.datas [i] [5] != "")
					rectTransform.sizeDelta = new Vector2 (int.Parse (pointDatas.datas [i] [4]), int.Parse (pointDatas.datas [i] [5]));
				if (pointDatas.datas [i] [6] != "" && pointDatas.datas [i] [7] != "" && pointDatas.datas [i] [8] != "")
					rectTransform.localPosition = new Vector3 (int.Parse (pointDatas.datas [i] [6]), int.Parse (pointDatas.datas [i] [7]), int.Parse (pointDatas.datas [i] [8]));
				PointAction.Instance.AddActionObj (name, obj);

				//TODO: Zoom後とZoomする前のトリガーの画像が同じになる
				if (pointDatas.datas [i] [2] == "Zoom") {
					EventTrigger trigger = obj.AddComponent<EventTrigger> ();
					var entry = new EventTrigger.Entry ();
					entry.eventID = EventTriggerType.PointerDown;
					entry.callback.AddListener ((x) => {
						PointAction.Instance.ZoomRoom (name);
					});
					trigger.triggers.Add (entry);
				}
			}
		}
	}
}
