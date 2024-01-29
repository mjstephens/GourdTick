using GalaxyGourd.Tick;

/// <summary>
/// Implement ITickable to expose any class to the Tick system. You can inherit MonoBehaviours from TickableBehaviour
/// to save on some boilerplate, but otherwise they behave identically.
/// </summary>
public class ExampleITickable : ITickable
{
    /// <summary>
    /// Define the tick group to which this behaviour belongs. This determines how frequently and in what order the Tick()
    /// method will be called.
    /// </summary>
    public string TickGroup => TickSettings.TickDefaultFixedUpdate;

    public ExampleITickable()
    {
        // We must register with the ticker in order for the Tick() callback to be called.
        // In a MonoBehaviour this would be done in OnEnable()
        Ticker.Register(this);
    }
    
    public void Cleanup()
    {
        // We must unregister this tickable when it is no longer needed.
        // In a MonoBehaviour this would be done in OnDisable()
        Ticker.Unregister(this);
    }
    
    /// <summary>
    /// This will be called as frequently as specified by the TickGroup parameter.
    /// </summary>
    /// <param name="delta">Time elapsed since the previous tick.</param>
    void ITickable.Tick(float delta)
    {
        // Do update stuff here...
    }
}