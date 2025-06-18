using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Forklifter
{
    public class Storage : MonoBehaviour
    {
        [SerializeField] private Cargo _boxPrefab;
        [SerializeField] private Transform _boxSpawnPoint, _spawnDestination;
        [SerializeField] private float _spawnTime = 5f;
        [SerializeField] private float _despawnTime = 5f;
        [SerializeField] private float _rotationSpeed = 90f;
        [SerializeField] private float _despawnMovementSpeed = 2f;
        [SerializeField] private float _despawnAcceleration = 3f;

        private async void Start()
        {
            await SpawnCargo();
        }

        private async void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out Cargo cargo) && cargo.Stored == false)
            {
                await DespawnCargo(cargo);
            }
        }

        private async UniTask DespawnCargo(Cargo cargo)
        {
            cargo.Stored = true;
            float timer = Time.time + _despawnTime;
            float speed = _despawnMovementSpeed;
            cargo.ToggleInteractive(false);
            while (timer > Time.time)
            {
                if (cargo == null)
                    return;
                cargo.transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
                cargo.transform.Rotate(Vector3.one * _rotationSpeed * Time.deltaTime);
                speed += _despawnAcceleration * Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            Destroy(cargo.gameObject);
            await SpawnCargo();
        }

        private async UniTask SpawnCargo()
        {
            Cargo cargo = Instantiate(_boxPrefab, _boxSpawnPoint.position, new Quaternion());
            float timePassed = 0f;
            while (timePassed < _spawnTime)
            {
                if (cargo == null)
                    return;
                cargo.transform.position = Vector3.Lerp(_boxSpawnPoint.position, _spawnDestination.position, timePassed / _spawnTime);
                cargo.transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
                timePassed += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            cargo.ToggleInteractive(true);
        }
    }
}
