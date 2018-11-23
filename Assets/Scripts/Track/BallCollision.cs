
using UnityEngine;

public struct BallCollision
{
    private readonly Vector3[] _absolutePoints;
    private readonly Vector3[] _localPoints;
    private const float Tau = Mathf.PI * 2;

    public BallCollision(float radius, uint nrOfRingPoints)
    {
        _absolutePoints = new Vector3[1 + nrOfRingPoints];
        _localPoints = new Vector3[_absolutePoints.Length];

        _localPoints[0] = Vector3.zero;

        var index = 1;
        var angleDiff = Tau / nrOfRingPoints;
        
        for (var p = 0; p < nrOfRingPoints; p++)
        {
            var angle = p * angleDiff;
            var x = Mathf.Cos(angle) * radius;
            var z = Mathf.Sin(angle) * radius;
            
            _localPoints[index] = new Vector3(x, 0, z);
            index++;
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
