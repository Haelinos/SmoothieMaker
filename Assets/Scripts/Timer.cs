using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

    IEnumerator StopTimer() 
    {
        AnimateBar();
        yield return new WaitForSeconds(time);

    }
}
