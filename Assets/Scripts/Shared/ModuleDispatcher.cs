using System.Collections;
using UnityEngine;

public class ModuleDispatcher : MonoBehaviour
{
    private void OnEnable()
    {
        foreach (var module in GetComponentsInChildren<IModule>())
        {
            module.Setup();
        }
    }

    private void Update()
    {
        foreach (var module in GetComponentsInChildren<IModule>())
        {
            module.Dispatch();
        }
    }
}
