using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CanvasAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Start() {
        _animator = GetComponent<Animator>();
    }

    public void SetTrigger(CanvasAnimatorTrigger trigger) {
        var stringTrigger = trigger.ToString();
        _animator.SetTrigger(stringTrigger);
    }

    public void SetTrigger(string triggerName) {
        _animator.SetTrigger(triggerName);
    }
}

[System.Serializable]
public enum CanvasAnimatorTrigger {
    PlayMusic,
    ShowPlaylist
}
