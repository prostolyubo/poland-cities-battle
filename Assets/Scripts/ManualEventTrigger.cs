using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class ManualEventTrigger : MonoBehaviour
{
    public UnityEvent OnTriggered;

    public void Trigger()
    {
        OnTriggered.Invoke();
    }
}
