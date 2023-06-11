using System.Linq;
using UnityEngine;

public class CarAIHandler : MonoBehaviour
{
    public enum AIMode
    {
        followPlayer, followWayPoints
    };

    [Header("AI settings")]
    public AIMode AICarMode;

    Vector3 _targetPosition = Vector3.zero;
    Transform _targetTransform = null;

    WayPointNode _currentWayPoint = null;
    WayPointNode[] _allWayPoints;

    private CarController _carController;

    private void Awake()
    {
        _carController = GetComponent<CarController>();
        _allWayPoints = FindObjectsOfType<WayPointNode>();
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        switch (AICarMode)
        {
            case AIMode.followPlayer:
                FollowPlayer();
                break;
            case AIMode.followWayPoints:
                FollowWayPoints();
                break;
            default:
                break;
        }

        inputVector.x = TurnTowardTarget();
        inputVector.y = 1.0f;

        _carController.SetInputVector(inputVector);
    }

    private void FollowPlayer()
    {
        if (_targetTransform == null)
        {
            _targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (_targetTransform != null)
        {
            _targetPosition = _targetTransform.position;
        }
    }

    private void FollowWayPoints()
    {
        if (_currentWayPoint == null)
        {
            _currentWayPoint = FindClossestWayPoints();
        }

        if (_currentWayPoint != null)
        {
            _targetPosition = _currentWayPoint.transform.position;

            float distanceToWayPoint = (_targetPosition - transform.position).magnitude;

            if (distanceToWayPoint <= _currentWayPoint.MinDistanceToReachWayPoint)
            {
                _currentWayPoint = _currentWayPoint.NextWayPointNode
                    [Random.Range(0, _currentWayPoint.NextWayPointNode.Length)];
            }
        }
    }

    WayPointNode FindClossestWayPoints()
    {
        return _allWayPoints
            .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
    }

    private float TurnTowardTarget()
    {
        Vector2 vectorToTarget = _targetPosition - transform.position;
        vectorToTarget.Normalize();

        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;

        float steerAmount = angleToTarget / 45.0f;

        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);

        return steerAmount;
    }
}
