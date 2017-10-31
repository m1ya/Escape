using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct CSVData{
	public int lineNum;
	public List<string[]> datas;

	public CSVData(int lineNum, List<string[]> datas){
		this.lineNum = lineNum;
		this.datas = datas;
	}
}