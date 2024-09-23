using UnityEngine;

public class StarsSpawner : ObjectSpawner
{
    private readonly int _starAnimationsCount = 4;

    protected override void Setup(GameObject star, Vector2 parentPosition) {
        base.Setup(star, parentPosition);

        var randAnimationId = Random.Range(1, _starAnimationsCount + 1);
        star.GetComponent<Animator>().SetTrigger($"Animation {randAnimationId}");
    }
}
