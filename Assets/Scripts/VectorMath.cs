using UnityEngine;

public static class VectorMath {
	
	public static float DistanceLinePoint(Vector2 a, Vector2 b, Vector2 p)
	{
		var n = b - a;
		var pa = a - p;
 
		var c = Vector2.Dot( n, pa );
 
		// Closest point is a
		if ( c > 0.0f )
			return Vector2.Dot( pa, pa );
 
		var bp = p - b;
 
		// Closest point is b
		if ( Vector2.Dot( n, bp ) > 0.0f )
			return Vector2.Dot( bp, bp );
 
		// Closest point is between a and b
		var e = pa - n * (c / Vector2.Dot(n, n));
 
		return Mathf.Sqrt(Mathf.Abs(Vector2.Dot( e, e )));
	}

	public static Vector2 ToXZ(Vector3 v)
	{
		return new Vector2(v.x, v.z);
	}

	public static Vector3 FromXZ(Vector2 v, float yValue = 0)
	{
		return new Vector3(v.x, 0, v.y);
	}
}
