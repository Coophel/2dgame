using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneControll : MonoBehaviour
{
	public static SceneControll Instance;
	public Animator transScene;

#region Unity Functions
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
	/* 씬 전환 함수 (씬 이름으로 전환)
	   await 으로 로딩 진행도를 띄울수도 있음. (추후) */
	public async void LoadScene(string SceneName)
	{
		var scene = SceneManager.LoadSceneAsync(SceneName);
		scene.allowSceneActivation = false;
		transScene.SetTrigger("Fade");
		await Task.Delay(1000);
		scene.allowSceneActivation = true;
	}
#endregion

#region Private Functions
#endregion
}
