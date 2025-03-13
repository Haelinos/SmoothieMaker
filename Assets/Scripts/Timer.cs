using UnityEngine;
using System.Collections;
using DentedPixel;

public class Timer : MonoBehaviour
{
    public GameObject timerBar;
    public int time;

    private void Start()
    {
        StartCoroutine(StopTimer());
    }

    public void AnimateBar()
    {
        // Ensure the scale starts at 1
        timerBar.transform.localScale = new Vector3(1, 1, 1);

        // Animate scaleX from 1 to 0 over 'time' seconds
        LeanTween.scaleX(timerBar, 0, time);
    }

    public void ResetTimer()
    {
        // Reset the scale
        timerBar.transform.localScale = new Vector3(1, 1, 1);

        // Restart the coroutine
        StartCoroutine(StopTimer());
    }

    IEnumerator StopTimer()
    {
        AnimateBar();
        yield return new WaitForSeconds(time);

        // Reset the bar after 20 seconds
        ResetTimer();
    }
}
