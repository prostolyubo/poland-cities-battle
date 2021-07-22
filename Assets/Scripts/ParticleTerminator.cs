using System;
using System.Collections;
using Assets;
using UnityEngine;

public class ParticleTerminator : MonoBehaviour
{
    public ParticleSystem particles;
    public void Terminate(Action callback)
    {
        StartCoroutine(FinishingRoutine(callback));
    }

    private IEnumerator FinishingRoutine(Action callback)
    {
        var emission = particles.emission;
        emission.rateOverTimeMultiplier = 0;
        while (particles.particleCount > 0)
            yield return null;
        callback?.Invoke();
    }
}