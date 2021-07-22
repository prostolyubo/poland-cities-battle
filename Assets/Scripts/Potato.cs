using System.Collections;
using UnityEngine;

public class Potato : MonoBehaviour
{
    public float timer;
    public ParticleTerminator particleTerminator;
 
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            enabled = false;
            particleTerminator.Terminate(Extinguish);
        }
    }

    void Extinguish()
    {
        Destroy(GetComponent<DamageDealerCollider>());
        Destroy(this);
    }
}
