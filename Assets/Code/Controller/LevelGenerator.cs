using System;
using System.Collections.Generic;
using Code.Interfaces;
using Code.Player;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controller
{
    internal class LevelGenerator : IInitialize, IExecute, IDisposable
    {
        private readonly List<Transform> _segments;
        private readonly Dictionary<Transform, Vector3> _segmentsSize;
        private readonly IEndGame _end;
        private readonly IPlayer _player;
        private readonly Config _config;
        private float _distance;
        private bool _isEnd;

        public LevelGenerator(Config config, IPlayer player, IEndGame end, Transform folder)
        {
            _config = config;
            _player = player;
            _end = end;
            _segments = new List<Transform>();
            _segmentsSize = new Dictionary<Transform, Vector3>();

            CreateLevel(folder);
        }

        private void CreateLevel(Transform folder)
        {
            for (int i = 0; i < _config.levelSegments.Length; i++)
            {
                var segment = Object.Instantiate(_config.levelSegments[i], folder);
                _segments.Add(segment);
                _segmentsSize.Add(segment, segment.GetComponent<Collider>().bounds.extents);
            }
        }

        public void Execute()
        {
            if (!_isEnd)
            {
                Transform lastObject = _segments[_segments.Count - 1];
                _distance = Vector3.Distance(lastObject.position, _player.Transform.position);

                if (_distance < _config.MinDistance)
                {
                    Transform firstObject = _segments[0];
                    firstObject.position = lastObject.position;

                    Vector3 offset = _segmentsSize[lastObject] + _segmentsSize[firstObject];
                    firstObject.position += Vector3.forward * offset.z;

                    _segments.Remove(firstObject);
                    _segments.Add(firstObject);
                }
            }
        }

        public void Initialize()
        {
            _end.EndGame += EndGame;
        }

        private void EndGame(bool value)
        {
            _isEnd = true;
            for (int i = 0; i < _segments.Count; i++)
            {
                Object.Destroy(_segments[i].gameObject);
            }
        }

        public void Dispose()
        {
            _end.EndGame -= EndGame;
        }
    }
}
