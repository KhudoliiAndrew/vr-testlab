using UnityEngine;
using UnityEngine.VFX;
using wands.beam;
using wands.grab;

public class BeamController : MonoBehaviour
{
    private BeamObjectsController _beamObjectsController;

    private bool _isObjectSelected;
    public VisualEffect vfx;
    

    private void Awake()
    {
        _beamObjectsController = gameObject.GetComponent<BeamObjectsController>();
    }

    private void Update()
    {
        vfx.SetFloat("length", _beamObjectsController.targetObjectPointDistance * .5f);

    }
}