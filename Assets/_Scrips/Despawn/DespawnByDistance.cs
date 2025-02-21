using UnityEngine;

public class DespawnByDistance : Despawn
{
    [SerializeField] private float limitDistance = 10f;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float distance;
    private void Awake()
    {
        this.LoadComponent();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadMainCamera();
    }

    private void LoadMainCamera()
    {
        if (this.mainCamera != null) return;
        this.mainCamera = FindObjectOfType<Camera>().transform;
        Debug.LogWarning("LoadMainCamera");
    }
    protected override bool CanDespawn()
    {
        this.distance = Vector2.Distance(this.transform.position, this.mainCamera.position);
        if (this.distance > this.limitDistance) return true;
        return false;
    }
}
