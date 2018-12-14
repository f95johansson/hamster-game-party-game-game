using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
	public CanvasGroup Menu;
	public CanvasGroup Sure;
	public Button Delete;
	public Button Cancel;

	private void Start ()
	{
		if (GameControl.HasNoSaveDate())
		{
			Destroy(gameObject);
		}
		
		Events.OnEvent(EventTriggerType.PointerClick,  GetComponent<Text>(), e =>
		{
			Show(Sure);
			Hide(Menu);
		});
		
		Cancel.onClick.AddListener(() =>
		{
			Show(Menu);
			Hide(Sure);
		});
		
		Delete.onClick.AddListener(DeleteProgress);
	}

	private static void DeleteProgress()
	{
		GameControl.Control.ClearAllData();
		Navigation.GoToIntroTrackStatic();
	}

	public void Hide(CanvasGroup cg)
	{
		cg.alpha = 0;
		cg.interactable = false;
		cg.blocksRaycasts = false;
	}
	
	public void Show(CanvasGroup cg)
	{
		cg.alpha = 1;
		cg.interactable = true;
		cg.blocksRaycasts = true;
	}
}
