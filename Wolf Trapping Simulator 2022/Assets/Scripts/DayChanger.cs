using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayChanger : MonoBehaviour
{
    private float totalTime;
    private float time;
    private Light light;
    public float dayTransitionSeconds;
    public float daytime;
    public float nightTime;
    bool day = true, night = false, transition = false;

    // Start is called before the first frame update
    void Start()
    {
        light = gameObject.GetComponent<Light>();
        if (dayTransitionSeconds == 0)
            dayTransitionSeconds = 60;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        totalTime += Time.deltaTime;
        Debug.Log("Time: " + time);
        Debug.Log("Total Time:" + time);
    }
    private void LateUpdate()
    {
        Debug.Log("Before: " + day + " " + transition + " " + night);
        if (day && !transition)
        {
            Debug.Log("Day");
            night = time > daytime ? true : false;
            transition = time > daytime ? true : false;
            day = time < daytime ? true : false;
            if (transition)
                time = 0;
        }

        if (night && !transition)
        {
            Debug.Log("Night");
            day = time > nightTime ? true : false;
            transition = time > nightTime ? true : false;
            night = time < nightTime ? true : false;
            if (transition)
                time = 0;
        }

        if (transition && day)
        {
            Debug.Log("Day transition");
            
            if (time == 0)
                time = 0.01f;
            light.intensity = 0 + (time / dayTransitionSeconds) > 0.95f ? 0.95f : (0 + (time / dayTransitionSeconds) );
            if (0 + (time / dayTransitionSeconds) > 0.95f)
            {
                time = 0;
                transition = false;
                night = false;
                day = true;
            }
        }
        if (transition && night)
        {
            Debug.Log("Night transition");
            light.intensity = 1 - (time / dayTransitionSeconds) < 0.05f ? 0.05f : (1 - (time / dayTransitionSeconds));
            if (1 - (time / dayTransitionSeconds) < 0.05f)
            {
                time = 0;
                night = true;
                day = false;
                transition = false;
            }

        }
        Debug.Log("After: " + day + " " + transition + " " + night);
    }
}
