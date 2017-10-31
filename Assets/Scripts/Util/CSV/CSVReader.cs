using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour {

	//CSVの中身をCsvData型の変数として読み込む
	public static CSVData ReadCsv(string fileName){
		List<string[]> csvDatas = new List<string[]>();
		int lineNum = 0;
		TextAsset csvFile = Resources.Load(fileName) as TextAsset;

		if( csvFile == null ){
			Debug.LogError("CSVファイルが見つかりませんでした");
			return new CSVData ();
		}

		StringReader reader = new StringReader(csvFile.text);

		while(reader.Peek() > -1) {
			string line = reader.ReadLine();
			csvDatas.Add(line.Split(',')); // リストに入れる
			lineNum++; // 行数加算
		}
		Resources.UnloadAsset(csvFile);
		return new CSVData (lineNum, csvDatas);
	}
		
}
