using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialMashPointsDistance : MonoBehaviour
{
    [SerializeField] Transform trackedObject;
    [SerializeField] Vector3 tipOffset;
    [SerializeField] float rayLength;

    Vector3 tipPosition;

    private void OnDrawGizmosSelected()
    {
        tipPosition = trackedObject.position + trackedObject.rotation * tipOffset; //Get proper offset if object is rotated
        Gizmos.color = Color.red;
        Gizmos.DrawLine(tipPosition, -(rayLength * trackedObject.up) + tipPosition);
        //Gizmos.DrawLine(trackedObject.position, -(rayLength * trackedObject.up)+trackedObject.position);
    }
}
