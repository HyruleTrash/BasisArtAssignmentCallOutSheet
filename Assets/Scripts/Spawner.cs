using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Vector2 minMaxX;
    public Vector2 minMaxY;
    public int spawnAmount;
    public GameObject prefab;
    
    public Vector3 startPosition;
    public RectTransform rectTransform;
    
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.TransformPoint(rectTransform.position);
        SpawnObjects();
    }
    
    private void SpawnObjects()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject obj = Instantiate(prefab, GetRandomPosition(), Quaternion.identity);
            obj.transform.SetParent(transform.parent);
            obj.transform.localScale = Vector3.one;
            var randomMovement = obj.GetComponent<RandomMovement>();
            if (randomMovement != null)
            {
                randomMovement.startPosition = startPosition;
            }
        }
    }
    
    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(minMaxX.x, minMaxX.y) + startPosition.x, Random.Range(minMaxY.x, minMaxY.y) + startPosition.y, startPosition.z);
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
