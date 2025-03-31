using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UILineRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public RectTransform rectTransform; // used component
    public float zDistModifier = 0.05f; // used for z distance modifier
    
    private int _positionCount = 0;
    private Vector3 _rectTransformPosition; // used for checking updates
    private void OnEnable()
    {
        if (GetComponent<LineRenderer>() == null && lineRenderer == null)
        {
            enabled = false;
            Debug.LogError("Line Renderer has not been set. Please set it in the inspector.");
        }
        else if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        
        if (GetComponent<RectTransform>() == null && rectTransform == null)
        {
            enabled = false;
            Debug.LogError("RectTransform has not been set. Please set it in the inspector.");
        }
        else if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }

    private void Start()
    {
        UpdateLineRenderer();
    }

    private void OnValidate()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rectTransform = GetComponent<RectTransform>();
        UpdateLineRenderer();
    }

    private void Update()
    {
        if (_rectTransformPosition != rectTransform.position)
        {
            _rectTransformPosition = rectTransform.position;
            UpdateLineRenderer();
        }
        else if (_positionCount != lineRenderer.positionCount)
        {
            _positionCount = lineRenderer.positionCount;
            UpdateLineRenderer();
        }
    }

    public void UpdateLineRenderer()
    {
        lineRenderer.SetPosition(0, RectTransformToWorldPosition(rectTransform));
    }
    
    public Vector3 RectTransformToWorldPosition(RectTransform rectTransform)
    {
        Camera camera = Camera.main;
        if (camera == null)
            return Vector3.zero;
    
        // Convert screen point to world position
        Vector3 worldPos = rectTransform.TransformPoint(rectTransform.position) + camera.transform.forward * zDistModifier;
    
        return worldPos;
    }
}