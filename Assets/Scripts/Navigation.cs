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

	public static void AddWrapper()
	{
		SceneManager.LoadScene("TrackWrapper", LoadSceneMode.Additive);
	}

	// This is not static so it can be used as a target for OnClickListener
	public void GoToLevelSelect()
	{
		StartLevelSelect();
	}
}
