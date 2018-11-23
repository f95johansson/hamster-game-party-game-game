using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class WinInformation : MonoBehaviour
{
	private CanvasGroup _gui;
	
	private void Start ()
	{
		_gui = GetComponent<CanvasGroup>();
	}
	
	public void Show()
	{
		_gui.alpha = 1;
		_gui.interactable = true;
		_gui.blocksRaycasts = true;
	}

	public void Hide()
	{
		_gui.alpha = 0;
		_gui.interactable = false;
		_gui.blocksRaycasts = false;
	}
}
