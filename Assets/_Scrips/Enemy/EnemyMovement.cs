public class EnemyMovement : ParentMovement
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.moveSpeed = 0.01f;
    }
}