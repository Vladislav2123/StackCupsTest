using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TrajectoryDrawer))]
public class CupsManager : MonoBehaviour
{
    [SerializeField] private Cup _initialCup;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _cupsDistance;
    [SerializeField] private float _jumpDelay;
    [SerializeField] private float _minJumpRange;
    [SerializeField] private float _maxJumpRange;
    [SerializeField] private float _rayLength;
    [SerializeField] private LayerMask _targetLayers;

    private Stack<Cup> _collectedCups;

    [Inject] private GameStateController _gameStateController;
    [Inject] private PlayerInput _playerInput;
    private TrajectoryDrawer _trajectoryDrawer;
    private float _jumpRange;

    private bool _isCanJump;
    private bool _isJumpRangeSwaying;

    public Vector3 CupsPosition { get; private set; }

    private void Awake()
    {
        _trajectoryDrawer = GetComponent<TrajectoryDrawer>();

        _playerInput.OnStartTap += TryStartSwayJumpRange;
        _playerInput.OnEndTap += TryStopSwayJumpRangeAndJump;

        _collectedCups = new Stack<Cup>(10);
        _collectedCups.Push(_initialCup);
        CupsPosition = _collectedCups.Peek().transform.position;

        _isCanJump = true;
    }

    private void TryStartSwayJumpRange()
    {
        if (_gameStateController.IsPlaying) StartCoroutine(JumpRangeSwayingRoutine());
    }

    private void TryStopSwayJumpRangeAndJump()
    {
        if (_gameStateController.IsPlaying == false || _isJumpRangeSwaying == false) return;

        _isJumpRangeSwaying = false;
        if (_isCanJump) StartCoroutine(JumpRoutine());
    }

    private IEnumerator JumpRangeSwayingRoutine()
    {
        _jumpRange = _minJumpRange;
        _trajectoryDrawer.StartDrawTrajectory();
        _isJumpRangeSwaying = true;

        while (_isJumpRangeSwaying)
        {
            float sin = Mathf.Sin(Time.time * Mathf.PI); // колебание числа от -1 до 1
            _jumpRange = Mathf.Lerp(_minJumpRange, _maxJumpRange, Mathf.Abs(sin));
            _trajectoryDrawer.DrawTrajectory(CupsPosition, _jumpRange);
            yield return null;
        }

        _trajectoryDrawer.StopDrawTrajectory();
    }

    private IEnumerator JumpRoutine()
    {
        _isCanJump = false;
        bool isPeak = false;
        bool isAbyss = false;
        bool isFinish = false;

        List<Cup> jumpedCups = new List<Cup>(_collectedCups.Count);
        Stack<Cup> newCupsStack = new Stack<Cup>(10);

        Vector3 originPosition = _collectedCups.Peek().transform.position;
        Vector3 endPosition = Vector3.zero;

        RaycastHit hitInfo = SendRay(originPosition);
        if (hitInfo.collider.TryGetComponent<Cup>(out Cup raycastingCup))
        {
            newCupsStack.Push(raycastingCup);
            CupsPosition = raycastingCup.transform.position;
            endPosition = raycastingCup.transform.position + new Vector3(0, _cupsDistance, 0);
        }
        else if (hitInfo.collider.TryGetComponent<Peak>(out Peak raycastingPeak))
        {
            isPeak = true;
            CupsPosition = raycastingPeak.transform.position;
            endPosition = raycastingPeak.transform.position;
        }
        else if (hitInfo.collider.TryGetComponent<Abyss>(out Abyss raycastingAbyss))
        {
            isAbyss = true;
            endPosition = hitInfo.point;
        }
        else if (hitInfo.collider.TryGetComponent<Finish>(out Finish raycastingFinish))
        {
            isFinish = true;
            CupsPosition = raycastingFinish.transform.position;
            endPosition = raycastingFinish.transform.position;
        }
        else
        {
            CupsPosition = hitInfo.point;
            endPosition = hitInfo.point;
        }

        int cupsCount = _collectedCups.Count;
        for (int i = 0; i < cupsCount; i++)
        {
            Cup cup = _collectedCups.Pop();

            if (i == 0) cup.Jump(endPosition, _jumpCurve, _jumpDuration, isAbyss);
            else cup.Jump(endPosition + new Vector3(0, _cupsDistance * jumpedCups.Count, 0), _jumpCurve, _jumpDuration, isAbyss);

            jumpedCups.Add(cup);
            if (i == 0)
            {
                if (isPeak == false) newCupsStack.Push(cup);
            }
            else newCupsStack.Push(cup);

            yield return new WaitForSeconds(_jumpDelay);
        }

        yield return new WaitForSeconds(_jumpDuration - _jumpDelay);

        if (isAbyss || newCupsStack.Count == 0)
        {
            _gameStateController.SetAfterGameState();
            yield break;
        }

        if (isFinish)
        {
            _gameStateController.SetFinished();
            yield break;
        }

        _collectedCups = newCupsStack;
        _isCanJump = true;
    }

    private RaycastHit SendRay(Vector3 originPosition)
    {
        Vector3 originRayPosition = originPosition + new Vector3(0, _rayLength / 2, _jumpRange);
        Ray ray = new Ray(originRayPosition, Vector3.down);
        RaycastHit hitInfo;

        Debug.DrawRay(originRayPosition, Vector3.down * _rayLength, Color.white, 5);

        if (Physics.Raycast(ray, out hitInfo, _rayLength, _targetLayers)) return hitInfo;

        return hitInfo; // НЕ НРАВИТСЯ
    }
}
