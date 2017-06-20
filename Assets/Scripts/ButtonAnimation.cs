using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour {

    public bool lockX;
    public bool lockY;
    public bool lockZ;

    public float returnSpeed;
    public float activationDistance;

    public Color inactiveColor;
    public Color activeColor;

    // This is the second part of the button which remains in static position but changes color according to press
    public GameObject IndicatorObject;

    public bool pressed = false;
    protected bool released = false;
    protected bool pressedWhenActive = false;
    protected Vector3 startPosition;

    public Vector3 originPosition;
    public Vector3 lowestPosition;

    private ButtonScript buttonScript;

    void Start()
    {
        // Remember start position of button
        startPosition = transform.localPosition;
        lowestPosition = startPosition;
        lowestPosition.y = 0.45f;
        buttonScript = GetComponentInParent<ButtonScript>();
    }

    void Update()
    {

        

        released = false;

        // Use local position instead of global, so button can be rotated in any direction
        Vector3 localPos = transform.localPosition;
        if (lockX) localPos.x = startPosition.x;
        if (lockY) localPos.y = startPosition.y;
        if (lockZ) localPos.z = startPosition.z;
        transform.localPosition = localPos;

        if (localPos.y >= 0.5f)
        {
            transform.localPosition = startPosition;
        }
        if (localPos.y <= 0.45f)
        {
            transform.localPosition = lowestPosition;
        }
        // Return button to startPosition
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * returnSpeed);

        //Get distance of button press. Make sure to only have one moving axis.
        Vector3 allDistances = transform.localPosition - startPosition;
        float distance = 0f;
        if (!lockX) distance = Math.Abs(allDistances.x);
        if (!lockY) distance = Math.Abs(allDistances.y);
        if (!lockZ) distance = Math.Abs(allDistances.z);
        float pressComplete = Mathf.Clamp(1 / activationDistance * distance, 0f, 1f);

        //Activate pressed button
        if (pressComplete >= 0.7f && !pressed)
        {
            pressed = true;
            GetComponent<AudioSource>().Play();
            Debug.Log("Pressed");
            //Change color of object to activation color
            if (buttonScript.active) {
                StartCoroutine(ChangeColor(gameObject, Color.red, activeColor, 0.1f));
                pressedWhenActive = true;
            }
                
        }
        //Dectivate unpressed button
        else if (pressComplete <= 0.2f && pressed)
        {
            pressed = false;
            released = true;
            //Change color of object back to normal
            if (pressedWhenActive)
            {
                StartCoroutine(ChangeColor(gameObject, activeColor, inactiveColor, 0.2f));
                
                pressedWhenActive = false;
            }
        }

        //Gradually color the indicator when button is pressed
        //if (IndicatorObject) IndicatorObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.black, activeColor, pressComplete);


        
    }


    public IEnumerator ChangeColor(GameObject obj, Color from, Color to, float duration)
    {
        float timeElapsed = 0.0f;
        float t = 0.0f;

        while (t < 1.0f)
        {
            timeElapsed += Time.deltaTime;
            t = timeElapsed / duration;
            obj.GetComponent<Renderer>().material.color = Color.Lerp(from, to, t);
            yield return null;
        }

    }

}