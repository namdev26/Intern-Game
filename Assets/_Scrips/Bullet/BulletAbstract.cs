//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class BulletAbstract : NamMonoBehaviour
//{
//    [Header("Bullet Abstract")]
//    [SerializeField] protected BulletController bulletController;
//    protected BulletController BulletController { get => bulletController; }
//    protected override void LoadComponent()
//    {
//        base.LoadComponent();
//        this.LoadDamageReceiver();
//    }
//    protected virtual void LoadDamageReceiver()
//    {
//        if (this.BulletController != null) return;
//        this.bulletController = transform.parent.GetComponent<BulletController>();
//        Debug.Log(transform.name + "Loading damage receiver", gameObject);
//    }
//}