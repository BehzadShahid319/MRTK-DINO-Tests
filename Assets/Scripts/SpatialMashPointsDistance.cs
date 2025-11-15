using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialMashPointsDistance : MonoBehaviour
{
    [SerializeField] LineRenderer lineRendererForDisplay;
    [SerializeField] Transform trackedObject;
    [SerializeField] MeshRenderer trackedObjectMesh;
    [SerializeField] Vector3 tipOffset;
    [SerializeField] float rayLength;

    [SerializeField] Material defaultMaterial;
    [SerializeField] Material triggeredMaterial;

    Vector3 tipPosition;
    Vector3 rayTargetPoint;
    Vector3 direction;

    private void Start()
    {
        lineRendererForDisplay.useWorldSpace = true;
    }

    private void Update()
    {
        direction = -trackedObject.up;
        tipPosition = trackedObject.position + trackedObject.rotation * tipOffset; //Get proper offset if object is rotated
        //rayTargetPoint = -(rayLength * trackedObject.up) + tipPosition;
        rayTargetPoint = tipPosition + direction * rayLength;

        // Ray direction: stylus "down" (or forward, depending on your model)
        Vector3 rayEnd = tipPosition - trackedObject.up * rayLength;

        lineRendererForDisplay .SetPosition(0, tipPosition);
        lineRendererForDisplay.SetPosition(1, rayTargetPoint);

        detectTipCollision(direction);
    }

    private void OnDrawGizmosSelected()
    {
        tipPosition = trackedObject.position + trackedObject.rotation * tipOffset; //Get proper offset if object is rotated
        Gizmos.color = Color.red;
        Gizmos.DrawLine(tipPosition, tipPosition - trackedObject.up * rayLength);
        //Gizmos.DrawLine(trackedObject.position, -(rayLength * trackedObject.up)+trackedObject.position);
    }

    void detectTipCollision(Vector3 dir)
    {
        if (Physics.Raycast(tipPosition, dir, out RaycastHit hit, rayLength, LayerMask.GetMask("Spatial Awareness")))
        {
            trackedObjectMesh.material = triggeredMaterial;
            Debug.Log("<color=yellow> Triggerred with Spatial Awareness Mesh</color>");
        }
        else
        {
            trackedObjectMesh.material = defaultMaterial;
            Debug.Log("<color=red> Triggerred Exit From SPatial Awareness Mesh</color>");
        }
    }
}
