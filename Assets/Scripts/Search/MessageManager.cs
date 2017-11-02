using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(TextController))]
public class MessageManager : SingletonMonoBehaviour<MessageManager>
{
	private TextController textController;
	private Dictionary<string, int> rMesItems = new Dictionary<string, int> ();

	public void AddRMes (string name)
	{
		rMesItems.Add (name, 0);
	}

	/// <summary>
	/// メッセージを表示する
	/// </summary>
	/// <param name="mes">Mes.</param>
	public void ShowMessages (string[] mes)
	{
		//Mesの処理ぷり
		int i = Random.Range (0, mes.Length);
		textController.SetNextLine (mes [i]);
	}

	/// <summary>
	/// 一定回数以上叩いているかの確認
	/// </summary>
	/// <returns><c>true</c>, if count was checked, <c>false</c> otherwise.</returns>
	/// <param name="name">Name.</param>
	/// <param name="n">N.</param>
	public bool CheckCount (string name, int n)
	{
		rMesItems [name]++;
		
		if (rMesItems [name] >= n)
			return true;
		else
			return false;
	}

	// Use this for initialization
	void Start ()
	{
		textController = GetComponent<TextController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (textController.IsCompleteDisplayText) {
			if (Input.GetMouseButtonDown (0)) {
				textController.ForceCompleteDisplayText ();
			}
		}
	}
}
