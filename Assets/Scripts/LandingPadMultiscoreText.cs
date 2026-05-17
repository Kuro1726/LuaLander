using System;
using TMPro;
using UnityEngine;

public class LandingPadMultiscoreText : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;

    private void Awake()
    {
        LandingPad landingPad = GetComponent<LandingPad>();
        textMeshPro.text = "X" + landingPad.GetMultiScore();
    }
}
