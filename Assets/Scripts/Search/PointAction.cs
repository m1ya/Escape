using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAction : MonoBehaviour
{

	private CSVData pointData;

	//ポイントファイルをセットする
	void SetPointData (string fileName)
	{
		pointData = CSVReader.ReadCsv ("Points/" + fileName);
	}

	// Use this for initialization
	void Start ()
	{
		SetPointData ("RegisterPoints");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	//TODO: Moveを生成 Zoomを生成 その他を生成（Move,Zoomのこ）
}
