using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _followSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Vector3 _rotationOffset;

    [Inject] private CupsManager _cupsManager;
    private Vector3 _targetPosition => _cupsManager.CupsPosition;

    private void Start()
    {
        transform.position = _targetPosition + _positionOffset;

        Vector3 lookDirection = ((_targetPosition + _rotationOffset) - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = lookRotation;
    }

    private void Update()
    {
        if (transform.position != _targetPosition + _positionOffset)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition + _positionOffset, _followSpeed * Time.deltaTime);
        }

        Vector3 lookDirection = ((_targetPosition + _rotationOffset) - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        if (transform.rotation != lookRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
