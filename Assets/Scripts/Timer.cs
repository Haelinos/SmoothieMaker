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
        LeanTween.scaleX(timerBar, -1, time);
    }

    IEnumerator StopTimer() 
    {
        AnimateBar();
        yield return new WaitForSeconds(time);
        Destroy(timerBar);
    }
}
