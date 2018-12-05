using UnityEngine;

public class WorldMapNode : MonoBehaviour {
	private const float BIG = 1.4f;
	private float _normal = 1.4f;
	private float _goal;
	private float _current;

	private void Start()
	{
		_normal = transform.localScale.x;
		_goal = _normal;
		_current = 0;

		if (GameControl.Control.Progress.HasCleared(gameObject.name))
		{
			var star = GetComponentsInChildren<SpriteRenderer>()[1];
			star.enabled = true;
		}
	}
	
	private void OnMouseEnter()
	{
		_goal = BIG * _normal;
	}

	private void OnMouseExit()
	{
		_goal = _normal;
	}

	private void Update()
	{
		_current = Mathf.Lerp(_current, _goal, .1f);
		transform.localScale = new Vector3(_current, _current, transform.localScale.z);
	}
}
