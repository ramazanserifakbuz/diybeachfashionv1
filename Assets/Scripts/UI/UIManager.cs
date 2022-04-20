using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    [SerializeField] private PaintSettings PS;
    [SerializeField] private Transform ColorButonGrid,StickerButonGrid,DecalButonGrid;
    
    public GameObject ColorSelectionPanel,StickerSelectionPanel,DecalSelectionPanel,ProgressPanel,EndGamePanel,Guide;
    public GameObject StickerApplyButon;
    public TextMeshProUGUI InfoTM;
    public List<GameObject> UITakens;
    [SerializeField] private Transform PopReactionTransform;
    [Header("Stage")]
    public List<GameObject> StageDots;
    [SerializeField] private Transform StageCounterGrid;
    [SerializeField] private Sprite DoneColor, CurrentColor,NoneColor;
    void Start()
    {
        instance = GetComponent<UIManager>();
       
    }
    public static UIManager Instance()
    {
        return instance;
    }
    
    public void DefaultSettings()
    {

        ClosePanels();
        foreach(GameObject taken in UITakens)
        {
            Pool.Instance().SendBack(taken);
        }
        ProgressPanel.SetActive(true);
        StageDots.Clear();
        FillColors();
        FillStickers();
        FillDecals();
        FillStages(6);
    }
    void ClosePanels()
    {
        ColorSelectionPanel.SetActive(false);
        StickerSelectionPanel.SetActive(false);
    }
    void FillColors()
    {
        for (int i = 0; i < PS.BrushColors.Count; i++)
        {
            GameObject Buton = Pool.Instance().Get("ColorButon");
            UITakens.Add(Buton);
            Buton.transform.SetParent(ColorButonGrid);
            Buton.GetComponent<ColorButon>().SetValues(i, PS.BrushColors[i].ColorSprite);

        }
    }
    public void ResetOutlines()
    {
        foreach (GameObject taken in UITakens)
        {
            taken.GetComponentInChildren<Image>().material = null;
        }
    }
    void FillStickers()
    {
        for (int i = 0; i < PS.Stickers.Count; i++)
        {
            GameObject Buton = Pool.Instance().Get("StickerButon");
            UITakens.Add(Buton);
            Buton.transform.SetParent(StickerButonGrid);
            Buton.GetComponent<StickerButon>().SetValues(i, PS.Stickers[i].StickerSprite);

        }
    }
    void FillDecals()
    {
        for (int i = 0; i < PS.Decals.Count; i++)
        {
            GameObject Buton = Pool.Instance().Get("DecalButon");
            UITakens.Add(Buton);
            Buton.transform.SetParent(DecalButonGrid);
            Buton.GetComponent<DecalButon>().SetValues(i, PS.Decals[i].DecalSprite);

        }
    }
    public void FillStages(int stage_count)
    {
        if(StageDots.Count != 0)
        {
           // StageDots.Clear();
        }
        
        for (int i = 0; i < stage_count; i++)
        {
            GameObject Dot = Pool.Instance().Get("StageDot");
            StageDots.Add(Dot);
            Dot.GetComponent<Image>().sprite = NoneColor;
            UITakens.Add(Dot);
            Dot.transform.SetParent(StageCounterGrid);
        }
        UpdateStage(0);
    }
    public void UpdateStage(int current_stage)
    {
        if(current_stage > 0)
        {
            StageDots[current_stage - 1].transform.GetComponent<Image>().sprite = DoneColor;
        }
       
        StageDots[current_stage].transform.GetComponent<Image>().sprite = CurrentColor;
      
    }
    public void LastStage() {
        StageDots[5].transform.GetComponent<Image>().sprite= DoneColor;
    }
    public void PopReactionText(Vector3 pos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);
        PopReactionTransform.position = screenPos + new Vector3(0,30,0);
        PopReactionTransform.gameObject.SetActive(true);
        Invoke("ClosePop", 1f);
    }
    void ClosePop()
    {
        PopReactionTransform.gameObject.SetActive(false);
    }
}
