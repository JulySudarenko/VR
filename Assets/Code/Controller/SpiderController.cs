using System;
using Code.Interfaces;
using Code.Player;
using UnityEngine;

namespace Code.Controller
{
    internal class SpiderController : IInitialize, IExecute, IDisposable
    {
        private readonly IPlayer _player;
        private readonly ISpider _spider;
        private readonly Config _config;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack1 = Animator.StringToHash("Attack");
        private bool _isAttack;

        public SpiderController(IPlayer player, ISpider spider, Config config)
        {
            _player = player;
            _spider = spider;
            _config = config;
        }

        public void Initialize()
        {
            _spider.Trigger.OnHitEnter += CheckHit;
        }

        public void Execute()
        {
            if (!_isAttack)
            {
                Vector3 dir = _player.Rigidbody.velocity;
                dir.x = _spider.Transform.forward.x * _config.SpiderSpeed * Time.deltaTime;
                _spider.Rigidbody.velocity = dir;
                _spider.Animator.SetFloat(Speed, _spider.Rigidbody.velocity.z);
            }
        }

        private void CheckHit(int id)
        {
            if (id == _player.ColliderID)
            {
                _isAttack = true;
                _spider.Animator.SetFloat(Speed, 0.0f);
                Attack();
            }
        }

        private void Attack()
        {
            _spider.Transform.LookAt(_player.Transform);
            _spider.Animator.SetTrigger(Attack1);
        }

        public void Dispose()
        {
            _spider.Trigger.OnHitEnter -= CheckHit;
        }
    }
}
