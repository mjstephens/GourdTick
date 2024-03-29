using System;
using UnityEngine;

namespace GalaxyGourd.Tick
{
    /// <summary>
    /// Basic MonoBehaviour tickable implementation, eliminates boilerplate for simple tickables.
    /// Use this as a base class if you don't require any advanced or custom implementation.
    /// </summary>
    public abstract class TickableBehaviour : MonoBehaviour, ITickable
    {
        #region VARIABLES

        public abstract string TickGroup { get; }

        #endregion VARIABLES


        #region INITIALIZATION

        protected virtual void OnEnable()
        {
            Ticker.Register(this);
        }

        protected virtual void OnDisable()
        {
            Ticker.Unregister(this);
        }

        #endregion INITIALIZATION


        #region TICK

        public abstract void Tick(float delta);

        #endregion TICK
    }
}