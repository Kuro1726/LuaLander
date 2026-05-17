using System;
using UnityEngine;

public class LanderVisuals : MonoBehaviour
{
    [SerializeField] private ParticleSystem LeftThrusterParticleSystem;
    [SerializeField] private ParticleSystem MiddleThrusterParticleSystem;
    [SerializeField] private ParticleSystem RightThrusterParticleSystem;

    private Lander lander;

    private void Awake()
    {
        lander = GetComponent<Lander>();
        lander.OnUpForce += LanderOnOnUpForce;
        lander.OnRightForce += LanderOnOnRightForce;
        lander.OnLeftForce += LanderOnOnLeftForce;
        lander.OnBeforeForce += LanderOnOnBeforeForce;

        SetEnabledThrusterParticleSystem(LeftThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(MiddleThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(RightThrusterParticleSystem, false);

       
    }

    private void LanderOnOnBeforeForce(object sender, EventArgs e)
    {
        SetEnabledThrusterParticleSystem(LeftThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(MiddleThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(RightThrusterParticleSystem, false);
    }

    private void LanderOnOnUpForce(object sender, EventArgs e)
    {
        SetEnabledThrusterParticleSystem(LeftThrusterParticleSystem, true);
        SetEnabledThrusterParticleSystem(MiddleThrusterParticleSystem, true);
        SetEnabledThrusterParticleSystem(RightThrusterParticleSystem, true);

    }
    
    private void LanderOnOnRightForce(object sender, EventArgs e)
    {
        SetEnabledThrusterParticleSystem(RightThrusterParticleSystem, true);
    }
    
    private void LanderOnOnLeftForce(object sender, EventArgs e)
    {
        SetEnabledThrusterParticleSystem(LeftThrusterParticleSystem, true);
    }
    
    

    private void SetEnabledThrusterParticleSystem(ParticleSystem particleSystem, bool enabled)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.enabled = enabled;
    }
}
