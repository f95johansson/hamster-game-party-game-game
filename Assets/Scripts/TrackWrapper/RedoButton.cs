using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RedoButton : MonoBehaviour
{
	public EffectorHolder EffectorHolder;
	public string TriggerName = "Redo";
	public string HoverName = "Hover";

	private Image _redo;
	private Animator _animator;

	private void Start ()
	{
		_redo = GetComponent<Image>();
		_animator = GetComponent<Animator>();
		
		Events.OnEvent(EventTriggerType.PointerClick, _redo, e =>
		{
			EffectorHolder.Redo();
			_animator.SetTrigger(TriggerName);
		});
		
		Events.OnEvent(EventTriggerType.PointerEnter, _redo, e =>
		{
			_animator.SetBool(HoverName, true);
		});
		
		Events.OnEvent(EventTriggerType.PointerExit, _redo, e =>
		{
			_animator.SetBool(HoverName, false);
		});
	}
}
