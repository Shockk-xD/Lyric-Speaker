using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private Transform[] _spawnPositions;

    [SerializeField] private float _minSpawnTime = 2.5f;
    [SerializeField] private float _maxSpawnTime = 5;
    
    private readonly float _maxPositionOffset = 0.5f;
    private float _nextSpawnTime = 0;

    protected virtual void Spawn() {
        for (int i = 0; i < _spawnPositions.Length; i++) {
            var randPrefabId = Random.Range(0, _prefabs.Length);
            var prefab = Instantiate(_prefabs[randPrefabId]);
            
            Setup(prefab, _spawnPositions[i].position);
        }

        _nextSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
        Invoke(nameof(Spawn), _nextSpawnTime);
    }

    protected virtual void Setup(GameObject obj, Vector2 parentPosition) {
        var positionOffsetX = Random.Range(-_maxPositionOffset, _maxPositionOffset);
        var positionOffsetY = Random.Range(-_maxPositionOffset, _maxPositionOffset);
        obj.transform.localPosition = parentPosition + new Vector2(positionOffsetX, positionOffsetY);
    }

    private void OnEnable() {
        Invoke(nameof(Spawn), _nextSpawnTime / 2);
    }

    private void OnDisable() {
        CancelInvoke(nameof(Spawn));
    }
}
