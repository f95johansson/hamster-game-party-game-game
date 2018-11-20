using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackProgress : MonoBehaviour {

    private GameControl _game;

    void Start () {
        _game = GameControl.Control;
    }

	public void OnBackClick() {
        _game.Progress.SaveTrackProgress("Track01", 2, true);
        Navigation.StartLevelSelect();
    }
}
