using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracksController : MonoBehaviour {

	public GameObject TracksContainer;
	
	void Start () {
		foreach (Transform track in TracksContainer.transform) {
			if (GameControl.Control.Progress.GetProgressOfTrack(track.name)[0] == true) {
				track.Find("Star").gameObject.SetActive(true);
			}
		}
	}
}
