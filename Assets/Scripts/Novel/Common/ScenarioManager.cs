using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

[RequireComponent(typeof( TextController))]
public class ScenarioManager : SingletonMonoBehaviour<ScenarioManager> {

	private CSVData scenarioData;

	private int m_currentLine = 0;
	private bool m_isCallPreload = false;

	private TextController m_textController;
	private NameLabelController m_nameLabelController;
	private CommandController m_commandController;
	private ImageController m_imageController;

	//シナリオファイルをセットする
	void SetScenarioData(string fileName){
		scenarioData = CSVReader.ReadCsv ("Scenarios/" + fileName);
	}

	//次に表示する文字列をセットする
	void RequestNextLine ()
	{
		//3行目にセリフが入る設定
		string currentText = scenarioData.datas[m_currentLine][2];
		//1行目のキャラクター名を表示
		string currentName = scenarioData.datas[m_currentLine][0];
		//2行目の表情を設定
		string currentFace = scenarioData.datas[m_currentLine][1];

		m_currentLine++;

		//コマンドが入っていたら処理して次の行を表示する
		if (currentText [0] == '@' && m_commandController.LoadCommand (currentText)) {
			if( m_currentLine < scenarioData.lineNum)
				RequestNextLine ();
		} else {
			m_textController.SetNextLine(currentText);
			m_nameLabelController.SetNextName (currentName);
			if(currentFace != "")
				m_imageController.SetFaceImage (currentName, currentFace);
			m_isCallPreload = false;
		}

	}

	public void StartScenario(string scenarioName){
		m_currentLine = 0;
		m_isCallPreload = false;
		m_imageController.InitImages ();
		AudioManager.Instance.InitAudio ();
		SetScenarioData(scenarioName);
		RequestNextLine();
	}

	#region UNITY_CALLBACK

	void Start () {
		m_textController = GetComponent<TextController>();
		m_nameLabelController = GetComponent<NameLabelController> ();
		m_commandController = GetComponent<CommandController>();
		m_imageController = GetComponent<ImageController>();
	}

	void Update () 
	{
		if( m_textController.IsCompleteDisplayText  ){
			if( m_currentLine < scenarioData.lineNum)
			{
				if( !m_isCallPreload )
				{
					m_commandController.PreloadCommand(scenarioData.datas[m_currentLine][2]);
					m_isCallPreload = true;
				}

				if( Input.GetMouseButtonDown(0)){
					RequestNextLine();
				}
			}
		}else{
			if(Input.GetMouseButtonDown(0)){
				m_textController.ForceCompleteDisplayText();
			}
		}
	}

	#endregion
}