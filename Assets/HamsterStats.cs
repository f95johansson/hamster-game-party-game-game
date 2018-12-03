using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HamsterStats : MonoBehaviour
{
	private Stat _speed;
	private Stat _weight;
	private Stat _turnSpeed;
	private Stat _friction;
	
	public readonly UnityEvent OnSelected = new UnityEvent();

	public uint SpeedPoints
	{
		get { return _speed.Points; }
	}
	
	public uint WeightPoints
	{
		get { return _weight.Points; }
	}
	
	public uint TurnSpeedPoints
	{
		get { return _turnSpeed.Points; }
	}
	
	public uint FrictionPoints
	{
		get { return _friction.Points; }
	}

	public void SetState(HamsterState state)
	{
		var stats = GetComponentsInChildren<Stat>(); //Speed, Weight, Turn speed, Friction

		_speed = stats[0];
		_weight = stats[1];
		_turnSpeed = stats[2];
		_friction = stats[3];
		
		_speed.Points = state.SpeedLevel;
		_weight.Points = state.WeightLevel;
		_turnSpeed.Points = state.TurnSpeedLevel;
		_friction.Points = state.FrictionLevel;

		GetComponent<Text>().text = state.HamsterName;

		foreach (var c in GetComponentsInChildren<Component>())
		{
			Events.OnEvent(EventTriggerType.PointerClick, c, ev =>
			{
				SelectMe();
			});
		}
	}

	public void SelectMe()
	{
		OnSelected.Invoke();
	}
}
