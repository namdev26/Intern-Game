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
    protected virtual void Awake()
    {
        //for override
    }
    protected virtual void OnEnable()
    {
        //for override
    }
    protected virtual void ResetValue()
    {
        //for override
    }
}
