using System.Collections;
using TMPro;
using UnityEngine;
 
[RequireComponent(typeof(TMP_Text))]
public class FPSCounter : MonoBehaviour
{
    private TextMeshProUGUI _fpsText;
 
    private void Start()
    {
        _fpsText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FramesPerSecond());
    }
 
    private IEnumerator FramesPerSecond()
    {
        while (true)
        {
            int fps = (int) (1f / Time.deltaTime);
            DisplayFPS(fps);
 
            yield return new WaitForSeconds(0.2f);
        }
    }
 
    private void DisplayFPS(float fps)
    {
        _fpsText.text = $"{fps} FPS";
    }
}