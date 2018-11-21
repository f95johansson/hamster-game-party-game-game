using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
	private void Start () {
		StartTrack("Track01");
	}

	public static void StartTrack(string sceneName)
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
		AddWrapper();
	}

    public static void StartLevelSelect() {
        SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);   
    }

	public static void AddWrapper()
	{
		SceneManager.LoadScene("TrackWrapper", LoadSceneMode.Additive);
	}
}
