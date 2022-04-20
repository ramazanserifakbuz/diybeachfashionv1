using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/PaintSettings")]
[System.Serializable]
public class PaintSettings : ScriptableObject
{

    public List<BrushColor> BrushColors;
    public List<Sticker> Stickers;

    public List<Decal> Decals;
}


[System.Serializable]
public class BrushColor
{
    public Color Color;
    public Sprite ColorSprite;


}

[System.Serializable]
public class Sticker
{
    public string StrickerName;
    public Sprite StickerSprite;
    public bool WithRewarded;
    [Range(1, 5)]
    public int Scale;
    

}
[System.Serializable]
public class Decal
{
    public string DecalName;
    public Texture Alpha, InvertedAlpha,DecalMask;
    public Sprite DecalSprite;
}
