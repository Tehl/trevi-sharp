namespace Trevi.DataTypes;

/// <summary>
///     Triangle Data Type
/// </summary>
internal class Triangle
{
    private readonly Vertex[] _points;

    private readonly double A;
    private readonly double B;
    private readonly double C;
    private readonly double D;
    private readonly double E;
    private readonly double F;

    /// <summary>
    ///     Constructor
    /// </summary>
    public Triangle(Vertex point0, Vertex point1, Vertex point2, Surface surface)
    {
        _points = new Vertex[3] { point0, point1, point2 };
        Surface = surface;

        // Cache values that can be precalculated

        // plane normal
        Normal = Vector.Cross(
            _points[1].Position - _points[0].Position,
            _points[2].Position - _points[0].Position
        ).Unit();

        // raytrace values
        A = _points[0].Position.I - _points[1].Position.I;
        B = _points[0].Position.J - _points[1].Position.J;
        C = _points[0].Position.K - _points[1].Position.K;

        D = _points[0].Position.I - _points[2].Position.I;
        E = _points[0].Position.J - _points[2].Position.J;
        F = _points[0].Position.K - _points[2].Position.K;
    }

    /// <summary>
    ///     Copy constructor
    /// </summary>
    public Triangle(Triangle source)
    {
        Surface = source.Surface;
        _points = new Vertex[3] { source._points[0], source._points[1], source._points[2] };

        Normal = source.Normal;

        A = source.A;
        B = source.B;
        C = source.C;
        D = source.D;
        E = source.E;
        F = source.F;
    }

    public Surface Surface { get; set; }

    public Vector Normal { get; private set; }

    public Vertex Point(int idx)
    {
        return _points[idx];
    }

    /// <summary>
    ///     Test a ray against the triangle
    /// </summary>
    /// <remarks>
    ///     Function modified from Shirley P42;
    ///     algorithm is not my own.
    /// </remarks>
    public Intersection Test(Ray ray)
    {
        // check for backfacing and parrallel triangles
        if (Vector.Dot(ray.Direction, Normal) >= 0)
        {
            return new Intersection(false);
        }

        // resolve intersection using Cramer's rule
        var G = ray.Direction.I;
        var H = ray.Direction.J;
        var I = ray.Direction.K;

        var J = _points[0].Position.I - ray.Position.I;
        var K = _points[0].Position.J - ray.Position.J;
        var L = _points[0].Position.K - ray.Position.K;

        var EIHF = (E * I) - (H * F);
        var GFDI = (G * F) - (D * I);
        var DHEG = (D * H) - (E * G);

        var denom = (A * EIHF) + (B * GFDI) + (C * DHEG);

        var beta = ((J * EIHF) + (K * GFDI) + (L * DHEG)) / denom;

        if (beta is < 0 or > 1)
            return new Intersection(false);

        var AKJB = (A * K) - (J * B);
        var JCAL = (J * C) - (A * L);
        var BLKC = (B * L) - (K * C);

        var gamma = ((I * AKJB) + (H * JCAL) + (G * BLKC)) / denom;

        if (gamma < 0 || beta + gamma > 1)
            return new Intersection(false);

        var tval = -((F * AKJB) + (E * JCAL) + (D * BLKC)) / denom;

        if (tval >= 0)
        {
            // triangle intersects
            return Surface.Smooth == false
                ? new Intersection(
                    true,
                    tval,
                    ray.Position + (ray.Direction * tval),
                    Normal,
                    this,
                    1.0 - beta - gamma, beta, gamma
                    )
                : new Intersection(
                    true,
                    tval,
                    ray.Position + (ray.Direction * tval),
                    ((_points[0].Normal * (1.0 - beta - gamma)) + (_points[1].Normal * beta) + (_points[2].Normal * gamma)).Unit(),
                    this,
                    1.0 - beta - gamma, beta, gamma
                    );
        }

        return new Intersection(false);
    }
}
