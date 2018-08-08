using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Text on UI
    public TextMesh mTextQuaternionX;
    public TextMesh mTextQuaternionY;
    public TextMesh mTextQuaternionZ;
    public TextMesh mTextQuaternionW;
    public TextMesh mTextMovementX;
    public TextMesh mTextMovementY;
    public TextMesh mTextMovementZ;

    // Interpolant
    public float mInterpolantQuaternion;
    public float mInterpolantMovement;

    // Quaternion and Movement values
    public float mX, mY, mZ, mQx, mQy, mQz, mQw;

    // Use this for initialization
    void Start()
    {
        mTextMovementX = GameObject.Find("MovementX").GetComponent<TextMesh>();
        mTextMovementY = GameObject.Find("MovementY").GetComponent<TextMesh>();
        mTextMovementZ = GameObject.Find("MovementZ").GetComponent<TextMesh>();
        mTextQuaternionX = GameObject.Find("QuaternionX").GetComponent<TextMesh>();
        mTextQuaternionY = GameObject.Find("QuaternionY").GetComponent<TextMesh>();
        mTextQuaternionZ = GameObject.Find("QuaternionZ").GetComponent<TextMesh>();
        mTextQuaternionW = GameObject.Find("QuaternionW").GetComponent<TextMesh>();
    }

    bool b = true;
    // Update is called once per frame
    void Update()
    {
        // Update helicopter rotation and position
        SmoothMove(mX, mY, mZ);
        SmoothRotate(mQx, mQy, mQz, mQw);

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

    private void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
    }

    #region Rotation
    private void Rotate(float x, float y, float z, float w)
    {
        GetComponent<Transform>().rotation = new Quaternion(x, y, z, w);
        mTextQuaternionX.text = string.Format("Qx: {0:0.0000}", x);
        mTextQuaternionY.text = string.Format("Qy: {0:0.0000}", y);
        mTextQuaternionZ.text = string.Format("Qz: {0:0.0000}", z);
        mTextQuaternionW.text = string.Format("Qw: {0:0.0000}", w);
    }

    private void SmoothRotate(float x, float y, float z, float w)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(x, y, z, w), mInterpolantQuaternion);

        mTextQuaternionX.text = string.Format("Qx: {0:0.0000}", x);
        mTextQuaternionY.text = string.Format("Qy: {0:0.0000}", y);
        mTextQuaternionZ.text = string.Format("Qz: {0:0.0000}", z);
        mTextQuaternionW.text = string.Format("Qw: {0:0.0000}", w);
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
            //Rotate(x, y, z, w);
            //mTextQuaternionX.text = string.Format("Qx: {0:0.0000}", x);
            //mTextQuaternionY.text = string.Format("Qy: {0:0.0000}", y);
            //mTextQuaternionZ.text = string.Format("Qz: {0:0.0000}", z);
            //mTextQuaternionW.text = string.Format("Qw: {0:0.0000}", w);
        }
    }

    // Export to set interpolant quaternion
    public void setQuaternionT(string t)
    {
        mInterpolantQuaternion = float.Parse(t);
    }
    #endregion

    #region Movement
    private void Move(float x, float y, float z)
    {
        GetComponent<Transform>().position = new Vector3(x, y, z);
        mTextMovementX.text = string.Format("X: {0:0.0000}", x);
        mTextMovementY.text = string.Format("Y: {0:0.0000}", y);
        mTextMovementZ.text = string.Format("Z: {0:0.0000}", z);
    }
private void SmoothMove(float x, float y, float z)
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, z), mInterpolantMovement);

        mTextMovementX.text = string.Format("X: {0:0.0000}", x);
        mTextMovementY.text = string.Format("Y: {0:0.0000}", y);
        mTextMovementZ.text = string.Format("Z: {0:0.0000}", z);
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
            //Move(x, y, z);
            //mTextMovementX.text = string.Format("X: {0:0.0000}", x);
            //mTextMovementY.text = string.Format("Y: {0:0.0000}", y);
            //mTextMovementZ.text = string.Format("Z: {0:0.0000}", z);
        }
    }

    // Export to set interpolant movement
    public void setMovementT(string t)
    {
        mInterpolantMovement = float.Parse(t);
    }
    #endregion
}
