using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cup : MonoBehaviour
{
    [SerializeField] private ColorsData _colorsData;

    private Rigidbody _rigidbody;
    private Transform _cupModel;

    private const float DEFAULT_CUP_X_ROTATION = 180;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _cupModel = transform.GetChild(0);
    }

    private void Start()
    {
        _rigidbody.isKinematic = true;
        SetRandomColor();
    }

    private void SetRandomColor()
    {
        int colorsCount = _colorsData.Colors.Length;
        int randomColorIndex = Random.Range(0, colorsCount);
        _cupModel.GetComponent<MeshRenderer>().material.color = _colorsData.Colors[randomColorIndex];
    }

    public void Jump(Vector3 endPosition, AnimationCurve jumpCurve, float duration, bool isAbyss) => StartCoroutine(JumpRoutine(endPosition, jumpCurve, duration, isAbyss));

    private IEnumerator JumpRoutine(Vector3 endPosition, AnimationCurve jumpCurve, float duration, bool isAbyss)
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

        if (isAbyss) _rigidbody.isKinematic = false;
    }
}
