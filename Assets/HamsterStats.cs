using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HamsterStats : MonoBehaviour
{
	public uint Index;
	private Stat _speed;
	private Stat _weight;
	private Stat _turnSpeed;
	private Stat _friction;
	
	public readonly UnityEvent OnSelected = new UnityEvent();

	private void Start ()
	{
		var state = GameControl.Control.Inventory.hamsterStates[Index];

		if (state == null)
		{
			var cg = GetComponent<CanvasGroup>();
			cg.alpha = 0;
			cg.interactable = false;
			return;
		}
		
		var stats = GetComponentsInChildren<Stat>(); //Speed, Weight, Turn speed, Friction

		_speed = stats[0];
		_weight = stats[1];
		_turnSpeed = stats[2];
		_friction = stats[3];
		
		_speed.Points = state.SpeedLevel;
		_weight.Points = state.WeightLevel;
		_turnSpeed.Points = state.TurnSpeedLevel;
		_friction.Points = state.FrictionLevel;

		foreach (var c in GetComponentsInChildren<Component>())
		{
			Events.OnEvent(EventTriggerType.PointerClick, c, ev =>
			{
				SelectMe();
			});
		}
	}

	private void SelectMe()
	{
		FindObjectOfType<HamsterStart>().NewStats(_speed.Points, _weight.Points, _turnSpeed.Points, _friction.Points);
		OnSelected.Invoke();
	}
}
