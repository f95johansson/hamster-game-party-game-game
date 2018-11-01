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
		Handle.OnChange.AddListener(HandleChanged);
		Handle.transform.localPosition = VectorMath.VectorFromDegree(transform.eulerAngles.z) * EffectRadius;
	}

	public void HandleChanged()
	{
		EffectRadius = Vector3.Distance(transform.position, Handle.transform.position);
		RecomputePoints();
	}

	public Vector3 GetLookForce(Vector3[] positions3D)
	{
		
		foreach (var position3D in positions3D)
		{
			var position = position3D;
			var myPos = transform.position;
	
			var diffVector = (position - myPos);
			var distance = diffVector.magnitude;
			
			if (distance > EffectRadius) continue;
			
			var effect = (positions3D[0] - myPos).normalized * Force;
			effect.y = 0;

			return Vector3.Lerp(effect, Vector3.zero, distance / EffectRadius);
		}

		return Vector3.zero;
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
