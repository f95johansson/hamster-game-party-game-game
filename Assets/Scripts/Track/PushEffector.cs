using System.Collections.Generic;
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
	private ParticleSystem _ps;

	private void Awake ()
	{
		_width = transform.localScale.x;
		_line = GetComponent<LineRenderer>();
		_line.useWorldSpace = true;

		Handle = Instantiate(Handle);
		Handle.transform.parent = transform;
		Handle.transform.localPosition = new Vector3(1, 0, 0) * Far;
		Handle.OnChange.AddListener(HandleChanged);
		_ps = GetComponentInChildren<ParticleSystem>();
	}

	private void Start()
	{
		RecomputePoints();
	}

	public void HandleChanged()
	{
		var position = Handle.transform.position;
		Far = Vector3.Distance(transform.position, Handle.transform.position);
		
		var diff = transform.position - Handle.transform.position;
		diff.y = 0;
		
		transform.rotation = Quaternion.LookRotation(diff);
		Handle.transform.position = position;
		RecomputePoints();
		RecomputeParticles();
	}

	private void RecomputeParticles()
	{
		var newSpeed = -1 * Far / ( _ps.main.startLifetime.constant);
		var ps = _ps.main;
		ps.startSpeedMultiplier = newSpeed;
	}

	public void RecomputePoints()
	{
		var farVector = VectorMath.ToXZ(Handle.transform.position - transform.position);
		var perpendicular = Vector2.Perpendicular(farVector).normalized; // in direction of right
		
		var left = -perpendicular * _width/2;
		var right = perpendicular * _width/2;
		
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

	public Vector3 GetPushForce(IEnumerable<Vector3> positions3D)
	{
		if (_points == null) return Vector3.zero;
		
		foreach (var pos in positions3D)
		{
			float distance;
			if (ContainsPoint(pos, out distance))
			{
				var mag = (Mathf.Abs(Far) <= float.Epsilon) ? 0 : 1 - distance/Far;
				var force = (Handle.transform.position - transform.position).normalized * Strength * mag;

				return force * Far;
			}
		}

		return Vector3.zero;
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
		return (Handle.transform.position - transform.position).normalized * Strength * mag;
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
