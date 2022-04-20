using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StickerButon : MonoBehaviour
{
    private int StickerID;
    [SerializeField] private Image StickerImage;
  


    public void SetValues(int color_id, Sprite sprite)
    {
        StickerID = color_id;
        StickerImage.sprite = sprite;
    }
    public void SelectSticker()
    {
        PaintController.Instance().ChangeSticker(StickerID);
        UIManager.Instance().InfoTM.text = "MOVE YOUR FINGER!";
        InputSystem.Instance().InputStoped = false;
        UIManager.Instance().StickerSelectionPanel.SetActive(false);
    }
}
