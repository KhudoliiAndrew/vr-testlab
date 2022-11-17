using UnityEngine;
using wands.grab;

public class ArcController : MonoBehaviour
{
    [Header("Beam Parameters")] public Transform startBeam;
    public Transform beam2;
    public Transform beam3;
    public Transform endBeam;
    public Transform pointerBeam;

    public Transform startTarget;

    private GrabbableController _grabbableController;

    private bool _isObjectSelected;

    private void Awake()
    {
        _grabbableController = gameObject.GetComponent<GrabbableController>();
    }

    private void Update()
    {
        startBeam.position = startTarget.position;

        var rayOutput = _grabbableController.RayOutput;
        _isObjectSelected = _grabbableController.selectedObject != null;

        UpdateHoverBeam(Vector3.Distance(rayOutput.point, transform.position));

        if (_isObjectSelected)
        {
            UpdateHoverBeam(.0f);
            SetEndPosition(_grabbableController.selectedObject.transform.position);
            gameObject.GetComponent<GrabWand>().beam.SetActive(true);
        }
        else
        {
            gameObject.GetComponent<GrabWand>().beam.SetActive(false);
        }
    }

    private void SetEndPosition(Vector3 position)
    {
        endBeam.position = position;

        var wandPosition = transform.position;
        var targetPointerPosition = _grabbableController.targetPointer.transform.position;
        beam2.position = _CalculateQuadraticBezierPoint(.3f, wandPosition, targetPointerPosition, position);
        beam3.position = _CalculateQuadraticBezierPoint(.6f, wandPosition, targetPointerPosition, position);
    }

    private void UpdateHoverBeam(float distance)
    {
        pointerBeam.localScale = new Vector3(pointerBeam.localScale.x, distance, pointerBeam.localScale.z);
        pointerBeam.transform.localPosition = new Vector3(0, distance, 0);
    }

    private Vector3 _CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }
}