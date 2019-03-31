using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeometryHelper
{
    public static void FindTouchPoints(Vector3 tangentStartPoint, Vector3 circleCenter, float circleRadius,
        out Vector3 firstTouchPoint, out Vector3 secondTouchPoint, out float distance)
    {
        // build perpendicular to line, connecting the center of explosion and the center of mob,
        // then find points where this line crosses the mob's collider
        Vector3 explosionPositionProjectionToMobPlane = new Vector3(tangentStartPoint.x, circleCenter.y, tangentStartPoint.z);
        Vector3 connectionVector = circleCenter - explosionPositionProjectionToMobPlane;
        Vector3 connectionVectorProjection = new Vector3(connectionVector.x, 0, connectionVector.z);

        float connectionVectorProjectionLength = connectionVectorProjection.magnitude;
        // cosine of angle between radius to touch point and connection vector, by geometry
        float radiusAndConnectionVectorCosine = circleRadius / connectionVectorProjection.magnitude;
        float radiusAndConnectionVectorAngle = Mathf.Acos(radiusAndConnectionVectorCosine) * Mathf.Rad2Deg;
        // normalized vector from mob center to explosion
        Vector3 connectionVectorProjectionOppositeNormalized = -connectionVectorProjection.normalized;

        // rotate around mob center to get right directions
        Vector3 firstTouchPointRadiusVector = Quaternion.AngleAxis(radiusAndConnectionVectorAngle, Vector3.up) * connectionVectorProjectionOppositeNormalized;
        Vector3 secondTouchPointRadiusVector = Quaternion.AngleAxis(-radiusAndConnectionVectorAngle, Vector3.up) * connectionVectorProjectionOppositeNormalized;
        // set right length to radius vectors
        firstTouchPointRadiusVector = firstTouchPointRadiusVector * circleRadius;
        secondTouchPointRadiusVector = secondTouchPointRadiusVector * circleRadius;

        // finally find touch points
        firstTouchPoint = circleCenter + firstTouchPointRadiusVector;
        secondTouchPoint = circleCenter + secondTouchPointRadiusVector;

        distance = connectionVectorProjectionLength;
    }
}
