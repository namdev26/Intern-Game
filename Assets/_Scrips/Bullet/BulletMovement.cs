public class BulletMovement : ParentMovement
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.moveSpeed = 15f;
    }
}