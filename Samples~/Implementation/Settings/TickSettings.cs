using UnityEngine;

namespace GalaxyGourd.Tick
{
    /// <summary>
    /// We implement a partial version of TickSettings to introduce custom tick groups and timed ticks. See below for
    /// implementation examples.
    /// </summary>
    public static partial class TickSettings
    {
        #region TICKS

        /*
         *  Here we define a number of custom tick categories for systems in our project. Our ITickable instances will
         *  reference these.
         * 
         *  Technically you don't need to use const values - ITickables could specify string literals. But defining const
         *  values ensures typos don't happen.
         */
        public const string TickVisioncast = "TickVisioncast";
        public const string TickRaycast = "TickRaycast";
        public const string TickDebug = "TickDebug";
        public const string TickControllerHumanoid = "TickControllerHumanoid";
        public const string TickUIUpdate = "TickUIUpdate";
        public const string TickInputCollection = "TickInputCollection";
        public const string TickInputTransmission = "TickInputTransmission";
        public const string TickVisibleObjectBoundsRefresh = "TickVisibleObjectBoundsRefresh";
        public const string TickInteractionSystem = "TickInteractionSystem";
        public const string TickCameraMovement = "TickCameraMovement";
        public const string TickHumanoidAnimation = "TickHumanoidAnimation";
        public const string TickPhysicsDiscoverableSleepTick = "TickPhysicsDiscoverableSleepTick";

        #endregion TICKS
        
        
        #region CORE GROUPS
        
        private static readonly string[] OrderedUpdateGroups =
        {
            TickInputCollection,          // Forwards collected input data from listeners to receivers
            TickInputTransmission,        // Forwards collected input data from listeners to receivers
            TickHumanoidAnimation,
            
            TickDefaultUpdate,
        };
        
        private static readonly string[] OrderedFixedUpdateGroups =
        {
            TickRaycast,
            TickDefaultFixedUpdate,
            TickControllerHumanoid,
            TickInteractionSystem
        };
        
        private static readonly string[] OrderedLateUpdateGroups =
        {
            TickCameraMovement,
            TickDebug,
            TickUIUpdate,
            
            TickDefaultLateUpdate
        };
        
        #endregion CORE GROUPS
        
        
        #region CUSTOM GROUPS
        
        private static readonly DataTimedTickGroup Timed100MSGroup = new()
        {
            Interval = 0.1f,
            OrderedGroups = new []
            {
                TickVisibleObjectBoundsRefresh,
                TickVisioncast
            }
        };
        
        private static readonly DataTimedTickGroup Timed500MSGroup = new()
        {
            Interval = 0.5f,
            OrderedGroups = new[]
            {
                TickPhysicsDiscoverableSleepTick
            }
        };
        
        #endregion CUSTOM GROUPS
        
        
        #region COMPILE

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void OnSubsystemRegistration()
        {
            Ticker.ReceiveTickGroups(
                OrderedUpdateGroups, 
                OrderedFixedUpdateGroups, 
                OrderedLateUpdateGroups,
                new []
                {
                    Timed100MSGroup,
                    Timed500MSGroup
                });
        }
        
        #endregion COMPILE
    }
}