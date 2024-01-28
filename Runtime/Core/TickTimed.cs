namespace GalaxyGourd.Tick
{
    internal class TickTimed : TickCollection
    {
        #region VARIABLES
        
        private float _elapsedSincePreviousUpdate;
        private readonly float _interval;

        #endregion VARIABLES


        #region CONSTRUCTION

        internal TickTimed(string[] orderedGroups, float interval) : base(orderedGroups)
        {
            _interval = interval;
        }

        #endregion CONSTRUCTION


        #region API

        internal void Reset()
        {
            Tickables.Clear();
            _elapsedSincePreviousUpdate = 0;
        }

        internal bool TickHasElapsed(float delta)
        {
            _elapsedSincePreviousUpdate += delta;
            if (_elapsedSincePreviousUpdate >= _interval)
            {
                _elapsedSincePreviousUpdate -= _interval;
                return true;
            }
            
            return false;
        }

        #endregion API
    }
}