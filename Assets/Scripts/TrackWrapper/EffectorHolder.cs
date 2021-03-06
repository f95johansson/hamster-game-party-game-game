﻿using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EffectorHolder : MonoBehaviour
{
    public readonly UnityEvent GoTimeListener = new UnityEvent(); //used by hamster start
    public TurnEffector TurnPrefab;
    public PushEffector PushPrefab;

    public CanvasGroup Interactive;

    public Image TurnButton;
    public Image PushButton;
    public Trash Trash;
    private readonly Stack<State> _redos = new Stack<State>();
    private readonly Stack<State> _states = new Stack<State>();

    private bool _goTime;

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
    private uint _numberCarrotsAllowed;
    private uint _numberFansAllowed;
    public bool Unlimited;

    public void Awake()
    {
        Application.targetFrameRate = 60; // tries to make all platforms of the game run at the same frame-rate
    }

    private void Start()
    {
        var playerData = GameControl.Control.PlayerData;

        _numberCarrotsAllowed = playerData.numberCarrotsAllowed;
        _numberFansAllowed = playerData.numberCatsAllowed;

        if (Unlimited)
        {
            _numberCarrotsAllowed = 99;
            _numberFansAllowed = 99;
        }
        
        _camera = Camera.main;
        _turnEffectors = new List<TurnEffector>();
        _pushEffectors = new List<PushEffector>();

        _eventSystem = FindObjectOfType<EventSystem>();

        // Should these maybe be changed to run in update?
        Events.OnEvent(EventTriggerType.PointerDown, TurnButton, e =>
        {
            if (!_gi.Grabbed && !_goTime && CarrotsLeft() > 0)
            {
                Grab(CreateTurner(ToWorldPoint(Input.mousePosition)).gameObject);
            }
        });

        Events.OnEvent(EventTriggerType.PointerDown, PushButton, e =>
        {
            if (!_gi.Grabbed && !_goTime && FansLeft() > 0)
            {
                Grab(CreatePusher(ToWorldPoint(Input.mousePosition)).gameObject);
            }
        });

        Save();
    }

    private uint CarrotsLeft()
    {
        var current = FindObjectsOfType<TurnEffector>().Length;
        var left = _numberCarrotsAllowed - current;
        return (left > 0) ? (uint) left : 0;
    }
    
    private uint FansLeft()
    {
        var current = FindObjectsOfType<PushEffector>().Length;
        var left = _numberFansAllowed - current;
        return (left > 0) ? (uint) left : 0;
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
        return VectorMath.ToWorldPoint(_camera, screenPos, transform.position, Vector3.up);
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
        if (!_goTime)
        {
            var carrotsLeft = CarrotsLeft();
            var fansLeft = FansLeft();
        
            TurnButton.enabled = carrotsLeft > 0;
            PushButton.enabled = fansLeft > 0;
        
            TurnButton.GetComponentInChildren<Text>().text = "" + carrotsLeft;
            PushButton.GetComponentInChildren<Text>().text = "" + fansLeft;
            
            var mousePos = Input.mousePosition;

            var mousePosWorld = ToWorldPoint(mousePos);
            var overGui = _eventSystem && _eventSystem.IsPointerOverGameObject();

            Trash.UpdateTrashCan(mousePos, _gi.Grabbed != null && !_gi.Grabbed.GetComponent<Handle>());

            if (_gi.Grabbed != null)
            {
                _gi.Grabbed.transform.position =
                    Trash.IsClose() ? CanvasToWorld(Trash.transform.position) : mousePosWorld + _gi.Offset;

                if (Input.GetMouseButtonUp(0))
                {
                    if (Trash.IsClose())
                    {
                        var toDelete = _gi.Grabbed;
                        Release();
                        Remove(toDelete);

                        Save();
                        _redos.Clear();
                    }
                    else if (!overGui)
                    {
                        _gi.Grabbed.transform.position = ClosestPointInDropZone(_gi.Grabbed.transform.position);
                        Release();
                        Save();
                        _redos.Clear();
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

    private void CreateFromState(State state)
    {
        //First nuke everything
        {
            Release();

            _pushEffectors.ForEach(p => { Destroy(p.gameObject); });
            _pushEffectors.Clear();

            _turnEffectors.ForEach(t => { Destroy(t.gameObject); });
            _turnEffectors.Clear();
        }

        //Re-instantiate from recovered state
        {
            foreach (var entity in state.Pushers)
            {
                var pusher = CreatePusher(entity.Position());
                pusher.Handle.transform.position = entity.HandlePosition();
            }

            foreach (var entity in state.Turners)
            {
                var turner = CreateTurner(entity.Position());
                turner.Handle.transform.position = entity.HandlePosition();
            }
        }
    }

    public void Undo()
    {
        if (_goTime) return;
        //Get previous state
        if (_states.Count == 0) return;
        var undo = _states.Pop();
        _redos.Push(undo);

        if (_states.Count == 0) return;
        var recoveredState = _states.Peek();

        CreateFromState(recoveredState);
    }

    public void Redo()
    {
        if (_goTime) return;
        if (_redos.Count == 0)
        {
            return;
        }

        CreateFromState(_redos.Pop());
        Save();
    }

    public void ToggleGoTime()
    {
        if (!_goTime)
        {
            _goTime = true;
            GoTimeListener.Invoke();
            Debug.Log("It is go-time!");

            var cameraMovement = _camera.gameObject.GetComponent<CameraMovement>();
            cameraMovement.GoTime();
            Interactive.alpha = 0;
            Interactive.interactable = false;
        }
        else
        {
            NotGameTime();
        }
    }

    public void NotGameTime()
    {
        _goTime = false;
        var cameraMovement = _camera.gameObject.GetComponent<CameraMovement>();
        cameraMovement.NotGoTime();
        Interactive.interactable = true;
        Interactive.alpha = 1;
    }

    public void HamsterDied()
    {
        if (_goTime)
        {
            NotGameTime();   
        }
    }


    public bool IsGoTime()
    {
        return _goTime;
    }
}