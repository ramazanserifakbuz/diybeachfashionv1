using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private static InputSystem instance;
    public bool InputUsing;
    public bool InputStoped;
    private Vector2 FirstMousePos, EndMousePos;
    public Vector2 MousePosDiffrence;

    void Awake()
    {
        instance = GetComponent<InputSystem>();
    }
    public static InputSystem Instance()
    {
        return instance;
    }

    void Update()
    {
        if (!InputStoped)
        {
           
            CheckInput();
        }
        else
        {
            InputUsing = false;
        }
      
    }

    void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {

            PaintMove.Instance.guidi.SetActive(false);
            FirstMousePos = Input.mousePosition;
            InputUsing = true;
          
        }
        else if (Input.GetMouseButton(0))
        {
            EndMousePos = Input.mousePosition;
            MousePosDiffrence = new Vector2((EndMousePos.x - FirstMousePos.x) / (Screen.width /28f), (EndMousePos.y - FirstMousePos.y) / (Screen.width / 28));
            InputUsing = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            InputUsing = false;
          /*  FirstMousePos = Vector3.zero;
            EndMousePos = Vector3.zero;
            MousePosDiffrence = Vector2.zero;*/
        }
    }
}
