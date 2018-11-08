using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class EffectorHolder : MonoBehaviour
{

	public TurnEffector TurnPrefab;
	public PushEffector PushPrefab;

	public Component TurnButton;
	public Component PushButton;
	public Trash Trash;

	private readonly Stack<State> _states = new Stack<State>();

	private Camera _camera;

	private struct GrabbedInfo
	{
		[CanBeNull] public GameObject Grabbed;
		public Vector3 Offset;
		public int OldRenderQueue;
		public const int RenderQueueMax = 5000;
	}
	
	private GrabbedInfo _gi;
	
	private EventSystem _eventSystem;
	
	private List<TurnEffector> _turnEffectors;
	private List<PushEffector> _pushEffectors;

	public MeshCollider DropZone;
	
	public void Awake()
	{
		Application.targetFrameRate = 60; // tries to make all platforms of the game run at the same frame-rate
	}

	private void Start()
	{	
		_turnEffectors = new List<TurnEffector>();
		_pushEffectors = new List<PushEffector>();

		_eventSystem = FindObjectOfType<EventSystem>();
		_camera = Camera.main;

		// Should these maybe be changed to run in update?
		Events.OnEvent(EventTriggerType.PointerDown, TurnButton, e =>
		{
			if (!_gi.Grabbed)
			{
				Grab(CreateTurner(ToWorldPoint(Input.mousePosition)).gameObject);
			}
		});
		
		Events.OnEvent(EventTriggerType.PointerDown, PushButton, e =>
		{
			if (!_gi.Grabbed)
			{
				Grab(CreatePusher(ToWorldPoint(Input.mousePosition)).gameObject);
			}
		});
	}

	private void Grab(GameObject toGrab)
	{
		if (!toGrab) return;
		_gi.Grabbed = toGrab;

		var r = toGrab.GetComponent<Renderer>();

		_gi.OldRenderQueue = r.material.renderQueue;
		r.material.renderQueue = GrabbedInfo.RenderQueueMax;
	}
	
	private void Release()
	{
		if (_gi.Grabbed == null) return;
		var r = _gi.Grabbed.GetComponent<Renderer>();
		r.material.renderQueue = _gi.OldRenderQueue;
		_gi.Grabbed = null;
	}
	
	private Vector3 ToWorldPoint(Vector3 screenPos) 
	{
		var plane = new Plane(Vector3.up, transform.position);
		var ray = _camera.ScreenPointToRay(screenPos);

		float distance;
		if (plane.Raycast(ray, out distance)){
			return ray.GetPoint(distance);
		}

		Debug.Log("We missed the y-plane, maybe we should handle this better, one idea is to bound where you can place effectors");
		return VectorMath.FromXZ(Vector3.zero);
	}

	private Vector3 ClosestPointInDropZone(Vector3 point)
	{
		var min = DropZone.bounds.min;
		var max = DropZone.bounds.max;
		
		if (point.x < min.x) point.x = min.x;
		else if (point.x > max.x) point.x = max.x;
		
		if (point.z < min.z) point.z = min.z;
		else if (point.z > max.z) point.z = max.z;

		return point;
	}

	public void Update()
	{	
		var mousePos = Input.mousePosition;
		
		var mousePosWorld = ToWorldPoint(mousePos);
		var overGui = _eventSystem && _eventSystem.IsPointerOverGameObject();
		
		Trash.UpdateTrashCan(mousePos, _gi.Grabbed != null && !_gi.Grabbed.GetComponent<Handle>());

		if (_gi.Grabbed != null)
		{
			_gi.Grabbed.transform.position = Trash.IsClose() ? CanvasToWorld(Trash.transform.position) : mousePosWorld + _gi.Offset;

			if (Input.GetMouseButtonUp(0))
			{
				if (Trash.IsClose())
				{
					var toDelete = _gi.Grabbed;
					Release();
					Remove(toDelete);
					
					Save();
				}
				else if (!overGui)
				{
					_gi.Grabbed.transform.position = ClosestPointInDropZone(_gi.Grabbed.transform.position);
					Release();
					Save();
				}
			}
		}
		else if (Input.GetMouseButtonDown(0) && !overGui)
		{
			Grab(Closest(mousePosWorld, 2f));
			if (_gi.Grabbed != null)
			{
				_gi.Offset = _gi.Grabbed.transform.position - mousePosWorld;
				_gi.Grabbed.GetComponent<Renderer>().material.renderQueue = 5000;
			}
		}
	}


	private Vector3 CanvasToWorld(Vector3 canvasPoint)
	{
		return ToWorldPoint(canvasPoint);
	}

	public Vector3 GetTurnFocus(Vector3[] positions)
	{
		return _turnEffectors
			.Select(a => a.GetLookForce(positions))
			.Aggregate(Vector3.zero, (a, b) => a + b)
			.normalized;
	}

	public Vector3 GetPushForce(Vector3[] positions)
	{
		return _pushEffectors
			.Select(a => a.GetPushForce(positions))
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

	private TurnEffector CreateTurner(Vector3 position)
	{
		var obj = Instantiate(TurnPrefab);
		obj.transform.position = position;
		_turnEffectors.Add(obj);
		return obj;
	}

	private PushEffector CreatePusher(Vector3 position)
	{
		var obj = Instantiate(PushPrefab);
		obj.transform.position = position;
		_pushEffectors.Add(obj);
		return obj;
	}

	private void Save()
	{
		_states.Push(new State(_turnEffectors, _pushEffectors));
	}

	private void Remove(GameObject obj)
	{
		if (obj.GetComponent<Handle>()) return;
		
		//sort of hack-ish, if an object does not have the desired component it returns null and removing null does not do anything
		_pushEffectors.Remove(obj.GetComponent<PushEffector>());
		_turnEffectors.Remove(obj.GetComponent<TurnEffector>());
		
		Destroy(obj);
	}

	public void Undo()
	{
		//First nuke everything
		{
			//TODO, what do we do with the currently grabbed thing. Not currently a problem since you need to click the undo button
			_pushEffectors.ForEach(p =>
			{
				Destroy(p.gameObject);
			});
			_pushEffectors.Clear();
		
			_turnEffectors.ForEach(t =>
			{
				Destroy(t.gameObject);
			});
			_turnEffectors.Clear();
		}
		
		//Get previous state
		if (_states.Count == 0) return;
		_states.Pop();
		if (_states.Count == 0) return;
		var recoveredState = _states.Peek();
		
		//Re-instantiate from recovered state
		{
			foreach (var entity in recoveredState.Pushers)
			{
				var pusher = CreatePusher(entity.Position());
				pusher.Handle.transform.position = entity.HandlePosition();
			}

			foreach (var entity in recoveredState.Turners)
			{
				var turner = CreateTurner(entity.Position());
				turner.Handle.transform.position = entity.HandlePosition();
			}
		}
	}
}