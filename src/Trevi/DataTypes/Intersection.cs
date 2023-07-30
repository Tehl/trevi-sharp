using System.Diagnostics;

namespace Trevi.DataTypes;

/// <summary>
///     Intersection Data Type
/// </summary>
internal class Intersection
{
    private readonly Vector? _position;
    private readonly Vector? _normal;
    private readonly Triangle? _triangle;
    private readonly double _weightA;
    private readonly double _weightB;
    private readonly double _weightC;

    /// <summary>
    ///     Constructor
    /// </summary>
    public Intersection(
        bool intersects,
        double tVal,
        Vector position,
        Vector normal,
        Triangle triangle,
        double weightA,
        double weightB,
        double weightC
    )
    {
        Intersects = intersects;
        TVal = tVal;
        _position = position;
        _normal = normal;
        _triangle = triangle;
        _weightA = weightA;
        _weightB = weightB;
        _weightC = weightC;
    }

    /// <summary>
    ///     Constructor
    /// </summary>
    public Intersection(bool intersects)
    {
        Intersects = intersects;
        TVal = 0;
        _position = null;
        _normal = null;
        _triangle = null;
        _weightA = 1;
        _weightB = 0;
        _weightC = 0;
    }

    /// <summary>
    ///     Returns whether an intersection has occured
    /// </summary>
    public bool Intersects { get; }

    /// <summary>
    ///     Returns the T value
    /// </summary>
    public double TVal { get; }

    /// <summary>
    ///     Returns the intersection position
    /// </summary>
    public Vector? Position
    {
        get
        {
            Debug.Assert(Intersects);
            return _position;
        }
    }

    /// <summary>
    ///     Returns the intersection normal
    /// </summary>
    public Vector? Normal
    {
        get
        {
            Debug.Assert(Intersects);
            return _normal;
        }
    }

    /// <summary>
    ///     Returns the intersecting surface
    /// </summary>
    public Triangle? Triangle
    {
        get
        {
            Debug.Assert(Intersects);
            return _triangle;
        }
    }

    /// <summary>
    ///     Returns the intersection weights
    /// </summary>
    public double WeightA
    {
        get
        {
            Debug.Assert(Intersects);
            return _weightA;
        }
    }

    /// <summary>
    ///     Returns the intersection weights
    /// </summary>
    public double WeightB
    {
        get
        {
            Debug.Assert(Intersects);
            return _weightB;
        }
    }

    /// <summary>
    ///     Returns the intersection weights
    /// </summary>
    public double WeightC
    {
        get
        {
            Debug.Assert(Intersects);
            return _weightC;
        }
    }
}
