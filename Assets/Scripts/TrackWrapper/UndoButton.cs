using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{
	public EffectorHolder EffectorHolder;
	public string TriggerName = "Undo";
	public string HoverName = "Hover";

	private Image _undoImage;
	private Animator _animator;

	private void Start ()
	{
		_undoImage = GetComponent<Image>();
		_animator = GetComponent<Animator>();
		
		Events.OnEvent(EventTriggerType.PointerClick, _undoImage, e =>
		{
			EffectorHolder.Undo();
			_animator.SetTrigger(TriggerName);
		});
		
		Events.OnEvent(EventTriggerType.PointerEnter, _undoImage, e =>
		{
			_animator.SetBool(HoverName, true);
		});
		
		Events.OnEvent(EventTriggerType.PointerExit, _undoImage, e =>
		{
			_animator.SetBool(HoverName, false);
		});
	}
}
