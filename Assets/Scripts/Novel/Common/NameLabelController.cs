using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameLabelController : MonoBehaviour {

	[SerializeField]
	private Text _uiNameLabel;

	// 次に表示する文字列をセットする
	public void SetNextName(string name)
	{
		_uiNameLabel.text = name;
	}
}
