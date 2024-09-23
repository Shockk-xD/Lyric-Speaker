using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private Vector2 _vector;

    private float _destroyY = 7;

    private void Update() {
        transform.Translate(_speed * Time.deltaTime * _vector);

        if (transform.position.y > _destroyY)
            Destroy(gameObject);
    }
}
