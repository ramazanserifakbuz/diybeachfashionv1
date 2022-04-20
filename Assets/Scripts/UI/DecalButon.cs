using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecalButon : MonoBehaviour
{
    private int DecalID;
    [SerializeField] private Image DecalImage;



    public void SetValues(int color_id,Sprite decal_sprite)
    {
        DecalID = color_id;
        DecalImage.sprite = decal_sprite;
    }
    public void SelectDecal()
    {/*
        StartCoroutine(PaintController.Instance().PasteDecal(DecalID));
        UIManager.Instance().InfoTM.text = "PAINT IT!";
        ProgressKeeper.Instance().AddStage();
        InputSystem.Instance().InputStoped = false;*/
    }
}
