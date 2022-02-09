using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CupsManager : MonoBehaviour
{
    [SerializeField] private Cup _initialCup;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _cupsDistance;
    [SerializeField] private float _jumpDelay;
    [SerializeField] private float _jumpRange;
    [SerializeField] private float _rayLength;
    [SerializeField] private LayerMask _jumpLayers;

    private Stack<Cup> _collectedCups;

    [Inject] private PlayerInput _playerInput;

    private bool _isCanJump;

    public Vector3 CupsPosition { get; private set; }

    private void Awake()
    {
        _playerInput.OnClick += Jump;
        _collectedCups = new Stack<Cup>(10);
        _collectedCups.Push(_initialCup);
        CupsPosition = _collectedCups.Peek().transform.position;

        _isCanJump = true;
    }

    private void Jump()
    {
        if (_isCanJump) StartCoroutine(JumpRoutine());
    }

    private IEnumerator JumpRoutine()
    {
        _isCanJump = false;

        Cup newCup = null;
        Vector3 originPosition = _collectedCups.Peek().transform.position;
        RaycastHit raycastHit = SendRay(originPosition);

        Vector3 endPosition = CalculateEndPositionAndTryGetCup(originPosition, out newCup);

        List<Cup> jumpedCups = new List<Cup>(_collectedCups.Count);
        Stack<Cup> newCupsStack = new Stack<Cup>(10);

        if (newCup != null) newCupsStack.Push(newCup);

        int cupsCount = _collectedCups.Count;

        for (int i = 0; i < cupsCount; i++)
        {
            Cup cup = _collectedCups.Pop();

            if (i == 0) cup.Jump(endPosition, _jumpCurve, _jumpDuration);
            else cup.Jump(endPosition + new Vector3(0, _cupsDistance * jumpedCups.Count, 0), _jumpCurve, _jumpDuration);

            jumpedCups.Add(cup);
            newCupsStack.Push(cup);

            yield return new WaitForSeconds(_jumpDelay);
        }

        yield return new WaitForSeconds(_jumpDuration - _jumpDelay);


        _collectedCups = newCupsStack;
        _isCanJump = true;
    }

    private Vector3 CalculateEndPositionAndTryGetCup(Vector3 originPosition, out Cup newCup)
    {
        Vector3 originRayPosition = originPosition + new Vector3(0, _rayLength / 2, _jumpRange);
        Ray ray = new Ray(originRayPosition, Vector3.down);
        RaycastHit hitInfo;

        Debug.DrawRay(originRayPosition, Vector3.down * _rayLength, Color.white, 5);

        if (Physics.Raycast(ray, out hitInfo, _rayLength, _jumpLayers))
        {
            if (hitInfo.collider.TryGetComponent<Cup>(out Cup cup))
            {
                newCup = cup;
                CupsPosition = hitInfo.transform.position;
                return cup.transform.position + new Vector3(0, _cupsDistance, 0);
            }
            else if (hitInfo.collider.TryGetComponent<Peak>(out Peak peak))
            {
                newCup = null;
                CupsPosition = hitInfo.transform.position;
                return peak.transform.position;
            }
            else
            {
                newCup = null;
                CupsPosition = hitInfo.point;
                return hitInfo.point;
            }
        }

        newCup = null;
        return Vector3.zero;
    }

    private RaycastHit SendRay(Vector3 originPosition)
    {
        Vector3 originRayPosition = originPosition + new Vector3(0, _rayLength / 2, _jumpRange);
        Ray ray = new Ray(originRayPosition, Vector3.down);
        RaycastHit hitInfo;

        Debug.DrawRay(originRayPosition, Vector3.down * _rayLength, Color.white, 5);

        if (Physics.Raycast(ray, out hitInfo, _rayLength, _jumpLayers)) return hitInfo;

        return hitInfo; // НЕ НРАВИТСЯ
    }

    //private void GetHitInfo(out Cup cup, out bool isPeak)
}
