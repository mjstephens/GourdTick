using System.Collections.Generic;

namespace GalaxyGourd.Tick
{
    internal class TickCollection
    {
        #region VARIABLES
        
        internal readonly List<Dictionary<string, List<ITickable>>> Tickables = new();

        #endregion VARIABLES


        #region INITIALIZATION

        internal TickCollection(IEnumerable<string> orderedGroups)
        {
            foreach (string group in orderedGroups)
            {
                Tickables.Add(new Dictionary<string, List<ITickable>>
                {
                    { group, new() }
                });
            }
        }

        #endregion INITIALIZATION


        #region TICK

        internal void Tick(float delta)
        {
            foreach (Dictionary<string, List<ITickable>> group in Tickables)
            {
                foreach (KeyValuePair<string, List<ITickable>> pair in group)
                {
                    foreach (ITickable tickable in pair.Value)
                    {
                        tickable.Tick(delta);
                    }
                }
            }
        }

        #endregion TICK
    }
}