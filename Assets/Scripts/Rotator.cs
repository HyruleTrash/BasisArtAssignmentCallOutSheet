using System;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed = 15f;
    public RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectTransform.rotation = Quaternion.Euler(0, 0, rectTransform.localRotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));
    }
}
