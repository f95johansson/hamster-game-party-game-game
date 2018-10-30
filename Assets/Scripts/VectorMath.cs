using UnityEngine;

public class VectorMath {

	public static float HeadingDegrees(Vector3 v2)
	{
		return Mathf.Atan2(v2.z, v2.x) * Mathf.Rad2Deg;
	}
	
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
	
	public static bool PolygonContainsPoint(Vector3[] polyPoints, Vector3 p) { 
		var j = polyPoints.Length-1;
		var inside = false; 
		for (var i = 0; i < polyPoints.Length; j = i++) {
			if (((polyPoints[i].y <= p.z && p.z < polyPoints[j].z) ||
			     (polyPoints[j].y <= p.z && p.z < polyPoints[i].z)) &&
			    (p.x < (polyPoints[j].x - polyPoints[i].x) * (p.z - polyPoints[i].z) /
			     (polyPoints[j].z - polyPoints[i].z) + polyPoints[i].x))
			{
				inside = !inside; 	
			}
		} 
		return inside; 
	}
	
	public static Vector3 VectorFromRadians(float radians, float y = 0)
	{
		return new Vector3(Mathf.Cos(radians), y, Mathf.Sin(radians));
	}
	
	public static Vector3 VectorFromDegree(float degrees, float y = 0)
	{
		return VectorFromRadians(degrees * Mathf.Deg2Rad, y);
	}

	public static Vector2 VectorFromDegree2D(float degrees)
	{
		return ToXZ(VectorFromDegree(degrees));
	}

	public static Quaternion FromToRotation(Vector3 a, Vector3 b)
	{
		var dx = b.x - a.x;
		var dz = b.z - a.z;
		var angle = Mathf.Atan2(dz, dx) * Mathf.Rad2Deg;
		return Quaternion.Euler(new Vector3(0, angle, 0));
	}

	public static Vector3 FromXZ(Vector2 v, float yValue = 0)
	{
		return new Vector3(v.x, 0, v.y);
	}
}
