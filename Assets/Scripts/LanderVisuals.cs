using System;
using UnityEngine;

public class LanderVisuals : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftThrusterParticleSystem;
    [SerializeField] private ParticleSystem middleThrusterParticleSystem;
    [SerializeField] private ParticleSystem rightThrusterParticleSystem;
    [SerializeField] private GameObject explosionParticleSystem;

    private Lander lander;

    private void Awake()
    {
        lander = GetComponent<Lander>();
        lander.OnUpForce += LanderOnOnUpForce;
        lander.OnRightForce += LanderOnOnRightForce;
        lander.OnLeftForce += LanderOnOnLeftForce;
        lander.OnBeforeForce += LanderOnOnBeforeForce;

        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, false);
    }

    private void Start()
    {
        lander.OnLanded += Lander_OnOnLanded;
    }

    private void Lander_OnOnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        switch (e.landingType)
        {
            case (Lander.LandingType.TooFastLanding) :
                case(Lander.LandingType.TooSteepAngle) :
                case (Lander.LandingType.WrongLandingArea) :
                    Instantiate(explosionParticleSystem, transform.position, Quaternion.identity);
                    lander.gameObject.SetActive(false);
                    break;
        }
    }

    private void LanderOnOnBeforeForce(object sender, EventArgs e)
    {
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, false);
    }

    private void LanderOnOnUpForce(object sender, EventArgs e)
    {
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, true);
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, true);
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, true);

    }
    
    private void LanderOnOnRightForce(object sender, EventArgs e)
    {
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, true);
    }
    
    private void LanderOnOnLeftForce(object sender, EventArgs e)
    {
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, true);
    }
    
    

    private void SetEnabledThrusterParticleSystem(ParticleSystem particleSystem, bool enabled)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.enabled = enabled;
    }
}
