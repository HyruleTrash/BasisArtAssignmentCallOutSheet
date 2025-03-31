using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class RandomMovement : MonoBehaviour
{
    public Vector2 minMaxX;
    public Vector2 minMaxY;
    public float speed;
    
    public Vector3 startPosition;
    public RectTransform rectTransform;
    private Vector3 _targetPosition;
    
    private bool _flipped;
    private bool _allowedToFlip = true;
    public RawImage image;
    
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.TransformPoint(rectTransform.position);
        PickPosition();
    }

    private void Update()
    {
        rectTransform.position = Vector3.MoveTowards(rectTransform.position, _targetPosition, Time.deltaTime * speed);
        if (Vector3.Distance(rectTransform.position, _targetPosition) < 0.1f)
        {
            PickPosition();
        }

        LookAt();
    }

    private void LookAt()
    {
        var targetRotation = Mathf.Atan((_targetPosition.y - rectTransform.anchoredPosition.y) / 
                                          (_targetPosition.x - rectTransform.anchoredPosition.x));
        rectTransform.localRotation = Quaternion.Euler(0, 0, targetRotation * Mathf.Rad2Deg);

        if (!_allowedToFlip)
            return;
        if (_targetPosition.x < rectTransform.position.x && _flipped)
        {
            _flipped = true;
            Flip();
        }
        else if (_targetPosition.x > rectTransform.position.x && !_flipped)
        {
            _flipped = false;
            Flip();
        }
    }

    private void Flip()
    {
        Rect uv = image.uvRect;
        uv.width *= -1;
        image.uvRect = uv;
        _allowedToFlip = false;
        Invoke(nameof(ResetAllowedToFlip), 1f);
    }
    
    public void ResetAllowedToFlip()
    {
        _allowedToFlip = true;
    }

    private void PickPosition()
    {
        _targetPosition = new Vector3(Random.Range(minMaxX.x, minMaxX.y) + startPosition.x, Random.Range(minMaxY.x, minMaxY.y) + startPosition.y, startPosition.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        rectTransform = GetComponent<RectTransform>();
        Vector3 pos = rectTransform.TransformPoint(rectTransform.position);
        Gizmos.DrawLine(new Vector3(minMaxX.x, minMaxY.x, 0) + pos, new Vector3(minMaxX.x, minMaxY.y, 0) + pos);
        Gizmos.DrawLine(new Vector3(minMaxX.x, minMaxY.y, 0) + pos, new Vector3(minMaxX.y, minMaxY.y, 0) + pos);
        Gizmos.DrawLine(new Vector3(minMaxX.y, minMaxY.y, 0) + pos, new Vector3(minMaxX.y, minMaxY.x, 0) + pos);
        Gizmos.DrawLine(new Vector3(minMaxX.y, minMaxY.x, 0) + pos, new Vector3(minMaxX.x, minMaxY.x, 0) + pos);
    }
}
