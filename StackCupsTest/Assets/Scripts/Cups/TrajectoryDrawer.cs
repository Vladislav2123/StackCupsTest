using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryDrawer : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
    }

    public void StartDrawTrajectory() => _lineRenderer.enabled = true;
    public void StopDrawTrajectory() => _lineRenderer.enabled = false;

    public void DrawTrajectory(Vector3 originPosition, float length)
    {
        _lineRenderer.SetPosition(0, originPosition + _offset);
        _lineRenderer.SetPosition(1, originPosition + new Vector3(0, 0, length) + _offset);
    }
}
