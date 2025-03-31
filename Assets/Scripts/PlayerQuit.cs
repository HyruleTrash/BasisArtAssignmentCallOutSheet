using UnityEngine;

public class PlayerQuit : MonoBehaviour
{
    private void Update()
    {
        // Press ESC to stop game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}