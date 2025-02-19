using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamMonoBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        this.LoadComponent();
    }
    protected virtual void LoadComponent()
    {
        // for override
    }
}
