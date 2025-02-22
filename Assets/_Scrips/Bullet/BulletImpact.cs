//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using static UnityEngine.RuleTile.TilingRuleOutput;

//[RequireComponent(typeof(BoxCollider2D))]
//[RequireComponent(typeof(Rigidbody2D))]
//public class BulletImpart : BulletAbstract
//{
//    [Header("Bullet Impart")]
//    [SerializeField] protected BoxCollider2D boxCollider;
//    [SerializeField] protected Rigidbody2D _rigidbody;

//    protected override void LoadComponent()
//    {
//        base.LoadComponent();
//        this.LoadCollider();
//        this.LoadRigibody();
//    }

//    protected virtual void LoadCollider()
//    {
//        if (this.boxCollider != null) return;
//        this.boxCollider = GetComponent<BoxCollider2D>();
//        this.boxCollider.isTrigger = true;
//        Debug.Log(transform.name + ": LoadCollider", gameObject);
//    }

//    protected virtual void LoadRigibody()
//    {
//        if (this._rigidbody != null) return;
//        this._rigidbody = GetComponent<Rigidbody2D>();
//        this._rigidbody.isKinematic = true;
//        Debug.Log(transform.name + ": LoadRigibody", gameObject);
//    }

//    protected virtual void OnTriggerEnter(Collider other)
//    {
//        Debug.Log(other.transform.parent);
//        //Debug.Log(transform.parent.name);
//        //Debug.Log(bulletController.Shooter.name);
//        if (other.transform.parent == this.bulletController.Shooter) return;

//        this.bulletController.DamageSender.Send(other.transform);
//        //this.CreateImpactFX(other);
//    }
//}