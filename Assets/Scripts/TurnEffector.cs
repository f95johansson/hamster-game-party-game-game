using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TurnEffector : MonoBehaviour
{
	// positive is away from effector (repellent)
	// negative is towards the effector (attractor)
	public float Force = -3.0f;
	
	public float EffectRadius = 1;
	private LineRenderer _line;
	public Handle Handle;

	private void Awake()
	{
		_line = GetComponent<LineRenderer>();
		_line.useWorldSpace = true;

		Handle = Instantiate(Handle);
		Handle.transform.parent = transform;
		Handle.transform.localPosition = VectorMath.VectorFromDegree(transform.eulerAngles.z) * EffectRadius;
		Handle.OnChange.AddListener(HandleChanged);
	}

	private void Start()
	{
		RecomputePoints();
	}

	public void HandleChanged()
	{
		EffectRadius = Vector3.Distance(transform.position, Handle.transform.position);
		RecomputePoints();
	}

	public Vector3 GetLookForce(Vector3 position3D, float radius)
	{
		var position = VectorMath.ToXZ(position3D);
		var myPos = VectorMath.ToXZ(transform.position);
	
		var diffVector = position - myPos; // position from effector to position
		var mag = Math.Max(0, diffVector.magnitude - radius);
		var normalized = diffVector.normalized;

		var vec = Vector2.Lerp(normalized * Force, Vector2.zero, mag / EffectRadius);

		return VectorMath.FromXZ(vec.normalized);
	}
	
	private void Update()
	{
		if (transform.hasChanged)
		{
			RecomputePoints();
		}
	}
	
	public void RecomputePoints()
	{	
		if (!_line) return;
		const int nrOfSegments = 64;
		_line.positionCount = nrOfSegments;
		
		var angle = 0f;

		for (var i = 0; i < nrOfSegments; i++)
		{
			var x = transform.position.x + Mathf.Cos(angle) * EffectRadius;
			var z = transform.position.z + Mathf.Sin(angle) * EffectRadius;
			
			_line.SetPosition(i, new Vector3(x, transform.position.y, z));
			angle += (float) (Math.PI * 2) / nrOfSegments;
		}
	}
}
