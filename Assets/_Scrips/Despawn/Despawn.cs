using UnityEngine;

public abstract class Despawn : NamMonoBehaviour
{
    protected abstract bool CanDespawn();
    protected virtual void FixedUpdate()
    {
        this.Despawning();
    }
    protected virtual void Despawning()
    {
        if (!this.CanDespawn()) return;
        this.DespawnObj();
    }
    protected virtual void DespawnObj()
    {
        Destroy(transform.parent.gameObject);
    }
}
