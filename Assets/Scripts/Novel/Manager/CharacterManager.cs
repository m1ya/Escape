using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

	public static Dictionary<string, string> characterName = new Dictionary<string, string> () {
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

	public static Dictionary<string, string> faceName = new Dictionary<string, string> () {
		{ "普通", "normal" },
		{ "驚き", "" },
		{ "喜び", "" }
	};

	public static string GetFileName (string name)
	{
		if (!characterName.ContainsKey (name)) {
			Debug.LogError ("キャラクターのキーが登録されていません (" + name + ")");
			return null;
		}
		return characterName [name];
	}

	public static string GetFaceName (string name)
	{
		if (!faceName.ContainsKey (name)) {
			Debug.LogError ("表情のキーが登録されていません (" + name + ")");
			return null;
		}
		return faceName [name];
	}
}


