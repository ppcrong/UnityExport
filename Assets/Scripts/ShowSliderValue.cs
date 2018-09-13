using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSliderValue : MonoBehaviour
{
    // The GameObject to set TrailRenderer
    public GameObject mPlayer;

    // The TrailRenderer to set time
    TrailRenderer mTrailRenderer;

    // The text to show value
    Text mTextValue;

    // Use this for initialization
    void Start()
    {
        mTextValue = GetComponent<Text>();
        mTrailRenderer = mPlayer.GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void valueUpdate(float value)
    {
        mTextValue.text = string.Format("{0:0}", value);
        mTrailRenderer.time = value;
    }
}
