namespace Trevi.DataTypes;

/// <summary>
///     Vertex Data Type
/// </summary>
internal class Vertex
{
    /// <summary>
    ///     Constructor
    /// </summary>
    public Vertex(Vector position, Vector normal, double u = 0, double v = 0)
    {
        Position = new Vector(position);
        Normal = new Vector(normal);
        U = u;
        V = v;
    }

    /// <summary>
    ///     Copy constructor
    /// </summary>
    public Vertex(Vertex source)
    {
        Position = new Vector(source.Position);
        Normal = new Vector(source.Normal);
        U = source.U;
        V = source.V;
    }

    /// <summary>
    ///     Default constructor
    /// </summary>
    public Vertex()
    {
        Position = new Vector();
        Normal = new Vector();
        U = 0;
        V = 0;
    }

    public Vector Position { get; set; }
    public Vector Normal { get; set; }
    public double U { get; private set; }
    public double V { get; private set; }

    public void UV(double u, double v)
    {
        U = u;
        V = v;
    }
}
