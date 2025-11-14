using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialMashPointsDistance : MonoBehaviour
{
    [SerializeField] Transform trackedObject;
    [SerializeField] MeshRenderer trackedObjectMesh;
    [SerializeField] Vector3 tipOffset;
    [SerializeField] float rayLength;

    [SerializeField] Material defaultMaterial;
    [SerializeField] Material triggeredMaterial;

    Vector3 tipPosition;
    Vector3 rayTargetPoint;
    private void Update()
    {
        tipPosition = trackedObject.position + trackedObject.rotation * tipOffset; //Get proper offset if object is rotated
        rayTargetPoint = -(rayLength * trackedObject.up) + tipPosition;
        detectTipCollision();
    }

    private void OnDrawGizmosSelected()
    {
        tipPosition = trackedObject.position + trackedObject.rotation * tipOffset; //Get proper offset if object is rotated
        Gizmos.color = Color.red;
        Gizmos.DrawLine(tipPosition, -(rayLength * trackedObject.up) + tipPosition);
        //Gizmos.DrawLine(trackedObject.position, -(rayLength * trackedObject.up)+trackedObject.position);
    }

    void detectTipCollision()
    {
        if (Physics.Raycast(tipPosition, rayTargetPoint, out RaycastHit hit, rayLength, LayerMask.GetMask("Spatial Awareness")))
        {
            trackedObjectMesh.material = triggeredMaterial;
        }
        else
        {
            trackedObjectMesh.material = defaultMaterial;
        }
    }
}
