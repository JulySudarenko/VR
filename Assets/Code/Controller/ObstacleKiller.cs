using System;
using Code.Interfaces;
using Code.Player;
using Object = UnityEngine.Object;

namespace Code.Controller
{
    internal class ObstacleKiller : IInitialize, IExecute, IDisposable
    {
        private readonly Config _config;
        private readonly IEndGame _end;
        private readonly IPlayer _player;
        private readonly ObstacleSpawner _obstacleSpawner;

        public ObstacleKiller(ObstacleSpawner obstacleSpawner, Config config, IPlayer player, IEndGame end)
        {
            _obstacleSpawner = obstacleSpawner;
            _config = config;
            _player = player;
            _end = end;
        }


        public void Execute()
        {
            var obstacles = _obstacleSpawner.SpawnedObstacles;

            for (int i = 0; i < obstacles.Count; i++)
            {
                if (_player.Transform.position.z > obstacles[i].position.z + _config.KillDistanceZ)
                {
                    Object.Destroy(obstacles[i].gameObject);
                }
            }
        }

        private void DestroyAll(bool value)
        {
            var obstacles = _obstacleSpawner.SpawnedObstacles;
            for (int i = 0; i < obstacles.Count; i++)
            {
                Object.Destroy(obstacles[i].gameObject);
            }
        }

        public void Initialize()
        {
            _end.EndGame += DestroyAll;
        }
        
        public void Dispose()
        {
            _end.EndGame -= DestroyAll;
        }
    }
}
