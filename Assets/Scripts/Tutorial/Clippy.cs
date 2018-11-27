using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clippy : MonoBehaviour {

	private Animator _animator;
	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
	}

	public void Jump() {
		_animator.Play("ClippyJump");
	}
}
