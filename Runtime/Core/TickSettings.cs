namespace GalaxyGourd.Tick
{
    public static partial class TickSettings
    {
        // Default tick groups, always included
        public const string TickDefaultUpdate = "DefaultUpdate";
        public const string TickDefaultFixedUpdate = "DefaultFixedUpdate";
        public const string TickDefaultLateUpdate = "DefaultLateUpdate";
        
        // Stub. Implement partial counterpart inside assets folder alongside GG.Tick asmdef reference. There you 
        // define the tick groups, their order, and also commit them to the ticker. See the 'Core' package sample for an
        // example implementation.
    }
}