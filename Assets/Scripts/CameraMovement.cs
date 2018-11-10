using System;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    public float PinchZoomSpeed = .5f;
    private Camera _camera;
    private Vector3 _zoomedOutPosition;
    private float _zoomedOutFieldOfView;
    private const float MinFieldOfView = 15f;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        Assert.IsNotNull(_camera, "We must have a camera tagged Main in scene");
        _zoomedOutPosition = transform.position;
        _zoomedOutFieldOfView = _camera.fieldOfView;
    }

    private float ZoomDelta()
    {
        if (!Input.touchSupported || Application.platform == RuntimePlatform.WebGLPlayer) return -Input.mouseScrollDelta.y;
        if (Input.touchCount != 2) return 0;

        var touches = Input.touches;
        var prev0 = touches[0].position - touches[0].deltaPosition;
        var prev1 = touches[1].position - touches[1].deltaPosition;

        var prevZoomDistance = Vector2.Distance(prev0, prev1);
        var zoomDistance = Vector2.Distance(touches[0].position, touches[1].position);

        var zoomDelta = prevZoomDistance - zoomDistance;
        
        return PinchZoomSpeed * zoomDelta;
    }

    private void Update()
    {
        var zoom = ZoomDelta();
        var before = VectorMath.ToWorldPoint(_camera, Input.mousePosition, Vector3.zero);
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView + zoom, MinFieldOfView, _zoomedOutFieldOfView);
        var after = VectorMath.ToWorldPoint(_camera, Input.mousePosition, Vector3.zero);
        transform.position += before - after;
    }
}