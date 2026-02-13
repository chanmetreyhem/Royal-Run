using UnityEngine;

public class Apple : PickUp
{
    float adjustChangeMoveSpeed = 2;
    protected LevelGenerator levelGenerator;
    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }
    protected override void OnPickUp()
    {
        Debug.Log("Power up");
        levelGenerator.ChangeChunkSpeed(adjustChangeMoveSpeed);
        Destroy(gameObject);
    }
}
