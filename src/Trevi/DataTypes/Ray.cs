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

    /// <summary>
    ///     Reflect the ray from an intersection
    /// </summary>
    /// <remarks>
    ///     Formula: Shirley, P174
    ///     intersection normal vector must be unit ( set elsewhere )
    /// </remarks>
    public Ray Reflect(Intersection intersection)
    {
        return new Ray(
            intersection.Position! - (intersection.Normal! * 0.001), // fix for self-shadowing
            (Direction - (intersection.Normal! * 2 * Vector.Dot(Direction, intersection.Normal!))).Unit()
            );
    }

    /// <summary>
    ///     Refract the ray through an intersection
    /// </summary>
    /// <remarks>
    ///     Formula: Shirley, P177, eq 12.7
    ///     ray direction, intersection normal vectors must be unit
    ///     ( set elsewhere to save repeat calculations )
    /// </remarks>
    public Ray Refract(Intersection intersection, double indexSource, double indexDestination)
    {
        var DdotN = Vector.Dot(Direction, intersection.Normal!);
        var N1overN2 = indexSource / indexDestination;
        return new Ray(
            intersection.Position! - (intersection.Normal! * 0.001), // fix for self-shadowing
            (((Direction - (intersection.Normal! * DdotN)) * N1overN2)
                - (intersection.Normal! * Math.Sqrt(1 - (N1overN2 * N1overN2 * (1 - (DdotN * DdotN)))))).Unit()
            );
    }

    #endregion // Member Functions
}
