using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct IntRockData
{
	public Text text;
	public int correctNum;
	public int currentNum;
	public bool isCorrect;

	public IntRockData (Text text, int correctNum, int currentNum)
	{
		this.text = text;
		this.correctNum = correctNum;
		this.currentNum = currentNum;
		isCorrect = (correctNum == currentNum);
		text.text = currentNum.ToString ();
	}

}

public struct ColorRockData
{
	public RawImage rawImage;
	public Color correctColor;
	public Color currentColor;
	public bool isCorrect;

	public ColorRockData (RawImage rawImage, Color correctColor, Color currentColor)
	{
		this.rawImage = rawImage;
		this.correctColor = correctColor;
		this.currentColor = currentColor;
		isCorrect = (correctColor == currentColor);
		rawImage.color = currentColor;
	}
}



public class RockManager : SingletonMonoBehaviour<RockManager>
{
	private Dictionary<string, IntRockData> iRocks = new Dictionary<string, IntRockData> ();
	private Dictionary<string, ColorRockData> cRocks = new Dictionary<string, ColorRockData> ();

	public void AddIRocks (string name, GameObject obj, int num, int dNum)
	{
		iRocks.Add (name, new IntRockData (obj.GetComponent<Text> (), num, dNum));
	}

	public void ChangeNumber (string name, int[] nums)
	{
		IntRockData intRockData = iRocks [name];

		for (int i = 0; i < nums.Length; i++) {
			if (nums [i] == intRockData.currentNum) {
				int j = i + 1;
				if (j >= nums.Length)
					j = 0;
				intRockData.currentNum = nums [j];
				break;
			}
		}
		intRockData.isCorrect = (intRockData.correctNum == intRockData.currentNum);
		intRockData.text.text = intRockData.currentNum.ToString ();
		iRocks [name] = intRockData;
	}

	public void AddCRocks (string name, GameObject obj, Color color, Color dColor)
	{
		cRocks.Add (name, new ColorRockData (obj.GetComponent<RawImage> (), color, dColor));
	}

	public void ChangeColor (string name, Color[] colors)
	{
		ColorRockData colorRockData = cRocks [name];

		for (int i = 0; i < colors.Length; i++) {
			if (colors [i] == colorRockData.currentColor) {
				int j = i + 1;
				if (j >= colors.Length)
					j = 0;
				colorRockData.currentColor = colors [j];
				break;
			}
		}
		colorRockData.isCorrect = (colorRockData.correctColor == colorRockData.currentColor);
		colorRockData.rawImage.color = colorRockData.currentColor;
		cRocks [name] = colorRockData;
	}

	public bool CheckRocks (string[] names)
	{
		for (int i = 0; i < names.Length; i++) {
			if (iRocks.ContainsKey (names [i])) {
				if (iRocks [names [i]].isCorrect == false)
					return false;
			} else if (cRocks.ContainsKey (names [i])) {
				if (cRocks [names [i]].isCorrect == false)
					return false;
			}
		}
		return true;
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
