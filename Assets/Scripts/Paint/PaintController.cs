using DG.Tweening;
using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PaintController : MonoBehaviour
{
    private static PaintController instance;
    

    [SerializeField] private List<GameObject> Tools;
    [SerializeField] private PaintSettings PS;
    [SerializeField] private GameObject SprayTool;
    [SerializeField] private LayerMask Mask,AdjustMask;
   
    [SerializeField] private ParticleSystem SprayEffect;
    [SerializeField] private Material SprayMaterial;
    public GameObject CurrentTool;
  
    private bool PaintUsing;
    [SerializeField] private Transform CooldownCircle;
    [SerializeField] private Image CooldownImage;
    public float cooldown,maxcooldown;
    private GameObject CurrentDecal;
 
    [Header("Control Limitter")]
    [SerializeField] private Vector2 MinimumValues, MaximumValues;
    private Vector3 CurrentToolLastPosition,CurrentPosition;
    void Start()
    {
        instance = GetComponent<PaintController>();

        DefaultSettings();
    }
    public static PaintController Instance()
    {
        return instance;
    }
    public void DefaultSettings()
    {
        DOTween.KillAll();
        CurrentTool = Tools[0];
        CurrentTool.transform.position = new Vector3(0, CurrentTool.transform.position.y, 2.76f);
        CurrentToolLastPosition = CurrentTool.transform.position;
    }
    void Update()
    {
        if (InputSystem.Instance().InputUsing&& PaintUsing)
        {
            PaintToolMove();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(CurrentTool == Tools[1] && InputSystem.Instance().InputUsing && PaintUsing)
            {
                ActivatingPaint();
            }
            if(CurrentTool != Tools[1] && CurrentTool != Tools[5])
            {
                ActiveAgain();
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            CurrentToolLastPosition = CurrentTool.transform.position;
            if(CurrentTool == Tools[1])
            {
                CooldownCircle.gameObject.SetActive(false);
                cooldown = 0;
                Deactive();
            }
        }
      
      
    }
    public void PaintUsingBool(bool value)
    {
       PaintUsing = value;
    }
    public void ActivatingPaint()
    {
       if(cooldown < maxcooldown)
        {
            if(cooldown > 0.1f)
            {
                CooldownCircle.gameObject.SetActive(true);
            }
            Vector3 screenPos = Camera.main.WorldToScreenPoint(CurrentTool.transform.position);
            CooldownCircle.transform.position = screenPos;
            cooldown += 1 * Time.deltaTime;
            float fillAmount = 1 / (maxcooldown / cooldown);
            CooldownImage.DOFillAmount(fillAmount, 0.1f);
        }
        else
        {
            CooldownCircle.gameObject.SetActive(false);
            cooldown = maxcooldown;
            ActiveAgain();
        }
     
        
    }
    public void ActiveStickerButon()
    {
        if (CurrentTool == Tools[5])
        {
        //    UIManager.Instance().StickerApplyButon.SetActive(true);
        }
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
        if (CurrentPosition.x > MaximumValues.x)
        {
            CurrentTool.transform.position = new Vector3(MaximumValues.x - 0.01f, CurrentPosition.y, CurrentPosition.z);
        }
        if (CurrentPosition.x < MinimumValues.x)
        {
            CurrentTool.transform.position = new Vector3(MinimumValues.x + 0.01f, CurrentPosition.y, CurrentPosition.z);
        }
        //VERTICAL MOVE
        if (CurrentPosition.z <= MaximumValues.y && CurrentPosition.z >= MinimumValues.y)
        {
            CurrentTool.transform.DOMoveZ(NewPosition.z, 0.1f);
        }
        else if (CurrentPosition.z > MaximumValues.y)
        {    
            CurrentTool.transform.position = new Vector3(CurrentPosition.x, CurrentPosition.y, MaximumValues.y-0.01f);
        }
        else if (CurrentPosition.z < MinimumValues.y)
        {
            CurrentTool.transform.position = new Vector3(CurrentPosition.x, CurrentPosition.y, MinimumValues.y+0.01f);
        }
        if(cooldown == maxcooldown && CurrentTool == Tools[1])
        {
            MMVibrationManager.Haptic(HapticTypes.SoftImpact);
        }
        AdjustTransform();
    }
    public void ActiveAgain()
    {
        if (CurrentTool == Tools[0])
        {
            CurrentTool.GetComponentInChildren<PaintIn3D.P3dHitThrough>().enabled = true;
            CurrentTool.GetComponentInChildren<Collider>().enabled = true;
        }
        if (CurrentTool == Tools[5])
        {
            UIManager.Instance().StickerApplyButon.SetActive(true);
        }
        if (CurrentTool == Tools[1] || CurrentTool == Tools[4])
        {
            CurrentTool.GetComponentInChildren<PaintIn3D.P3dHitThrough>().enabled = true;
            CurrentTool.GetComponentInChildren<ParticleSystem>().Play();
        }
        if (CurrentTool == Tools[2])
        {
            CurrentTool.GetComponentInChildren<Collider>().enabled = true;

        }
    }
    public void Deactive()
    {
        if (CurrentTool == Tools[0])
        {
            CurrentTool.GetComponentInChildren<PaintIn3D.P3dHitThrough>().enabled = false;
            CurrentTool.GetComponentInChildren<Collider>().enabled = false;
        }
        if (CurrentTool == Tools[1] || CurrentTool == Tools[4])
        {
            CurrentTool.GetComponentInChildren<PaintIn3D.P3dHitThrough>().enabled = false;
            CurrentTool.GetComponentInChildren<ParticleSystem>().Stop();
        }
        if (CurrentTool == Tools[2])
        {
            CurrentTool.GetComponentInChildren<Collider>().enabled = false;
            CurrentTool.GetComponentInChildren<ParticleSystem>().Stop();
        }
    }
    void AdjustTransform()
    {
       /* if (Physics.Raycast(CurrentTool.transform.position + new Vector3(0,2,0),Vector3.down, out RaycastHit hit, Mathf.Infinity, AdjustMask))
        {
            Debug.Log("Hitting Clip");
            float hitPointY = hit.point.y;
            //CurrentTool.transform.position = new Vector3(CurrentTool.transform.position.x, hitPointY + 0.25f, CurrentTool.transform.position.z);
            CurrentTool.transform.DOMove(new Vector3(CurrentTool.transform.position.x, hitPointY + 0.25f, CurrentTool.transform.position.z), 0.2f);
        }
        else
        {
            CurrentTool.transform.DOMove(new Vector3(CurrentTool.transform.position.x, 1, CurrentTool.transform.position.z), 0.2f);
            //CurrentTool.transform.position = new Vector3(CurrentTool.transform.position.x, 1, CurrentTool.transform.position.z);
        }

        AdjustRotation();*/
    }
    void AdjustRotation()
    {
        float XDiffrence = 0 + CurrentTool.transform.position.x;
        CurrentTool.transform.DORotate(new Vector3(CurrentTool.transform.eulerAngles.x, XDiffrence * 20, CurrentTool.transform.eulerAngles.z), 0.1f);
    }
    public void ChangeTool(int toolID)
    {
        DOTween.Kill(CurrentTool.transform);
        CurrentTool.transform.position = new Vector3(-0.5f, CurrentTool.transform.position.y, 2.5f);
        for (int i = 0; i < Tools.Count; i++)
        {
            if(i != toolID)
            {
                Tools[i].SetActive(false);
            }
          
        }
        Tools[toolID].SetActive(true);
        CurrentTool = Tools[toolID];
        ToolPositionAdjust();
    }
    public void ToolOff(bool value = false)
    {
        CurrentTool.SetActive(value);
        ToolPositionAdjust();
    }
    void ToolPositionAdjust()
    {
        if (CurrentTool == Tools[5])
        {
            //CurrentTool.transform.position = new Vector3(CurrentClip.transform.position.x, CurrentTool.transform.position.y, CurrentClip.transform.position.z);
        }
        else
        {
            CurrentTool.transform.position = new Vector3(-0.5f, CurrentTool.transform.position.y, 2.3f);
        }
    }
    public void ChangeBrushColor(int colorID)
    {
        Color _color = PS.BrushColors[colorID].Color;
        CurrentTool.GetComponentInChildren<PaintIn3D.P3dPaintSphere>().Color = _color;
        
        SprayEffect.startColor = _color; ;
        foreach (ParticleSystem ps in SprayEffect.GetComponentsInChildren<ParticleSystem>())
        {
            ps.startColor = _color;
        }
        foreach (ParticleSystemRenderer ren in SprayEffect.GetComponentsInChildren<ParticleSystemRenderer>())
        {
            ren.material.color = _color;
        }
        SprayMaterial.color = _color;
    }
    public void ChangeSticker(int stickerID)
    {
        CurrentTool.GetComponentInChildren<PaintIn3D.P3dPaintDecal>().Texture = PS.Stickers[stickerID].StickerSprite.texture;
        CurrentTool.GetComponentInChildren<PaintIn3D.P3dPaintDecal>().Scale = new Vector3(1,1,1) * PS.Stickers[stickerID].Scale;
        CurrentTool.SetActive(true);
    }

    public IEnumerator PasteDecal(int decalID)
    {
     /*   LevelGenerator.Instance().ResetChangeCounter(true);
        CurrentDecal = CurrentClip.transform.GetChild(0).gameObject;
        CurrentDecal.GetComponent<MeshRenderer>().material.mainTexture = PS.Decals[decalID].InvertedAlpha;
        CurrentDecal.GetComponent<PaintIn3D.P3dPaintableTexture>().LocalMaskTexture = PS.Decals[decalID].DecalMask;
        CurrentDecal.SetActive(true);
        CurrentDecal.transform.DOMove(CurrentClip.transform.position, 1f);
        CurrentDecal.transform.DORotate(CurrentClip.transform.eulerAngles, 0.5f);*/

     //   CurrentClip.GetComponent<PaintIn3D.P3dPaintableTexture>().LocalMaskTexture = PS.Decals[decalID].Alpha;
       
        yield break;
    }
    public void RemoveDecal()
    {
//        LevelGenerator.Instance().ResetChangeCounter(false);
//        CurrentClip.GetComponent<PaintIn3D.P3dPaintableTexture>().LocalMaskTexture = null;
//        CurrentDecal = CurrentClip.transform.GetChild(0).gameObject;
     /*   CurrentDecal.transform.DOMove(CurrentClip.transform.position + new Vector3(0,3,7), 1f).OnComplete(() =>
        {
            CurrentDecal.SetActive(false);
        });*/
        
    }
    public void SendClipToHair()
    {
        /*
        CurrentClip.transform.SetParent(HairClipTransforms[0]);
        ClipTransformHolder CTH = CurrentClip.GetComponent<ClipTransformHolder>();
      
        CurrentClip.transform.DOScale(CTH.HairScale, 1);
        CurrentClip.transform.DOLocalMove(CTH.HairPosition, 1f);
        CurrentClip.transform.DOLocalRotate(CTH.HairRotation, 1f).OnComplete(() =>
        {
            ParticleManager.Instance().Get("StarPoof", CurrentClip.transform.position+new Vector3(0,0,-0.5f), 1, 2f);
            UIManager.Instance().PopReactionText(CurrentClip.transform.position);
            StartCoroutine(ProgressKeeper.Instance().Finish());
        }); ;*/

       
    }
}
