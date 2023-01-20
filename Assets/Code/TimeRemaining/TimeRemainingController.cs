using System.Collections.Generic;
using Code.Interfaces;
using UnityEngine;


namespace Asteroids
{
    public sealed class TimeRemainingController : IExecute
    {
        private readonly List<ITimeRemaining> _timeRemainings;

        public TimeRemainingController()
        {
            _timeRemainings = TimeRemainingExtensions.TimeRemainings;
        }

        public void Execute()
        {
            for (var i = 0; i < _timeRemainings.Count; i++)
            {
                var obj = _timeRemainings[i];
                obj.CurrentTime -= Time.deltaTime;
                if (obj.CurrentTime <= 0.0f)
                {
                    obj?.Method?.Invoke();
                    if (!obj.IsRepeating)
                    {
                        obj.RemoveTimeRemaining();
                    }
                    else
                    {
                        obj.CurrentTime = obj.Time;
                    }
                }
            }
        }
    }
}
