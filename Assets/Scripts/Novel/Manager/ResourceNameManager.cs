using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNameManager : MonoBehaviour
{

	public static Dictionary<string, string> bgmName = new Dictionary<string, string> () {
		{ "藤咲日和", "hiyori" },
		{ "藤咲晴", "haru" },
		{ "大川澄人", "sumito" },
		{ "湊柚月", "yuduki" },
		{ "一宮桜雨", "" },
		{ "有村雪", "yuki" },
		{ "渡辺美保", "miho" },
		{ "小澤浩太朗", "koutarou" },
		{ "藤咲学", "manabu" },
		{ "藤咲文恵", "humie" }
	};

	public static Dictionary<string, string> seName = new Dictionary<string, string> () {
		{ "普通", "normal" },
		{ "驚き", "" },
		{ "喜び", "" }
	};

	public static Dictionary<string, string> bgName = new Dictionary<string, string> () {
		{ "普通", "normal" },
		{ "驚き", "" },
		{ "喜び", "" }
	};

	public static Dictionary<string, string> itemName = new Dictionary<string, string> () {
		{ "hiyori", "hiyori" },
		{ "驚き", "" },
		{ "喜び", "" }
	};

	public static Dictionary<string, ItemRectTransform> itemTrans = new Dictionary<string,  ItemRectTransform> () {
		{ "hiyori", new ItemRectTransform (new Vector3 (0, 0, 0), new Vector2 (200, 200)) }
	};

	public static string GetBgmName (string name)
	{
		if (!bgmName.ContainsKey (name)) {
			Debug.LogError ("BGMのキーが登録されていません (" + name + ")");
			return null;
		}
		return bgmName [name];
	}

	public static string GetSeName (string name)
	{
		if (!seName.ContainsKey (name)) {
			Debug.LogError ("SEのキーが登録されていません (" + name + ")");
			return null;
		}
		return seName [name];
	}

	public static string GetBgName (string name)
	{
		if (!bgName.ContainsKey (name)) {
			Debug.LogError ("背景のキーが登録されていません (" + name + ")");
			return null;
		}
		return bgName [name];
	}

	public static string GetItemName (string name)
	{
		if (!itemName.ContainsKey (name)) {
			Debug.LogError ("アイテムのキーが登録されていません (" + name + ")");
			return null;
		}
		return itemName [name];
	}

	public static ItemRectTransform GetItemRectTransform (string name)
	{
		if (!itemTrans.ContainsKey (name)) {
			Debug.LogError ("ItemRectTransformのキーが登録されていません (" + name + ")");
			return null;
		}
		return itemTrans [name];
	}
}
