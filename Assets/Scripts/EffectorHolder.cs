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
	public Trash Trash;
	
	private Camera _camera;
	
	[CanBeNull]
	private GameObject _grabbed;
	private Vector3 _grabOffset;
	
	private EventSystem _eventSystem;
	
	private List<TurnEffector> _turnEffectors;
	private List<PushEffector> _pushEffectors;


	public Canvas Canvas;

	private void Start()
	{	
		_turnEffectors = new List<TurnEffector>();
		_pushEffectors = new List<PushEffector>();

		_eventSystem = FindObjectOfType<EventSystem>();
		_camera = Camera.main;

		// Should these maybe be changed to run in update?
		Events.OnEvent(EventTriggerType.PointerDown, TurnButton, e =>
		{
			if (!_grabbed)
			{
				_grabbed = CreateTurner(ToWorldPoint(Input.mousePosition));
			}
		});
		
		Events.OnEvent(EventTriggerType.PointerDown, PushButton, e =>
		{
			if (!_grabbed)
			{
				_grabbed = CreatePusher(ToWorldPoint(Input.mousePosition));
			}
		});
	}

	private Vector3 ToWorldPoint(Vector3 screenPos) // could be optimized by caching the plane but I don't think it is worth it
	{
		var plane = new Plane(Vector3.up, transform.position);
		var ray = _camera.ScreenPointToRay(screenPos);

		float distance;
		if (plane.Raycast(ray, out distance)){
			return ray.GetPoint(distance);
		}

		Debug.Log("We missed the y-plane, this should be impossible");
		return new Vector3(-1, -1, -1);
	}

	public void Update()
	{	
		var mousePos = Input.mousePosition;
		
		var mousePosWorld = ToWorldPoint(mousePos);
		var overGui = _eventSystem && _eventSystem.IsPointerOverGameObject();

		var canvasMousePos = ScreenToCanvas(mousePos);
		
		Trash.UpdateTrashCan(canvasMousePos, _grabbed != null && !_grabbed.GetComponent<Handle>());

		if (_grabbed != null)
		{
			_grabbed.transform.position = Trash.IsClose() ? CanvasToWorld(Trash.transform.position) : mousePosWorld + _grabOffset;

			if (Input.GetMouseButtonUp(0))
			{
				if (Trash.IsClose())
				{
					Remove(_grabbed);
					_grabbed = null;
				}
				else
				{
					_grabbed = null;
				}
			}
		}
		else if (Input.GetMouseButtonDown(0) && !overGui)
		{
			_grabbed = Closest(mousePosWorld, 2f);

			if (_grabbed != null)
			{
				_grabOffset = _grabbed.transform.position - mousePosWorld;	
			}
		}
	}

	private Vector3 ScreenToCanvas(Vector3 screenPoint)
	{
		screenPoint.z = Canvas.planeDistance;
		return _camera.ScreenToWorldPoint(screenPoint);
	}
	
	private Vector3 CanvasToWorld(Vector3 canvasPoint)
	{
		return ToWorldPoint(_camera.WorldToScreenPoint(canvasPoint));
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
		
		//sort of hack-ish, if an object does not have the desired component it returns null and removing null does not do anything
		_pushEffectors.Remove(obj.GetComponent<PushEffector>());
		_turnEffectors.Remove(obj.GetComponent<TurnEffector>());
		
		Destroy(obj);
	}
}
