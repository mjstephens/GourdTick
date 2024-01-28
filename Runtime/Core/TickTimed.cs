namespace GalaxyGourd.Tick
{
    internal class TickTimed : TickCollection
    {
        #region VARIABLES
        
        private float _elapsedSincePreviousUpdate;

        #endregion VARIABLES


        #region CONSTRUCTION

        internal TickTimed(int[] orderedGroups) : base(orderedGroups)
        {
            
        }

        #endregion CONSTRUCTION


        #region API

        internal void Reset()
        {
            Tickables.Clear();
            _elapsedSincePreviousUpdate = 0;
        }

        internal bool TickHasElapsed(float updateRate, float delta)
        {
            _elapsedSincePreviousUpdate += delta;
            if (_elapsedSincePreviousUpdate >= updateRate)
            {
                _elapsedSincePreviousUpdate -= updateRate;
                return true;
            }
            
            return false;
        }

        #endregion API
    }
}