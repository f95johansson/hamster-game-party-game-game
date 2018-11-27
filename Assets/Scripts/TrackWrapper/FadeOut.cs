using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
	public float WaitBeforeFadingOut = 3f;
	public float FadeTime = 3f;

	private CanvasGroup _canvasGroup;

	
	private void Start()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
		_canvasGroup.alpha = 1;
	}
	
	private void Update ()
	{
		if (WaitBeforeFadingOut > 0)
		{
			WaitBeforeFadingOut -= Time.deltaTime;
		}
		else
		{
			if (_canvasGroup.alpha > 0)
			{
				_canvasGroup.alpha -= FadeTime * Time.deltaTime;
			}
			
			if (_canvasGroup.alpha <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
