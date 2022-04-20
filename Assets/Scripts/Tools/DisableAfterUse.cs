using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterUse : MonoBehaviour
{
    public float time;
    private void OnEnable()
    {
        Invoke("DisableObject",time);
    }
    void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
