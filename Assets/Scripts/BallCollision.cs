
using UnityEngine;

public struct BallCollision
{
    private readonly Vector3[] _absolutePoints;
    private readonly Vector3[] _localPoints;

    public BallCollision(float radius, uint numberOfPointsAlongRing, uint numberOfRings)
    {
        
        _absolutePoints = new Vector3[1 + numberOfPointsAlongRing * numberOfRings];
        _localPoints = new Vector3[_absolutePoints.Length];

        _localPoints[0] = Vector3.zero;

        var index = 0;
        
        for (var l = 0; l < numberOfRings; l++)
        {
            var yAngle = l * 2 * Mathf.PI / numberOfRings;

            var y = Mathf.Cos(yAngle) * radius;

            for (var p = 0; p < numberOfPointsAlongRing; p++)
            {
                var angle = numberOfPointsAlongRing * 2 * Mathf.PI / p;
                var x = Mathf.Cos(angle) * radius;
                var z = Mathf.Sin(angle) * radius;
                
                _localPoints[index] = new Vector3(x, y, z);
                index++;
            }
        }
    }

    public Vector3[] GetPoints(Vector3 center)
    {   
        for (var i = 0; i < _localPoints.Length; i++)
        {
            _absolutePoints[i] = _localPoints[i] + center;
        }
        return _absolutePoints;
    }
}
