using System.Collections.Generic;
using UnityEngine;

namespace GalaxyGourd.Tick
{
    /// <summary>
    /// Collates tick callbacks
    /// </summary>
    public static class Ticker
    {
        #region VARIABLES
        
        // Updates
        private static TickCollection _collectionUpdate;
        private static TickCollection _collectionFixedUpdate;
        private static TickCollection _collectionLateUpdate;
        private static TickTimed[] _collectionsTimed;

        private static readonly List<ITickable> _queueAdd = new();
        private static readonly List<ITickable> _queueRemove = new();
        
        #endregion VARIABLES


        #region LOAD
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void OnAfterSceneLoad()
        {
            // Fallback, load default groups
            if (_collectionUpdate == null)
            {
                ClearAllTickables();
                ReceiveTickGroups(null, null, null, null);
            }
            
            // Create the object that will call the Unity Update functions for us
            GameObject tickSource = new GameObject("[TickSource]");
            tickSource.AddComponent<TickSource>();
        }

        #endregion LOAD
        
        
        #region REGISTRATION

        public static void Register(ITickable tickable)
        {
            // Immediately add if strict
            if (tickable is ITickableStrict)
            {
                Add(tickable);
            }
            else
            {            
                _queueAdd.Add(tickable);
            }
        }

        public static void Unregister(ITickable tickable)
        {
            // Immediately remove if strict
            if (tickable is ITickableStrict)
            {
                Remove(tickable);
            }
            else
            {
                _queueRemove.Add(tickable);
            }
        }

        internal static void FlushQueued()
        {
            foreach (ITickable tickable in _queueAdd)
            {
                Add(tickable);
            }
            
            foreach (ITickable tickable in _queueRemove)
            {
                Remove(tickable);
            }
            
            _queueAdd.Clear();
            _queueRemove.Clear(); 
        }

        private static void Add(ITickable tickable)
        {
            List<ITickable> collection = GetTickGroupList(tickable.TickGroup);
            collection?.Add(tickable);
        }

        private static void Remove(ITickable tickable)
        {
            List<ITickable> collection = GetTickGroupList(tickable.TickGroup);
            collection?.Remove(tickable);
        }

        #endregion REGISTRATION


        #region TICK

        internal static void TickUpdate(float delta)
        {
            _collectionUpdate.Tick(delta);
            CheckCustomTicks(delta);
        }
        
        internal static void TickFixedUpdate(float delta)
        {
            _collectionFixedUpdate.Tick(delta);
        }
        
        internal static void TickLateUpdate(float delta)
        {
            _collectionLateUpdate.Tick(delta);
            FlushQueued();
        }

        private static void CheckCustomTicks(float delta)
        {
            foreach (TickTimed timedGroup in _collectionsTimed)
            {
                if (timedGroup.TickHasElapsed(delta))
                {
                    timedGroup.Tick(delta);
                }
            }
        }

        #endregion TICK


        #region UTILITY

        internal static void ReceiveTickGroups(
            string[] orderedUpdateGroups,
            string[] orderedFixedUpdateGroups,
            string[] orderedLateUpdateGroups,
            DataTimedTickGroup[] timedGroups)
        {
            // 
            ClearAllTickables();
            
            //
            string[] updateGroups = orderedUpdateGroups ?? new[] { TickSettings.TickDefaultUpdate };
            _collectionUpdate = new TickCollection(updateGroups);
            string[] fixedUpdateGroups = orderedFixedUpdateGroups ?? new[] { TickSettings.TickDefaultFixedUpdate };
            _collectionFixedUpdate = new TickCollection(fixedUpdateGroups);
            string[] lateUpdateGroups = orderedLateUpdateGroups ?? new[] { TickSettings.TickDefaultLateUpdate };
            _collectionLateUpdate = new TickCollection(lateUpdateGroups);
            
            if (timedGroups == null)
                return;
            
            //
            _collectionsTimed = new TickTimed[timedGroups.Length];
            for (int i = 0; i < timedGroups.Length; i++)
            {
                _collectionsTimed[i] = new TickTimed(timedGroups[i].OrderedGroups, timedGroups[i].Interval);
            }
        }

        private static List<ITickable> GetTickGroupList(string key)
        {
            // Check core groups
            foreach (Dictionary<string, List<ITickable>> group in _collectionUpdate.Tickables)
                if (group.TryGetValue(key, out var list)) return list;
            foreach (Dictionary<string, List<ITickable>> group in _collectionFixedUpdate.Tickables)
                if (group.TryGetValue(key, out var list)) return list;
            foreach (Dictionary<string, List<ITickable>> group in _collectionLateUpdate.Tickables)
                if (group.TryGetValue(key, out var list)) return list;
            
            // Check timed groups
            foreach (TickTimed timed in _collectionsTimed)
            {
                foreach (Dictionary<string, List<ITickable>> group in timed.Tickables)
                    if (group.TryGetValue(key, out var list)) return list;
            }

            return null;
        }

        private static void ClearAllTickables()
        {
            _queueAdd.Clear();
            _queueRemove.Clear();

            _collectionUpdate = null;
            _collectionFixedUpdate = null;
            _collectionLateUpdate = null;
        }

        #endregion UTILITY
    }
}