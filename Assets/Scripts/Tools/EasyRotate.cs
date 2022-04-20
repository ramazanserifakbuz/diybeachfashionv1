using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyRotate : MonoBehaviour
{
    public bool WorldSpace;
    public Vector3 Axis;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WorldSpace)
        {
            transform.Rotate(Axis * Time.deltaTime,Space.World);
        }
        else
        {
            transform.Rotate(Axis * Time.deltaTime,Space.Self);
        }
       
    }
}
