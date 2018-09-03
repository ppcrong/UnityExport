using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    // Rotate Text
    public Text mTextQx;
    public Text mTextQy;
    public Text mTextQz;
    public Text mTextQw;
    public Text mTextEx;
    public Text mTextEy;
    public Text mTextEz;

    // Move Text
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
    public float mX, mY, mZ, mQx, mQy, mQz, mQw, mEx, mEy, mEz;

    /*
     * Rotate type
     * 0: Direct Quaternion
     * 1: Smooth Quaternion
     * 2: Direct Eular
     * 3: Smooth Eular
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
        Rotate(mRotateType, mQx, mQy, mQz, mQw, mEx, mEy, mEz);
        Move(mMoveType, mX, mY, mZ);
    }

    #region Rotation
    private void Rotate(int type, float qx, float qy, float qz, float qw,
        float ex, float ey, float ez)
    {
        switch (type)
        {
            case 0:
            default:
                transform.rotation = new Quaternion(qx, qy, qz, qw);
                PrintRotateLog(type, "RotateQuaternion", qx, qy, qz, qw);
                break;
            case 1:
                transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(qx, qy, qz, qw), mRotateInterpolant);
                PrintRotateLog(type, "SmoothRotateQuaternion", qx, qy, qz, qw);
                break;
            case 2:
                transform.eulerAngles = new Vector3(ex, ey, ez);
                PrintRotateLog(type, "RotateEular", ex, ey, ez);
                break;
            case 3:
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(ex, ey, ez), mRotateInterpolant);
                PrintRotateLog(type, "SmoothRotateEular", ex, ey, ez);
                break;
        }
    }

    private void PrintRotateLog(int type, string prefix, float x, float y, float z, float w = 0)
    {
        try
        {
            switch (type)
            {
                case 0:
                case 1:
                default:
                    mTextQx.text = string.Format("Qx: {0:0.0000}", x);
                    mTextQy.text = string.Format("Qy: {0:0.0000}", y);
                    mTextQz.text = string.Format("Qz: {0:0.0000}", z);
                    mTextQw.text = string.Format("Qw: {0:0.0000}", w);
                    break;
                case 2:
                case 3:
                    mTextEx.text = string.Format("Ex: {0:0.0000}", x);
                    mTextEy.text = string.Format("Ey: {0:0.0000}", y);
                    mTextEz.text = string.Format("Ez: {0:0.0000}", z);
                    break;
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(prefix + " exception!!!");
            Debug.Log(ex.ToString());
        }
    }

    // Export to set rotation
    public void setRotation(string QxyzwExyz)
    {
        string[] values = QxyzwExyz.Split(',');
        if (values.Length != 7)
        {
            Debug.Log("QxyzwExyz size is less than 7");
            return;
        }
        else
        {
            mQx = float.Parse(values[0]);
            mQy = float.Parse(values[1]);
            mQz = float.Parse(values[2]);
            mQw = float.Parse(values[3]);
            mEx = float.Parse(values[4]);
            mEy = float.Parse(values[5]);
            mEz = float.Parse(values[6]);
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
    private void Move(int type, float x, float y, float z)
    {
        string prefix = "";
        switch (type)
        {
            case 0:
            default:
                transform.position = new Vector3(x * mMoveScale, y * mMoveScale, z * mMoveScale);
                prefix = "Move";
                break;
            case 1:
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(x * mMoveScale, y * mMoveScale, z * mMoveScale), mMoveInterpolant);
                prefix = "SmoothMove";
                break;
        }

        PrintMoveLog(prefix, x, y, z);
    }

    private void PrintMoveLog(string prefix, float x, float y, float z)
    {
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
            Debug.Log(prefix + "exception!!!");
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
