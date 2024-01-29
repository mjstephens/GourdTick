using GalaxyGourd.Tick;

/// <summary>
/// Monobehaviour that implements ticking. Subscription to/from ticker is taken care of in base class. All you need to
/// implement is the TickGroup the Tick() method as shown below.
/// </summary>
public class ExampleTickableBehaviour : TickableBehaviour
{
    /// <summary>
    /// Define the tick group to which this behaviour belongs. This determines how frequently and in what order the Tick()
    /// method will be called.
    /// </summary>
    public override string TickGroup => TickSettings.TickDefaultUpdate; 

    /// <summary>
    /// This will be called as frequently as specified by the TickGroup parameter.
    /// </summary>
    /// <param name="delta">Time elapsed since the previous tick.</param>
    public override void Tick(float delta)
    {
        // Do update stuff here...
    }
}
