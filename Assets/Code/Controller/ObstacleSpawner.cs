using System.Collections.Generic;
using Code.Interfaces;
using Code.Player;
using UnityEngine;

namespace Code.Controller
{
    internal class ObstacleSpawner : IExecute
    {
        
        private readonly List<Transform> _spawnedObstacles;
        private readonly Transform _folder;
        private readonly Config _config;
        private readonly IPlayer _player;
        private Vector3 _lastPosition;
        

        public List<Transform> SpawnedObstacles
        {
            get
            {
                _spawnedObstacles.RemoveAll(TransformIsNull);
                return _spawnedObstacles;
            }
        }

        private bool TransformIsNull(Transform t)
        {
            return t == null;
        }

        public ObstacleSpawner(Config config, IPlayer player, Transform folder)
        {
            _player = player;
            _folder = folder;
            _config = config;

            _spawnedObstacles = new List<Transform>();
            _lastPosition = _player.Transform.position;
        }

        public void Execute()
        {
            if (_player.Transform.position.z > _lastPosition.z + _config.SpawnStep)
            {
                _lastPosition.z += _config.SpawnStep;

                var newObstacle = Object.Instantiate(_config.obstacles[Random.Range(0, _config.obstacles.Length)],
                    new Vector3(Random.Range(_config.SegmentWidth.x, _config.SegmentWidth.y), 0,
                        _lastPosition.z + _config.SpawnDistance), Quaternion.identity, _folder);

                _spawnedObstacles.Add(newObstacle);
            }
        }
    }
}
