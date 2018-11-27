using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress {

    private Dictionary<string, bool[]> _tracksProgress = new Dictionary<string, bool[]>();

    public void SaveTrackProgress(string level, bool star1, bool star2, bool star3) {
        _tracksProgress[level] = new[] {star1, star2, star3};
    }

    public void SaveTrackProgress(string level, bool[] stars) {
        if (stars.Length < 3) {
            throw new ArgumentException("Must be 3 stars in array");
        }
        SaveTrackProgress(level, stars[0], stars[1], stars[2]);
    }

    public void SaveTrackProgress(string level, int starNumber, bool star) {
        if (starNumber < 1 || 3 < starNumber) {
            throw new ArgumentException(String.Format("Invalid star number, should be between 1 and 3, was {0}", starNumber), "starNumber");
        }

        if (!_tracksProgress.ContainsKey(level)) {
            _tracksProgress[level] = new[] {false, false, false};
        }
        var progress = _tracksProgress[level];
        progress[starNumber-1] = star;
        _tracksProgress[level] = progress;
    }

    public bool[] GetProgressOfTrack(string level) {
        if (_tracksProgress.ContainsKey(level)) {
            return _tracksProgress[level];
        } else {
            return new bool[] {false, false, false};
        }
    }


    public ProgressData Serialize() {
        ProgressData progress = new ProgressData();
        string[] names = new string[_tracksProgress.Count];
        bool[][] values = new bool[_tracksProgress.Count][];
        var i = 0;
        foreach (var track in _tracksProgress) {
            names[i] = track.Key;
            values[i] = track.Value;
            i++;
        }
        progress.trackNames = names;
        progress.trackProgress = values;
        return progress;
    }

    public static GameProgress FromSerialized(ProgressData data) {
        GameProgress progress = new GameProgress();
        for (int i = 0; i < data.trackNames.Length; i++) {
            progress.SaveTrackProgress(data.trackNames[i], data.trackProgress[i]);
        }
        return progress;
    }

    public string ToString() {
        string build = "";
        foreach (var track in _tracksProgress) {
            build += String.Format("[ {0}: {1}, {2}, {3} ], ", track.Key, track.Value[0].ToString(), track.Value[1].ToString(), track.Value[2].ToString());
        }
        return build;
    }
}

[Serializable]
public struct ProgressData {
    public string[] trackNames;
    public bool[][] trackProgress;
}
