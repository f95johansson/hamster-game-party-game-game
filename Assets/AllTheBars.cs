using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTheBars : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private Bar GetBar(uint i) {
        var bars = GetComponentsInChildren<Bar>();
        return bars[i];
    }

    public Bar Weight() {
        return GetBar(0);
    }


    public Bar Speed()
    {
        return GetBar(1);
    }


    public Bar Friction()
    {
        return GetBar(2);
    }


    public Bar TurnSpeed()
    {
        return GetBar(3);
    }
}
