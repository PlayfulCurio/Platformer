using UnityEngine;
using UnityEngine.Splines;

public class FoePlaneInput : PlaneInput
{
    [SerializeField] private SplineContainer _pathToFollow;

    private float _sqrDistanceToCountStep = .01f;
    private float _percentageStepLength = .02f;
    private float _currentPathPercentage;
    private Vector3 _directionToPathPoint, _currentPathPoint;

    private void FixedUpdate()
    {
        _currentPathPoint = _pathToFollow.EvaluatePosition(_currentPathPercentage);
        _directionToPathPoint = _currentPathPoint - transform.position;
        while (_directionToPathPoint.sqrMagnitude <= _sqrDistanceToCountStep && _currentPathPercentage < 1f)
        {
            _currentPathPercentage += _percentageStepLength;
            _currentPathPoint = _pathToFollow.EvaluatePosition(_currentPathPercentage);
            _directionToPathPoint = _currentPathPoint - transform.position;
        }
        MoveDirection = _directionToPathPoint.normalized;
    }
}
