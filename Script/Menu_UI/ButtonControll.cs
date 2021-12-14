using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControll : MonoBehaviour
{
#region Public Functions
	#region In MenuScene
	public void newStart() // 새 게임 시작 버튼
	{
		Gamemanager.Instance.NewGameStart();
	}
	public void LoadStart() // 게임 불러오는 버튼
	{

	}
	public void optionPanal() // 옵션 패널로 전환 버튼
	{
		Gamemanager.Instance.PageController.TurnPageOff(PageType.Main, PageType.Option);
	}
	public void backToMainPanal() // 메인 패널로 전환 버튼
	{
		Gamemanager.Instance.PageController.TurnPageOff(PageType.Option, PageType.Main);
	}
	public void endingPanal() // 엔딩 보기 패널로 전환 버튼
	{
		Debug.Log("Working on it\nIt will be last work");
	}
	public void quitGame() // 게임 종료 버튼
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
	#endregion

	#region In GameScene
	public void gameOption() // 게임 내 옵션으로 전환 버튼 (일시정지)
	{
		Gamemanager.Instance.PageController.TurnPageOff(PageType.Game_Interface, PageType.Game_Option);
		Time.timeScale = 0f;
	}

	public void gameResume() // 게임 내로 전환 버튼 (일시정지 해제)
	{
		Gamemanager.Instance.PageController.TurnPageOff(PageType.Game_Option, PageType.Game_Interface);
		Time.timeScale = 1f;
	}

	public void gameToMain() // 저장 후 메인 화면으로
	{
		Gamemanager.Instance.SaveGameStatus();
	}
	#endregion
#endregion
}
