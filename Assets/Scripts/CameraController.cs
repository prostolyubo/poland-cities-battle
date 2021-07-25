using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    RoundManager manager;

    public Camera cam;
    public Transform target;
    public float lerpFactor;
    public float minZoom;
    public float maxZoom;
    public float zoomMargin;

    Vector3 desiredPosition;

    float GetCameraExtent()
    {
        return cam.orthographicSize * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!manager.isIngame)
            return;

        float extent = GetCameraExtent();
        float rightSide = extent + target.position.x;
        float leftSide = target.position.x - extent;
        CalculateOptimalZoom(ref extent);
        desiredPosition = (manager.first.root.position + manager.second.root.position) / 2 
            + Vector3.up* cam.orthographicSize / 2f;
        desiredPosition = Vector3.Lerp(target.position, desiredPosition, lerpFactor);

        if (rightSide > manager.map.rightBound.position.x)
        {

            desiredPosition.x = target.position.x;
        } else
        {
            if (leftSide < manager.map.leftBound.position.x)
            {
                desiredPosition.x = target.position.x;
            }
        }

        if (desiredPosition.y - cam.orthographicSize < manager.map.lowerBound.position.y)
            desiredPosition.y = manager.map.lowerBound.position.y + cam.orthographicSize;

        target.position = desiredPosition;
    }

    private void CalculateOptimalZoom(ref float extent)
    {
        float desiredZoom = cam.orthographicSize;
        float delta = Mathf.Abs(manager.first.root.position.x - manager.second.root.position.x);
        if (delta < 2 * extent - zoomMargin * 1.1 )
            desiredZoom = minZoom;
        else if (delta > 2 * extent - zoomMargin * 0.9f)
            desiredZoom = maxZoom;
        desiredZoom = Mathf.Lerp(cam.orthographicSize, desiredZoom, lerpFactor/2f);
        if (desiredZoom == cam.orthographicSize)
            return;

        cam.orthographicSize = desiredZoom;
        extent = GetCameraExtent();
    }
}
