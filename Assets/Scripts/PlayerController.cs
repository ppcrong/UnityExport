using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    // Text on UI
    public Text mTextQx;
    public Text mTextQy;
    public Text mTextQz;
    public Text mTextQw;
    public Text mTextMx;
    public Text mTextMy;
    public Text mTextMz;
    public Text mTextPx;
    public Text mTextPy;
    public Text mTextPz;

    // Interpolant
    public float mRotateInterpolant = 0.8f;
    public float mMoveInterpolant = 0.1F;

    // Quaternion and Movement values
    public float mX, mY, mZ, mQx, mQy, mQz, mQw;

    /*
     * Rotate type
     * 0: Direct
     * 1: Smooth
     */
    public int mRotateType = 0;

    /*
     * Move type
     * 0: Direct
     * 1: Smooth
     */
    public int mMoveType = 1;

    public float mMoveScale = 1F;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Leave app when pressing back/home keys
        if (Application.platform == RuntimePlatform.Android)
        {
            // Back/Home key to exit Unity app
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
            {
                Application.Quit();
            }
        }
    }

    void FixedUpdate()
    {
        // Update helicopter rotation and position
        switch (mRotateType)
        {
            case 0:
            default:
                Rotate(mQx, mQy, mQz, mQw);
                break;
            case 1:
                SmoothRotate(mQx, mQy, mQz, mQw);
                break;
        }
        switch (mMoveType)
        {
            case 0:
            default:
                Move(mX, mY, mZ);
                break;
            case 1:
                SmoothMove(mX, mY, mZ);
                break;
        }
    }

    #region Rotation
    private void Rotate(float x, float y, float z, float w)
    {
        transform.rotation = new Quaternion(x, y, z, w);

        try
        {
            mTextQx.text = string.Format("Qx: {0:0.0000}", x);
            mTextQy.text = string.Format("Qy: {0:0.0000}", y);
            mTextQz.text = string.Format("Qz: {0:0.0000}", z);
            mTextQw.text = string.Format("Qw: {0:0.0000}", w);
        }
        catch (System.Exception ex)
        {
            Debug.Log("Rotate exception!!!");
            Debug.Log(ex.ToString());
        }
    }

    private void SmoothRotate(float x, float y, float z, float w)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(x, y, z, w), mRotateInterpolant);

        try
        {
            mTextQx.text = string.Format("Qx: {0:0.0000}", x);
            mTextQy.text = string.Format("Qy: {0:0.0000}", y);
            mTextQz.text = string.Format("Qz: {0:0.0000}", z);
            mTextQw.text = string.Format("Qw: {0:0.0000}", w);
        }
        catch (System.Exception ex)
        {
            Debug.Log("SmoothRotate exception!!!");
            Debug.Log(ex.ToString());
        }
    }

    // Export to set rotation
    public void setRotation(string xyzw)
    {
        string[] values = xyzw.Split(',');
        if (values.Length != 4)
        {
            return;
        }
        else
        {
            mQx = float.Parse(values[0]);
            mQy = float.Parse(values[1]);
            mQz = float.Parse(values[2]);
            mQw = float.Parse(values[3]);
        }
    }

    // Export to set interpolant quaternion
    public void setRotateInterpolant(string interpolant)
    {
        mRotateInterpolant = float.Parse(interpolant);
    }

    // Export to set rotate type
    public void setRotateType(string type)
    {
        mRotateType = int.Parse(type);
    }
    #endregion

    #region Movement
    private void Move(float x, float y, float z)
    {
        transform.position = new Vector3(x * mMoveScale, y * mMoveScale, z * mMoveScale);

        try
        {
            // Movement
            mTextMx.text = string.Format("X: {0:0.0000}", x);
            mTextMy.text = string.Format("Y: {0:0.0000}", y);
            mTextMz.text = string.Format("Z: {0:0.0000}", z);

            // Current player position
            mTextPx.text = string.Format("Px: {0:0.0000}", transform.position.x);
            mTextPy.text = string.Format("Py: {0:0.0000}", transform.position.y);
            mTextPz.text = string.Format("Pz: {0:0.0000}", transform.position.z);
        }
        catch (System.Exception ex)
        {
            Debug.Log("Move exception!!!");
            Debug.Log(ex.ToString());
        }
    }

    private void SmoothMove(float x, float y, float z)
    {
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(x * mMoveScale, y * mMoveScale, z * mMoveScale), mMoveInterpolant);

        try
        {
            // Movement
            mTextMx.text = string.Format("X: {0:0.0000}", x);
            mTextMy.text = string.Format("Y: {0:0.0000}", y);
            mTextMz.text = string.Format("Z: {0:0.0000}", z);

            // Current player position
            mTextPx.text = string.Format("Px: {0:0.0000}", transform.position.x);
            mTextPy.text = string.Format("Py: {0:0.0000}", transform.position.y);
            mTextPz.text = string.Format("Pz: {0:0.0000}", transform.position.z);
        }
        catch (System.Exception ex)
        {
            Debug.Log("SmoothMove exception!!!");
            Debug.Log(ex.ToString());
        }
    }

    // Export to set movement
    public void setMovement(string xyz)
    {
        string[] values = xyz.Split(',');
        if (values.Length != 3)
        {
            return;
        }
        else
        {
            mX = float.Parse(values[0]);
            mY = float.Parse(values[1]);
            mZ = float.Parse(values[2]);
        }
    }

    // Export to set interpolant movement
    public void setMoveInterpolant(string interpolant)
    {
        mMoveInterpolant = float.Parse(interpolant);
    }

    // Export to set move type
    public void setMoveType(string type)
    {
        mMoveType = int.Parse(type);
    }

    // Export to set move scale
    public void setMoveScale(string scale)
    {
        mMoveScale = float.Parse(scale);
    }
    #endregion
}
