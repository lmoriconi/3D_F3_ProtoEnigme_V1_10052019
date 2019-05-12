using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private const string MALUS_TEXT = "-30 s.";

    private float timeLeft = 600.0f;        //10 min
    [SerializeField] Text timer;
    [SerializeField] Text malus;

    void Start ()
    {
        malus.text = MALUS_TEXT;
        malus.CrossFadeAlpha(0f, 0f, false);
    }
	
	void Update ()
    {
        timeLeft -= Time.deltaTime;
        int minutes = (int)timeLeft / 60;
        int seconds = (int)timeLeft % 60;
        float fraction = timeLeft * 100;
        fraction = (int)(fraction % 100);

        timer.text = minutes + ":" + seconds + ":" + fraction;
    }

    public void SetTime(float timeToAdd)
    {
        timeLeft += timeToAdd;
        malus.CrossFadeAlpha(1f, 0f, false);
        malus.CrossFadeAlpha(0f, 2f, false);
    }
}
