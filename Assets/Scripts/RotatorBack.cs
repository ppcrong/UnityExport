using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorBack : MonoBehaviour
{
    public float xEularAngle;
    public float zEularAngle;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(new Vector3(xEularAngle, 0, zEularAngle) * Time.deltaTime);
    }
}
