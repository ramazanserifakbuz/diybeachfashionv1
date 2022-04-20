using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColorButon : MonoBehaviour
{

    private int ColorID;
    [SerializeField] private Image ColorImage;
    [SerializeField] private Material OutlineMaterial;
    void Start()
    {
        
    }

    
    public void SetValues(int color_id,Sprite color_sprite)
    {
        ColorID = color_id;
        ColorImage.sprite = color_sprite;
        ColorImage.material = null;
    }
    public void SelectColor()
    {

        PaintController.Instance().ChangeBrushColor(ColorID);
        PaintController.Instance().ToolOff(true);
        UIManager.Instance().InfoTM.text = "PAINT ALL!";
        InputSystem.Instance().InputStoped = false;
        UIManager.Instance().ResetOutlines();
        ColorImage.material = OutlineMaterial;
    }

  
}
