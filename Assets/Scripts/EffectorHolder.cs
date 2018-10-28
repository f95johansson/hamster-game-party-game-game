using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EffectorHolder : MonoBehaviour
{

	public TurnEffector TurnPrefab;
	public PushEffector PushPrefab;

	public Component TurnButton;
	public Component PushButton;
	public Component Trash;
	
	private Camera _camera;
	
	[CanBeNull]
	private GameObject _selected;
	private EventSystem _eventSystem;

	private bool _isOnTrash;
	
	private List<TurnEffector> _turnEffectors;
	private List<PushEffector> _pushEffectors;

	private void Start()
	{
		_turnEffectors = new List<TurnEffector>();
		_pushEffectors = new List<PushEffector>();

		_eventSystem = FindObjectOfType<EventSystem>();
		_camera = Camera.main;

		OnEvent(EventTriggerType.PointerDown, TurnButton, e =>
		{
			if (!_selected)
			{
				_selected = CreateTurner(GetMousePos());
			}
		});
		
		OnEvent(EventTriggerType.PointerDown, PushButton, e =>
		{
			if (!_selected)
			{
				_selected = CreatePusher(GetMousePos());
			}
		});
		
		//PointerUp does not work unless we PointerDown was also registered on the object (which is stupid)
		OnEvent(EventTriggerType.PointerEnter, Trash, e =>
		{
			_isOnTrash = true;
		});
		OnEvent(EventTriggerType.PointerExit, Trash, e =>
		{
			_isOnTrash = false;
		});
	}

	private static void OnEvent(EventTriggerType ett, Component s, UnityAction<BaseEventData> action)
	{
		var trigger = s.gameObject.AddComponent<EventTrigger>();
		var eventType = new EventTrigger.Entry {eventID = ett};
		eventType.callback.AddListener(action);
		trigger.triggers.Add(eventType);
	}

	private Vector3 GetMousePos()
	{
		var mousePos = Input.mousePosition;
		mousePos.z = _camera.transform.position.y;
		mousePos = _camera.ScreenToWorldPoint(mousePos);
		mousePos.y = transform.position.y;
		return mousePos;
	}

	public void Update()
	{
		var mousePos = GetMousePos();
		var overGui = _eventSystem && _eventSystem.IsPointerOverGameObject();
		
		var left = Input.GetMouseButtonDown(0);	
		var leftUp = Input.GetMouseButtonUp(0);	

		if (_selected != null)
		{
			_selected.transform.position = mousePos;
			
			if (leftUp) {
				if (!overGui)
				{
					_selected = null;
					
				} else if (_isOnTrash)
				{
					Remove(_selected);
					_selected = null;
				}		
			}
		}
		else if (left && !overGui)
		{
			_selected = Closest(mousePos, 2f);
		}
	}

	public Vector3 GetTurnFocus(Vector3 position, float radius)
	{
		return _turnEffectors
			.Select(a => a.GetLookForce(position, radius))
			.Aggregate(Vector3.zero, (a, b) => a + b)
			.normalized;
	}

	public Vector3 GetPushForce(Vector3 position)
	{
		return _pushEffectors
			.Select(a => a.GetPushForce(position))
			.Aggregate(Vector3.zero, (a, b) => a + b);
	}

	private IEnumerable<GameObject> AllGameObjects()
	{
		var set = new HashSet<GameObject>();

		foreach (var turnEffector in _turnEffectors)
		{
			set.Add(turnEffector.gameObject);
			set.Add(turnEffector.Handle.gameObject);
		}

		foreach (var pusher in _pushEffectors)
		{
			set.Add(pusher.gameObject);
			set.Add(pusher.Handle.gameObject);
		}

		return set;
	}

	private GameObject Closest(Vector3 position, float maxDistance = float.MaxValue)
	{
		var all = AllGameObjects();
		GameObject obj = null;
		
		foreach (var o in all)
		{
			var oDistance = Vector3.Distance(position, o.transform.position);
			if (oDistance <= maxDistance)
			{
				obj = o;
				maxDistance = oDistance;
			}
		}

		return obj;
	}

	private GameObject CreateTurner(Vector3 position)
	{
		var obj = Instantiate(TurnPrefab);
		obj.transform.position = position;
		_turnEffectors.Add(obj);
		return obj.gameObject;
	}

	private GameObject CreatePusher(Vector3 position)
	{
		var obj = Instantiate(PushPrefab);
		obj.transform.position = position;
		_pushEffectors.Add(obj);
		return obj.gameObject;
	}

	private void Remove(GameObject obj)
	{
		if (obj.GetComponent<Handle>()) return;
		
		_pushEffectors.Remove(obj.GetComponent<PushEffector>());
		_turnEffectors.Remove(obj.GetComponent<TurnEffector>());
		Destroy(obj);
	}
}
