using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PaintMove : SceneDependentSingleton<PaintMove>
{
    [Header("Control Limitter")]

    public float cooldown, maxcooldown;
    [SerializeField] private Vector2 MinimumValues, MaximumValues;
    private Vector3 CurrentToolLastPosition, CurrentPosition;
    public GameObject CurrentTool;

    public GameObject guidi;

    int selectNumber = 0;
    void Start()
    {
        CurrentToolLastPosition = CurrentTool.transform.position;
    }
    void PaintToolMove()
    {
        Vector3 CurrentPosition = CurrentTool.transform.position;
        Vector3 NewPosition = new Vector3(CurrentToolLastPosition.x + InputSystem.Instance().MousePosDiffrence.x, CurrentPosition.y, CurrentToolLastPosition.z + InputSystem.Instance().MousePosDiffrence.y);
        //*****CONTROL LIMITTER***//
        //HORIZONTAL MOVE
        if (CurrentPosition.x <= MaximumValues.x && CurrentPosition.x >= MinimumValues.x)
        {
            CurrentTool.transform.DOMoveX(NewPosition.x, 0.1f);
        }
      
        //VERTICAL MOVE
        if (CurrentPosition.z <= MaximumValues.y && CurrentPosition.z >= MinimumValues.y)
        {
            CurrentTool.transform.DOMoveZ(NewPosition.z, 0.1f);
        }





        if (Input.GetKey(KeyCode.Mouse0))
        {
          
            if (InputSystem.Instance().InputUsing)
            {
                ActiveAgain();
            }
          
        }

        if (Input.GetMouseButtonUp(0))
        {
            /*if (!InputSystem.Instance().InputUsing)
            {*/
                Deactive();
/*
            }*/
        }

    }


    public void ActiveAgain()
    {
     
      
        CurrentTool.GetComponentInChildren<PaintIn3D.P3dHitNearby>().enabled = true;
       
        CurrentTool.GetComponentInChildren<ParticleSystem>().Play();

    }
    public void Deactive()
    {
       
            CurrentTool.GetComponentInChildren<PaintIn3D.P3dHitNearby>().enabled = false;
        
        CurrentTool.GetComponentInChildren<ParticleSystem>().Stop();
    }

    void Update()
    {
        PaintToolMove();
    }
    // Update is called once per frame
  
}
