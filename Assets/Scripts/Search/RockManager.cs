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

public class RockManager : SingletonMonoBehaviour<RockManager>
{
	private Dictionary<string, IntRockData> rocks = new Dictionary<string, IntRockData> ();

	public void AddRocks (string name, GameObject obj, int num, int dNum)
	{
		rocks.Add (name, new IntRockData (obj.GetComponent<Text> (), num, dNum));
	}

	public void ChangeNumber (string name, int[] nums)
	{
		IntRockData intRockData = rocks [name];

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
		rocks [name] = intRockData;
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
