using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
public class EmeryHead : MonoBehaviour
{
    
    [SerializeField] private ParticleSystem EmeryParticle;
    private float Timer;
    private void OnEnable()
    {
        Timer = 0;
    }
    private void OnTriggerEnter(Collider other)
    {

       
            EmeryParticle.Play();
            MMVibrationManager.Haptic(HapticTypes.SoftImpact);
        
    }
   
   
}
