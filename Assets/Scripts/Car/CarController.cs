using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class CarController : MonoBehaviour
{
    [Header("Car settings")]
    [SerializeField] private float _driftFactor = 0.93f;
    [SerializeField] private float _accelerationFactor = 5f;
    [SerializeField] private float _turnFactor = 3f;
    [SerializeField] private float _maxSpeed = 7f;
    [SerializeField] private bool _isEndlessMap = false;

    [SerializeField] private GameObject _sfx;

    public float MaxSpeed
    {
        get { return _maxSpeed; }
        set { _maxSpeed = value; }
    }
    public bool IsEndlessMap
    {
        get { return _isEndlessMap; }
        set { _isEndlessMap = value; }
    }

    private float _defaultSpeedBeforeAddSpeed;
    private float _defaultSpeedBeforeSlowSpeed;

    private float _accelerationInput = 1;
    private float _steeringInput = 0;
    private float _rotationAngle = 0;
    private float _velocityVsUp = 0;

    private Rigidbody2D _carRigidbody2D;

    private void Awake()
    {
        _carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.Instance.GetGameState() == GameStates.countDown)
        {
            return;
        }

        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();
    }

    private void ApplyEngineForce()
    {
        // Calculate how much "forward" we are going in terms of the direction of our velocity
        _velocityVsUp = Vector2.Dot(transform.up, _carRigidbody2D.velocity);

        // Limit so we cannot go faster than the max speed in the "forward: direction
        if (_velocityVsUp > _maxSpeed && _accelerationInput > 0)
        {
            return;
        }

        // Limit so we cannot go faster than the 25% of max speed in the "reverse" direction
        if (_velocityVsUp < _maxSpeed * 0.25f && _accelerationInput < 0)
        {
            return;
        }

        // Limit so we cannot go faster in any direction while accelerating
        if (_carRigidbody2D.velocity.sqrMagnitude > _maxSpeed * _maxSpeed && _accelerationInput > 0)
        {
            return;
        }

        // Apply drag if there is no accelarationInput so the car stops when the player lets go of the accelerator
        if (_accelerationInput == 0)
        {
            _carRigidbody2D.drag = Mathf.Lerp(_carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            _carRigidbody2D.drag = 0;
        }

        Vector2 engineForceVector = transform.up * _accelerationInput * _accelerationFactor;

        _carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void ApplySteering()
    {
        // Limit the cars ability to turn when moving slowly
        float minSpeedBeforeAllowTurningFactor = (_carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        _rotationAngle -= _steeringInput * _turnFactor * minSpeedBeforeAllowTurningFactor;

        _carRigidbody2D.MoveRotation(_rotationAngle);
    }

    private void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(_carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(_carRigidbody2D.velocity, transform.right);

        _carRigidbody2D.velocity = forwardVelocity + rightVelocity * _driftFactor;
    }

    public float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, _carRigidbody2D.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBreaking)
    {
        lateralVelocity = GetLateralVelocity();
        isBreaking = false;

        if (_accelerationInput < 0 && _velocityVsUp > 0)
        {
            isBreaking = true;
            return true;
        }

        if (Mathf.Abs(GetLateralVelocity()) > 1.0f)
        {
            return true;
        }

        return false;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        _steeringInput = inputVector.x;
        //_accelerationInput = inputVector.y;
    }

    public void AddSpeed(float speed)
    {
        _maxSpeed += speed;
        _defaultSpeedBeforeAddSpeed = speed;
        StartCoroutine(AddSpeedCoroutine());
    }

    public void SlowSpeed(float speed)
    {
        _maxSpeed -= speed;
        _defaultSpeedBeforeSlowSpeed = speed;
        StartCoroutine(SlowSpeedCoroutine());
    }

    public void OffSFX()
    {
        _sfx.SetActive(false);
    }

    IEnumerator AddSpeedCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _maxSpeed -= _defaultSpeedBeforeAddSpeed;
    }

    IEnumerator SlowSpeedCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _maxSpeed += _defaultSpeedBeforeSlowSpeed;
    }

    public void Reset()
    {
        //chassis.Reset();

        //chassis.MotorTorque = 0;
        //chassis.BreakTorque = 0;
        //chassis.SteerAngle = 0;

        //ThrottleControl = 0;
        //BrakeControl = 0;
        //SteerControl = 0;
    }

    //public void Respawn(Vector3 position, Quaternion rotation)
    //{
    //    Reset();
    //    transform.position = position;
    //    transform.rotation = rotation;
    //}

    internal float GetVelocityMagnitude()
    {
        return _carRigidbody2D.velocity.magnitude;
    }
}
