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
    
    private Vector3 _lastNormalPos;
    private Quaternion _lastNormalRotation;

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
        if (_coolMovement)
        {
            CoolMovement();
        }
        else
        {
            NormalMovement();
        }
    }

    public float CameraDistanceXZ = 2f;
    public float CameraYWhenFollowing = 1f;
    private bool _coolMovement;

    private void CoolMovement()
    {
        var hs = FindObjectOfType<HamsterStart>();
        if (hs.HasHamster())
        {
            var t = hs.CurrentHamsterTransform();

            var offset = -t.forward;
            offset.y = 0;
            offset = offset.normalized;

            var newCamPos = offset * CameraDistanceXZ + t.position;
            newCamPos.y = CameraYWhenFollowing;

            transform.position = Vector3.Lerp(transform.position, newCamPos, .2f);

            var newCameraRotation = Quaternion.LookRotation(t.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, newCameraRotation,2f);
        }
        else
        {
            NotGoTime(); // goes back to boring mode automatically, TODO, maybe this should not be the cameras decision
        }
    }

    private void NormalMovement()
    {
        var zoom = ZoomDelta();
        var beforeFieldOfView = _camera.fieldOfView;

        var before = VectorMath.ToWorldPoint(_camera, Input.mousePosition, Vector3.zero, Vector3.up);
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView + zoom, MinFieldOfView, _zoomedOutFieldOfView);
        var after = VectorMath.ToWorldPoint(_camera, Input.mousePosition, Vector3.zero, Vector3.up);
        
        if (zoom < 0)
        {
            var target = transform.position + before - after;
            //transform.position = ClosestPointInDropZone(target);
            transform.position = target;
        }
        else if (zoom > 0)
        {
            var b = _camera.fieldOfView - beforeFieldOfView;
            var a = _zoomedOutFieldOfView - beforeFieldOfView; // whole distance

            if (a == 0) transform.position = _zoomedOutPosition;
            else transform.position = Vector3.Lerp(_zoomedOutPosition, transform.position, 1 - b / a);
        }
        
        _lastNormalPos = transform.position;
        _lastNormalRotation = transform.rotation;
    }


    public void GoTime()
    {
        _coolMovement = true;
    }

    public void NotGoTime()
    {
        _coolMovement = false;

        transform.position = _lastNormalPos;
        transform.rotation = _lastNormalRotation;
        
        Debug.Log("Not game time, resetting camera");
    }
}