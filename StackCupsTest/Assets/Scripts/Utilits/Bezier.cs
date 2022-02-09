using UnityEngine;

public class Bezier
{
    private Vector3 _p0;
    private Vector3 _p1;
    private Vector3 _p2;
    private Vector3 _p3;

    private const float QUARTER = 1f / 4f;

    public Bezier(Vector3 originPosition, Vector3 endPosition, AnimationCurve animationCurve)
    {
        _p0 = originPosition;
        _p3 = endPosition;
        _p1 = Vector3.Lerp(_p0, _p3, QUARTER) + new Vector3(0, animationCurve.Evaluate(QUARTER), 0);
        _p2 = Vector3.Lerp(_p0, _p3, QUARTER * 3) + new Vector3(0, animationCurve.Evaluate(QUARTER * 3), 0);
    }

    public Vector3 GetPoint(float t)
    {
        return Mathf.Pow(1f - t, 3) * _p0 + 3f * Mathf.Pow(1f - t, 2) * t * _p1 + 3f * (1f - t) * Mathf.Pow(t, 2) * _p2 + Mathf.Pow(t, 3) * _p3;
    }

    /*public Vector3 GetRotation(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return 3 * Mathf.Pow(1f - t, 2) * (p1 - p0) + 6 * (1 - t) * t * (p2 - p1) + 3 * Mathf.Pow(t, 2) * (p3 - p2);
    }*/
}
