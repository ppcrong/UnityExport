using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    // Text on UI
    public Text mTextCx;
    public Text mTextCy;
    public Text mTextCz;

    // Helicopter
    public GameObject mPlayer;

    // Offset between player and camera
    private Vector3 mOffset;

    // For SmoothDamp
    public float mSmoothTime = 0.05F;
    private Vector3 mVelocity = Vector3.zero;

    // For Lerp
    public float mMoveInterpolant = 0.1F;

    // If camera should follow player
    public bool mIsFollowPlayer = true;

    // Use this for initialization
    void Start()
    {
        mOffset = transform.position - mPlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Current player position
        mTextCx.text = string.Format("Cx: {0:0.0000}", transform.position.x);
        mTextCy.text = string.Format("Cy: {0:0.0000}", transform.position.y);
        mTextCz.text = string.Format("Cz: {0:0.0000}", transform.position.z);

        if (mIsFollowPlayer)
        {
            SmoothMove();
        }
    }

    // SmoothDamp: https://docs.unity3d.com/ScriptReference/Vector3.SmoothDamp.html
    // Lerp: https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
    void SmoothMove()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = mPlayer.transform.position + mOffset;

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref mVelocity, mSmoothTime);

        // Use Lerp
        //transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, moveInterpolant);
    }

    // Export to set if camera follow player
    public void setIsFollowPlayer(string b)
    {
        mIsFollowPlayer = bool.Parse(b);
    }

    // Export to set interpolant movement
    public void setMoveSmoothTime(string time)
    {
        mSmoothTime = float.Parse(time);
    }

    // Export to set interpolant movement
    public void setMoveInterpolant(string interpolant)
    {
        mMoveInterpolant = float.Parse(interpolant);
    }
}
