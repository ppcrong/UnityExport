using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public TextMesh mTextQuaternionX;
    public TextMesh mTextQuaternionY;
    public TextMesh mTextQuaternionZ;
    public TextMesh mTextQuaternionW;
    public TextMesh mTextMovementX;
    public TextMesh mTextMovementY;
    public TextMesh mTextMovementZ;

#if TEST
    // TestOnly
    public float X, Y, Z, Qx, Qy, Qz, Qw;
#endif

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

#if TEST
        Move(X, Y, Z);
        Rotate(Qx, Qy, Qz, Qw);
#endif
    }

    // Update is called once per frame
    void Update()
    {

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
    public void Rotate(float x, float y, float z, float w)
    {
        GetComponent<Transform>().rotation = new Quaternion(x, y, z, w);
        mTextQuaternionX.text = string.Format("Qx: {0:0.0000}", x);
        mTextQuaternionY.text = string.Format("Qy: {0:0.0000}", y);
        mTextQuaternionZ.text = string.Format("Qz: {0:0.0000}", z);
        mTextQuaternionW.text = string.Format("Qw: {0:0.0000}", w);
    }

    // For Android SendMessage
    public void setRotation(string xyzw)
    {
        string[] values = xyzw.Split(',');
        if (values.Length != 4)
        {
            return;
        }
        else
        {
            float x = float.Parse(values[0]);
            float y = float.Parse(values[1]);
            float z = float.Parse(values[2]);
            float w = float.Parse(values[3]);
            Rotate(x, y, z, w);
            mTextQuaternionX.text = string.Format("Qx: {0:0.0000}", x);
            mTextQuaternionY.text = string.Format("Qy: {0:0.0000}", y);
            mTextQuaternionZ.text = string.Format("Qz: {0:0.0000}", z);
            mTextQuaternionW.text = string.Format("Qw: {0:0.0000}", w);
        }
    }
    #endregion

    #region Movement
    public void Move(float x, float y, float z)
    {
        GetComponent<Transform>().position = new Vector3(x, y, z);
        mTextMovementX.text = string.Format("X: {0:0.0000}", x);
        mTextMovementY.text = string.Format("Y: {0:0.0000}", y);
        mTextMovementZ.text = string.Format("Z: {0:0.0000}", z);
    }

    // For Android SendMessage
    public void setMovement(string xyz)
    {
        string[] values = xyz.Split(',');
        if (values.Length != 3)
        {
            return;
        }
        else
        {
            float x = float.Parse(values[0]);
            float y = float.Parse(values[1]);
            float z = float.Parse(values[2]);
            Move(x, y, z);
            mTextMovementX.text = string.Format("X: {0:0.0000}", x);
            mTextMovementY.text = string.Format("Y: {0:0.0000}", y);
            mTextMovementZ.text = string.Format("Z: {0:0.0000}", z);
        }
    }
    #endregion
}
