using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{
	public EffectorHolder EffectorHolder;
	public string TriggerName = "Undo";
	public string HoverName = "Hover";

	private Image _image;
	private Animator _animator;

	// Use this for initialization
	private void Start ()
	{
		_image = GetComponent<Image>();
		_animator = GetComponent<Animator>();
		
		Events.OnEvent(EventTriggerType.PointerClick, _image, e =>
		{
			EffectorHolder.Undo();
			_animator.SetTrigger(TriggerName);
		});
		
		Events.OnEvent(EventTriggerType.PointerEnter, _image, e =>
		{
			_animator.SetBool(HoverName, true);
		});
		
		Events.OnEvent(EventTriggerType.PointerExit, _image, e =>
		{
			_animator.SetBool(HoverName, false);
		});
	}
}
