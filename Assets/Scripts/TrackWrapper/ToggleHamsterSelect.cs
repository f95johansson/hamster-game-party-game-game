using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleHamsterSelect : MonoBehaviour
{
	public CanvasGroup Selector;

	public void Start()
	{
		Selector.interactable = false;
		Selector.blocksRaycasts = false;
		Selector.alpha = 0;
		
		Events.OnEvent(EventTriggerType.PointerClick, GetComponent<Image>(), ev =>
		{
			if (Selector.interactable)
			{
				Selector.interactable = false;
				Selector.blocksRaycasts = false;
				Selector.alpha = 0;
			}
			else
			{
				Selector.interactable = true;
				Selector.blocksRaycasts = true;
				Selector.alpha = 1;
			}
		});
	}
}
