using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PushEffector : MonoBehaviour
{
	[Range(0, 10)]
	public float Strength = 3;
	
	public float Far = 2;
	private float _width;
	private Vector2[] _points;
	private LineRenderer _line;
	
	public Handle Handle;

	private void Awake ()
	{
		_width = transform.localScale.x;
		_line = GetComponent<LineRenderer>();
		_line.useWorldSpace = true;

		Handle = Instantiate(Handle);
		Handle.transform.parent = transform;
		Handle.transform.localPosition = VectorMath.VectorFromDegree(0) * Far;
		Handle.OnChange.AddListener(HandleChanged);
	}

	private void Start()
	{
		RecomputePoints();
	}

	public void HandleChanged()
	{
		var position = Handle.transform.position;
		Far = Vector3.Distance(transform.position, Handle.transform.position);
		transform.rotation = VectorMath.FromToRotation(transform.position, position);
		Handle.transform.position = position;
		RecomputePoints();
	}

	public void RecomputePoints()
	{
		
		var dir = transform.eulerAngles.y;
		var left = VectorMath.VectorFromDegree2D(dir-90) * _width/2;
		var right = VectorMath.VectorFromDegree2D(dir+90) * _width/2;
		
		var farVector = VectorMath.VectorFromDegree2D(dir) * Far;
		
		_points = new Vector2[4];
		_points[0] = left;
		_points[1] = right;
		_points[2] = right + farVector;
		_points[3] = left + farVector;

		
		if (!_line) return;
		_line.positionCount = _points.Length;
		for (var i = 0; i < _points.Length; i++)
		{
			_line.SetPosition(i, transform.position + VectorMath.FromXZ(_points[i]));
		}
	}

	public Vector3 GetPushForce(Vector3 position3D)
	{
		if (_points == null) return Vector3.zero;
		
		float distance;
		if (!ContainsPoint(position3D, out distance))
		{
			return Vector3.zero;
		}
		
		var mag = 1 - distance/Far;
		var pushVector = VectorMath.VectorFromDegree(transform.eulerAngles.y) * Strength * mag;

		return pushVector;
	}

	private bool ContainsPoint(Vector3 position, out float distance)
	{
		//positions order is {left, right, rightFar, leftFar}
		var left = _points[0];
		var right = _points[1];
		var rightFar = _points[2];
		var leftFar = _points[3];
		
		var relativePosition = VectorMath.ToXZ(position - transform.position);

		distance = -1;

		var distanceY = VectorMath.DistanceLinePoint(left, right, relativePosition);
		if (distanceY > Far) return false;
		
		var distanceYFar = VectorMath.DistanceLinePoint(leftFar, rightFar, relativePosition);
		if (distanceYFar > Far) return false;
		
		var distanceLeft = VectorMath.DistanceLinePoint(left, leftFar, relativePosition);
		if (distanceLeft > _width) return false;
		
		var distanceRight = VectorMath.DistanceLinePoint(right, rightFar, relativePosition);
		if (distanceRight > _width) return false;

		distance = distanceY;
		return true;
	}
}
