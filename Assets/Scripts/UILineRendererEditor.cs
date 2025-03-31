#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UILineRenderer))]
public class UILineRendererEditor : Editor
{
    private UILineRenderer _targetScript;

    private void OnEnable()
    {
        _targetScript = (UILineRenderer)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Update start position"))
        {
            _targetScript.UpdateLineRenderer();
        }
    }
}
#endif