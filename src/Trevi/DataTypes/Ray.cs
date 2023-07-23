namespace Trevi.DataTypes;

/// <summary>
///     Ray Data Type
/// </summary>
internal class Ray
{
    /// <summary>
    ///     Constructor
    /// </summary>
    public Ray(Vector position, Vector direction)
    {
        Position = position;
        Direction = direction;
    }

    /// <summary>
    ///     Default constructor
    /// </summary>
    public Ray()
    {
        Position = new Vector(0, 0, 0);
        Direction = new Vector(0, 0, 1);
    }

    /// <summary>
    ///     Copy constructor
    /// </summary>
    public Ray(Ray source)
    {
        Position = new Vector(source.Position);
        Direction = new Vector(source.Direction);
    }

    public Vector Position { get; set; }
    public Vector Direction { get; set; }

    #region Member Functions

    // TODO: Reflect()

    // TODO: Refract()

    #endregion // Member Functions
}
