using System;
using System.Collections.Generic;

namespace GalaxyGourd.Tick
{
    internal class TickCollection
    {
        #region VARIABLES

        private readonly List<ITickable>[] _tickables;
        private string[] _groupsKeyMap;
        private readonly int _groupsCount;
        private static int _cacheIndex;

        #endregion VARIABLES


        #region INITIALIZATION

        internal TickCollection(IReadOnlyList<string> orderedGroups)
        {
            _groupsCount = orderedGroups.Count;
            _tickables = new List<ITickable>[_groupsCount];
            _groupsKeyMap = new string[_groupsCount];
            for (int i = 0; i < _groupsCount; i++)
            {
                _groupsKeyMap[i] = orderedGroups[i];
                _tickables[i] = new List<ITickable>();
            }
        }

        #endregion INITIALIZATION


        #region TICK

        internal void Tick(float delta)
        {
            for(int i = 0; i < _groupsCount; i++)
            {
                List<ITickable> l = _tickables[i];
                int c = l.Count;
                for(int e = 0; e < c; e++)
                {
                    l[e].Tick(delta);
                }
            }
        }

        #endregion TICK


        #region UTILITY

        internal bool CollectionContainsKey(string key)
        {
            _cacheIndex = Array.IndexOf(_groupsKeyMap, key);
            return _cacheIndex != -1;
        }
        
        internal List<ITickable> GetListForKey()
        {
            return _tickables[_cacheIndex];
        }

        #endregion UTILITY
    }
}