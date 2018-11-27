using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class UiAnimation : MonoBehaviour
{
	private Animator _animator;
	public string HoverName = "Hover";

	private void Start()
	{
		_animator = GetComponent<Animator>();
		
		Events.OnEvent(EventTriggerType.PointerEnter, this, arg0 =>
		{
			_animator.SetBool(HoverName, true);
		});
		
		Events.OnEvent(EventTriggerType.PointerExit, this, arg0 =>
		{
			_animator.SetBool(HoverName, false);
		});	
	}
}
