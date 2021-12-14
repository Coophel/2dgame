using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;
	public MainResource GameResource;
	public PageController PageController;

	public int days;

#region Unity Funtions
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
#endregion

#region Public Functions
	public void NewGameStart()
	{
		days = 1;
		GameResource.initializeNewGame();
		PageController = null;
		SceneControll.Instance.LoadScene("GameScene");
	}

	public void SaveGameStatus()
	{
		// save game data functions.


		PageController = null;
		SceneControll.Instance.LoadScene("MainScene");
	}

	public void LoadGameStart()
	{
		// make load data
	}

//  12 . 01 starting code
	public void PassDays()
	{
		Debug.Log("Working on it");

		CheckMovement();
		// if ture : start wrap ani. -> move to next map <if (energy is enough)>
		// if false : not move and start ani .  check next.

		CheckEvent();
		// if ture : Go to Event Dialgue.
		// if false : check next.

		CheckBattle();
		// if ture : GO to battle -> using ammo / Or use energy to run away
		// if false : check next.

		UseResource();
		// minuse resource about ship and people

		CheckLive();
		// check our ship can survival and add days
	}
#endregion

#region Private Functions
	private void CheckMovement()
	{

	}

	private void CheckEvent()
	{

	}

	private void CheckBattle()
	{

	}

	private void UseResource()
	{

	}

	private void CheckLive()
	{
		days++;
	}
#endregion
}
