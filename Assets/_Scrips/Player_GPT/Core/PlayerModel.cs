using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public Animator Animator { get; private set; }
    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }
}
