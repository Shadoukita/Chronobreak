using System;
using static Chronobreak.GameServer.Game;

namespace Chronobreak.GameServer.Scripting.CSharp
{
    //Timer class for GameScripts to be able to trigger events after a duration
    public class GameScriptTimer
    {
        private readonly bool _repeat;
        private readonly float _duration;
        private float _currentTime;
        private Action _callback;
        internal bool ToRemove;

        public GameScriptTimer(float duration, Action callback, bool executeImmediately = true, bool repeat = false)
        {
            _duration = duration * 1000;
            _callback = callback;
            _repeat = repeat;

            if (executeImmediately)
            {
                callback();
            }
        }

        public void SetCallback(Action callback)
        {
            _callback = callback;
        }

        public void Update()
        {
            _currentTime += Time.DeltaTime;
            if (_currentTime >= _duration && !ToRemove)
            {
                _callback();

                if (!_repeat)
                {
                    ToRemove = true;
                }
                else
                {
                    _currentTime = 0;
                }
            }
        }

        public double GetPercentageFinished()
        {
            return _currentTime / _duration * 100.0;
        }

        public float GetCurrentTime()
        {
            return _currentTime;
        }

        public void SetTimeTo(float time)
        {
            _currentTime = time;
        }

        public void EndTimerNow()
        {
            _currentTime = _duration;
            _callback();
            ToRemove = true;
        }

        public void EndTimerWithoutCallback()
        {
            _currentTime = _duration;
            ToRemove = true;
        }
    }
}
