using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VisualRyth : MonoBehaviour
{
    [Range(0f, 1f)]
    public float chromaticAbbrev;
    public Volume volume;
    private VolumeProfile profile;
    private ChromaticAberration cr;
    private void Start()
    {
        profile = volume.profile;
        profile.TryGet<ChromaticAberration>(out cr);
        RtythManager.instance.OnTick += StartVisual;
    }
    public void StartVisual(float errorMargin)
    {
        StartCoroutine(RhythVisuals(errorMargin, chromaticAbbrev));
    }
    public IEnumerator RhythVisuals(float errorMargin, float chromaticAbbrev, int steps = 5)
    {

        cr.intensity.value = chromaticAbbrev;
        for(int i = 0; i <steps; i++)
        {
            cr.intensity.value -= chromaticAbbrev / steps;
            yield return new WaitForSeconds(errorMargin / steps);
        }
        yield break;
        
    }
}
