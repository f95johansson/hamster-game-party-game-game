using System.Collections;
using System.Collections.Generic;
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
		SceneManager.LoadScene("TrackWrapper", LoadSceneMode.Additive);
	}

    public static void StartLevelSelect() {
        SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);   
    }
}
