using System;
using System.Threading.Tasks;
using Code.Interfaces;
using Code.Player;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Controller
{
    internal class EndGameController : IEndGame, IDisposable, IInitialize
    {
        public event Action<bool> EndGame;
        private readonly IPlayer _player;
        private readonly IVRChecker _vrChecker;
        private readonly Transform _folder;
        private readonly ISpider _spider;
        private readonly Config _config;
        private bool _isEnd;

        public EndGameController(IPlayer player, ISpider spider, Config config, IVRChecker vrChecker)
        {
            _player = player;
            _spider = spider;
            _config = config;
            _vrChecker = vrChecker;
            StartTimer();
        }

        public void Initialize()
        {
            _spider.Trigger.OnHitEnter += CheckHit;
        }

        private void CheckHit(int id)
        {
            if (id == _player.ColliderID)
            {
                if (!_isEnd)
                {
                    Wait();

                    _player.Transform.LookAt(_spider.Transform);
                    _isEnd = true;
                }
            }
        }

        private async void Wait()
        {
            await Task.Delay(_config.AttackTime);
            EndGame?.Invoke(false);
            _player.Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
            Object.Destroy(_spider.Transform.gameObject);
        }

        private async void StartTimer()
        {
            await Task.Delay(_config.GameTime);
            if (!_isEnd)
            {
                EndGame?.Invoke(true);
                _player.Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
                _isEnd = true;
                //Object.Destroy(_spider.Transform.gameObject);
                _spider.Transform.gameObject.SetActive(false);
            }
        }

        public void Dispose()
        {
            _spider.Trigger.OnHitEnter -= CheckHit;
        }
    }
}
