using System.Collections;
using UnityEngine;

public class Cup : MonoBehaviour
{
    private Transform _cupModel;

    private const float DEFAULT_CUP_X_ROTATION = 180;


    private void Awake() => _cupModel = transform.GetChild(0);

    public void Jump(Vector3 endPosition, AnimationCurve jumpCurve, float duration) => StartCoroutine(JumpRoutine(endPosition, jumpCurve, duration));

    private IEnumerator JumpRoutine(Vector3 endPosition, AnimationCurve jumpCurve, float duration)
    {
        Vector3 originPosition = transform.position;

        float originXRotation = DEFAULT_CUP_X_ROTATION;
        float endXRotation = originXRotation + 360;

        Bezier bezierCurve = new Bezier(originPosition, endPosition, jumpCurve);
        float t = 0;

        while (t <= 1)
        {
            transform.position = bezierCurve.GetPoint(t);
            float xRotation = Mathf.Lerp(originXRotation, endXRotation, t);
            _cupModel.eulerAngles = new Vector3(xRotation, 0, 0);

            t += Time.deltaTime / duration;
            yield return null;
        }

        transform.position = endPosition;
        _cupModel.eulerAngles = new Vector3(DEFAULT_CUP_X_ROTATION, 0, 0);
    }
}
