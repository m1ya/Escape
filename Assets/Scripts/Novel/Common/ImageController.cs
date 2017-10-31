using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageController : SingletonMonoBehaviour<ImageController>
{

	//配列かリストの方が処理綺麗だった
	[SerializeField]
	private RawImage left;
	[SerializeField]
	private RawImage center;
	[SerializeField]
	private RawImage right;

	[SerializeField]
	private RawImage back;

	[SerializeField]
	private RawImage item;

	[SerializeField]
	private RectTransform itemRectTransform;

	public void SetFaceImage (string charaName, string face)
	{
		
		charaName = CharacterManager.GetFileName (charaName);
		face = CharacterManager.GetFaceName (face);

		//charaNameとtexture.nameの_以前を比較する
		if (left.enabled == true) {
			var name = Regex.Match (left.texture.name, "(\\S+)_\\S+");
			string fileName = name.Groups [1].ToString ();
			if (fileName == "")
				fileName = left.texture.name;
			if (fileName == charaName)
				left.texture = Resources.Load ("Images/Characters/" + charaName + "_" + face) as Texture;
		}
		if (center.enabled == true) {
			var name = Regex.Match (center.texture.name, "(\\S+)_\\S+");
			string fileName = name.Groups [1].ToString ();
			if (fileName == "")
				fileName = center.texture.name;
			if (fileName == charaName)
				center.texture = Resources.Load ("Images/Characters/" + charaName + "_" + face) as Texture;
		}
		if (right.enabled == true) {
			var name = Regex.Match (right.texture.name, "(\\S+)_\\S+");
			string fileName = name.Groups [1].ToString ();
			if (fileName == "")
				fileName = right.texture.name;
			if (fileName == charaName)
				right.texture = Resources.Load ("Images/Characters/" + charaName + "_" + face) as Texture;
		}
	}

	public void SetCharacterImage (string pos, string fileName)
	{
		RawImage position = null;

		if (pos == "left")
			position = left;
		else if (pos == "center")
			position = center;
		else if (pos == "right")
			position = right;
		else {
			Debug.LogError ("引数が間違っています");
			return;
		}

		position.enabled = true;
		position.texture = Resources.Load ("Images/Characters/" + fileName) as Texture;
		return;
	}

	public void HideCharacterImage (string charaName)
	{
		if (charaName == null)
			return;
		
		//charaNameとtexture.nameの_以前を比較する
		if (left.enabled == true) {
			var name = Regex.Match (left.texture.name, "(\\S+)_\\S+");
			string fileName = name.Groups [1].ToString ();
			if (fileName == "")
				fileName = left.texture.name;
			if (fileName == charaName)
				left.enabled = false;
		}
		if (center.enabled == true) {
			var name = Regex.Match (center.texture.name, "(\\S+)_\\S+");
			string fileName = name.Groups [1].ToString ();
			if (fileName == "")
				fileName = center.texture.name;
			if (fileName == charaName)
				center.enabled = false;
		}
		if (right.enabled == true) {
			var name = Regex.Match (right.texture.name, "(\\S+)_\\S+");
			string fileName = name.Groups [1].ToString ();
			if (fileName == "")
				fileName = right.texture.name;
			if (fileName == charaName)
				right.enabled = false;
		}
	}

	public void SetBackgroundImage (string fileName)
	{
		if (fileName == null)
			return;
		
		back.texture = Resources.Load ("Images/Backgrounds/" + fileName) as Texture;
		back.enabled = true;
	}

	public void HideBackgroundImage ()
	{
		back.enabled = false;
	}

	public void SetItemImage (string fileName, ItemRectTransform itemRectTransform)
	{
		if (fileName == null)
			return;
		
		item.texture = Resources.Load ("Images/Items/" + fileName) as Texture;
		itemRectTransform.Set (this.itemRectTransform);
		item.enabled = true;
	}

	public void HideItemImage ()
	{
		item.enabled = false;
	}

	public void InitImages ()
	{
		left.enabled = false;
		center.enabled = false;
		right.enabled = false;
		back.enabled = false;
		item.enabled = false;
	}
}
