using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Vector3 _cameraOffset = new(0f, 5f, -8f);
    [SerializeField] private float _followSmoothTime = 0.15f;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _range = 1000f;
    [SerializeField, Min(0f)] private float _horizontalMoveSpeed = 4f;
    [SerializeField, Min(0f)] private float _holdToMoveDelay = 0.25f;
    [SerializeField] private LayerMask _hitLayers;

    private Vector3 _cameraVelocity;
    private float _touchStartTime;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Awake()
    {
        if (_firePoint == null)
        {
            _firePoint = transform;
        }

        if (_playerCamera == null)
        {
            _playerCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            var touch = Touch.activeTouches[0];
            MoveControl(touch);
            ShootControl(touch);
        }
    }

    private void LateUpdate()
    {
        if (_playerCamera == null) return;

        Vector3 targetPosition = transform.TransformPoint(_cameraOffset);
        _playerCamera.transform.position = Vector3.SmoothDamp(
            _playerCamera.transform.position,
            targetPosition,
            ref _cameraVelocity,
            _followSmoothTime
        );

        _playerCamera.transform.LookAt(transform);
    }

    private void MoveControl(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                _touchStartTime = Time.time;
                break;
            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                if (Time.time - _touchStartTime >= _holdToMoveDelay)
                {
                    float direction = touch.screenPosition.x < Screen.width * 0.5f ? 1f : -1f;
                    transform.position += _horizontalMoveSpeed * direction * Time.deltaTime * Vector3.right;
                }
                break;
        }
    }

    private void ShootControl(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Ended:
                if (Time.time - _touchStartTime < _holdToMoveDelay)
                {
                    Shoot();
                }
                break;
        }
    }

    private void Shoot()
    {
        Debug.DrawRay(_firePoint.position, _firePoint.forward * _range, Color.red, 4f);

        if (Physics.Raycast(_firePoint.position, _firePoint.forward, out RaycastHit hit, _range, _hitLayers))
        {
            if (hit.collider.TryGetComponent<ShootableButton>(out var button))
            {
                button.Press();
            }
            else
            {
                Debug.Log("Hit object: " + hit.collider.name);
            }
        }
    }
}
