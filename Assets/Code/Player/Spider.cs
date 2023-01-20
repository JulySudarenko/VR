using Code.Assistance;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Player
{
    internal class Spider : ISpider
    {
        public Transform Transform { get; }
        public Rigidbody Rigidbody { get; }
        public Animator Animator { get; }
        public IHit Trigger { get; }
        public int ColliderID { get; }

        public Spider(Config config, Transform folder)
        {
            var spider = Object.Instantiate(config.spider, folder).gameObject;
            Transform = spider.transform;
            Rigidbody = spider.GetOrAddComponent<Rigidbody>();
            Animator = spider.GetOrAddComponent<Animator>();
            Trigger = spider.GetOrAddComponent<TriggerHit>();
            var collider = spider.GetOrAddComponent<Collider>();
            ColliderID = collider.GetInstanceID();
        }
    }
}
