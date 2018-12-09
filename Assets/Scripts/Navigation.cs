using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
	public static void StartTrack(string sceneName)
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single); // the tracks load the wrapper
	}

    public static void StartLevelSelect() {
        SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);   
    }

	// This is not static so it can be used as a target for OnClickListener
	public void GoToLevelSelect()
	{
		StartLevelSelect();
	}
	
	public void GoToIntro()
	{
		StartTrack("IntroTrack");
	}

    public void GoToIntroOfTheGame_NotTheTrack() {
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }

	public static void StartHome()
	{
		SceneManager.LoadScene("HamsterCage", LoadSceneMode.Single); 
	}

	public static void StartShop()
	{
		SceneManager.LoadScene("HamsterShop", LoadSceneMode.Single); 
	}
}
