using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    public float PinchZoomSpeed = .5f;
    public float ScrollZoomSpeed = 1f;
    private Camera _camera;
    private Vector3 _zoomedOutPosition;
    private float _zoomedOutFieldOfView;
    private const float MinFieldOfView = 15f;
    public MeshCollider DropZone;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        Assert.IsNotNull(_camera, "We must have a camera tagged Main in scene");
        _zoomedOutPosition = transform.position;
        _zoomedOutFieldOfView = _camera.fieldOfView;
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

    private float ZoomDelta()
    {
        if (!Input.touchSupported || Application.platform == RuntimePlatform.WebGLPlayer) return -Input.mouseScrollDelta.y * ScrollZoomSpeed;
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
        var beforeFieldOfView = _camera.fieldOfView;
        
        var before = VectorMath.ToWorldPoint(_camera, Input.mousePosition, Vector3.zero);
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView + zoom, MinFieldOfView, _zoomedOutFieldOfView);
        var after = VectorMath.ToWorldPoint(_camera, Input.mousePosition, Vector3.zero);
        
        if (zoom < 0) {
            var target = transform.position + before - after;
            transform.position = ClosestPointInDropZone(target);
        }
        else if (zoom > 0)
        {
            var b = _camera.fieldOfView - beforeFieldOfView;
            var a = _zoomedOutFieldOfView - beforeFieldOfView; // whole distance

            if (a == 0) transform.position = _zoomedOutPosition;
            else transform.position = Vector3.Lerp(_zoomedOutPosition, transform.position, 1 - b / a);
        }
    }
}