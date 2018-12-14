using UnityEngine;

public class WorldMapNode : MonoBehaviour {
	private const float Big = 1.4f;
	private float _normal = 1.4f;
	private float _goal;
	protected float Current;

	private void Start()
	{
		Init();
	}

	protected void Init()
	{
		_normal = transform.localScale.x;
		_goal = _normal;
		Current = _goal;

		if (GameControl.Control.Progress.HasCleared(gameObject.name))
		{
			var star = GetComponentsInChildren<SpriteRenderer>()[1];
			star.enabled = true;
		}
	}

	private void OnMouseEnter()
	{
		_goal = Big * _normal;
	}

	private void OnMouseExit()
	{
		_goal = _normal;
	}

	private void Update()
	{
		Current = Mathf.Lerp(Current, _goal, .1f);
		transform.localScale = new Vector3(Current, Current, transform.localScale.z);
	}
}
