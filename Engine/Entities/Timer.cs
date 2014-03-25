using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DragonEngine.Entities
{
    public class Timer : GameObject
    {
        #region Properties

        protected double _startTime;
        protected double _duration;
        protected double _goalTime;
        protected double _restTime;

        protected bool _finished = false;
        protected bool _running = false;
        //public delegate void SelfDestroyer(object sender, EventArgs ea);
        //public event SelfDestroyer Destroy;
        #endregion

        #region Getter & Setter

        public double RestTimePercent { get { return _restTime / _duration; } }
        public bool IsRunning { set { _running = value; } get { return _running; } }
        public bool Finish { get { return _finished; } set { _finished = value; } }
        #endregion

        #region Constructor

        public Timer() { }

        public Timer (double pDuration)
        {
            _duration = pDuration;
        }
        #endregion

        #region Function

        public bool IsTimerFinish(GameTime gameTime)
        {
            if (_running)
            {
                CalculateRestTime(gameTime);

                if (_restTime < 0)
                {
                    _finished = true;
                    _running = false;
                }
            }

            return _finished;
        }

        public void StartTimer(GameTime _gameTime)
        {
            if (!_running)
            {
                _startTime = _gameTime.TotalGameTime.TotalMilliseconds;
                _goalTime = _startTime + _duration;
                _running = true;
            }
        }

        public String GetInfo()
        {
            return "StartTime = " + _startTime + ".\nGoalTime = " + _goalTime + "\nDuration = " + _duration + "\nRestTime = " + _restTime + "\nPercent = " + RestTimePercent + "\nFinished : " + _finished;
        }

        public void CalculateRestTime(GameTime gameTime)
        {
            _restTime = _goalTime - gameTime.TotalGameTime.TotalMilliseconds;
            if (_restTime < 0)
            {
                _finished = true;
                _running = false;
            }
        }

        public void ResetTimer(GameTime gameTime)
        {
            _startTime = gameTime.TotalGameTime.TotalMilliseconds;
            _goalTime = _startTime + _duration;
            _finished = false;
            _running = false;
        }
        #endregion
    }

}
