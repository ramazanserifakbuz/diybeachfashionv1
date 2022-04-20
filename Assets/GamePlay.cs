using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GamePlay : MonoBehaviour
{
   public List<GameObject> beachObj = new List<GameObject>();
    public List<GameObject> target = new List<GameObject>();
    public GameObject Target1;
    public GameObject camera2;
    public GameObject ClearGun;
    public GameObject ColorGun;
    public GameObject poffParticle;
    public GameObject complatePanel;
    int i = 0;
    void Start()
    {
        StartCoroutine(StartGame()); ;
    }

    IEnumerator StartGame()
    {
        ClearGun.SetActive(true);
        ColorGun.SetActive(false);
      

        PaintMove.Instance.CurrentTool = ClearGun;
        poffParticle.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
        beachObj[i].transform.DOMove(Target1.transform.position,1f);
        yield return new WaitForSeconds(5f);
        poffParticle.GetComponent<ParticleSystem>().Play();
        ClearGun.SetActive(false);
        ColorGun.SetActive(true);
     
        PaintMove.Instance.CurrentTool = ColorGun;
        yield return new WaitForSeconds(5f);
        beachObj[i].transform.GetChild(0).transform.DOLocalRotate(new Vector3( 0f,180,0),.35f);
        yield return new WaitForSeconds(5f);
        beachObj[i].transform.DOMove(target[i].transform.position, 4f);
        camera2.SetActive(true);
        yield return new WaitForSeconds(5);
        camera2.SetActive(false);
        i++;
        if (i<beachObj.Count) { 
           
            StartCoroutine(StartGame());
        }
        else
        {
            complatePanel.transform.DOScale(1,0.25f).SetEase(Ease.InOutSine);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
