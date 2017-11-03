using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : SingletonMonoBehaviour<ColorManager>
{

	/// <summary>
	/// String型をColor型に変換する。デフォルトはWhite
	/// </summary>
	/// <returns>The to color.</returns>
	/// <param name="name">Name.</param>
	public Color StringToColor (string name)
	{
		if (name == "Yellow")
			return Color.yellow;
		else if (name == "Red")
			return Color.red;
		else if (name == "Blue")
			return Color.blue;
		else if (name == "Black")
			return Color.black;
		else if (name == "White")
			return Color.white;
		else if (name == "Green")
			return Color.green;
		else
			return Color.white;
	}
}