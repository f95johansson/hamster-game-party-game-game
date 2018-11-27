using System;
using UnityEngine;

public class LevelSelectShop : MonoBehaviour
{
	public float RotateSpeed = 20f;
	private Light _light;
	public float HighlightIntensity = 10;
	private float _defaultIntensity = 10;

	public void Start()
	{
		_light = GetComponentInChildren<Light>();
        var child = GetComponentInChildren<OnCliickThingy>();

        child.MouseExit.AddListener(OnMouseExit);
        child.MouseClick.AddListener(OnMouseUpAsButton);
        child.MouseEnter.AddListener(OnMouseEnter);

        _defaultIntensity = _light.intensity;
	}
	
	private void Update ()
    {
        transform.Rotate(new Vector3(0, 0, RotateSpeed * Time.deltaTime));
    }

	private void OnMouseUpAsButton()
	{
		if (Math.Abs(_light.intensity - HighlightIntensity) < 0.01f)
		{
			Navigation.StartTrack(gameObject.name);
		}
	}

	private void OnMouseEnter()
	{
		_light.intensity = HighlightIntensity;
	}

	private void OnMouseExit()
	{
		_light.intensity = _defaultIntensity;
	}
}
