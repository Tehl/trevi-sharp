namespace Trevi.DataTypes;

/// <summary>
///     Vector Library
/// </summary>
/// <remarks>
///     No checks are done on the 'h' variable; thus, care should be taken to ensure
///     that vectors retain the same value for this when performing calculations on them.
/// </remarks>
internal class Vector
{
    /// <summary>
    ///     Constructor
    /// </summary>
    public Vector(double x = 0, double y = 0, double z = 0, double w = 1)
    {
        I = x;
        J = y;
        K = z;
        H = w;
    }

    /// <summary>
    ///     Copy constructor
    /// </summary>
    public Vector(Vector source)
    {
        I = source.I;
        J = source.J;
        K = source.K;
        H = source.H;
    }

    public double I { get; set; }

    public double J { get; set; }

    public double K { get; set; }

    public double H { get; set; }

    #region Operator Overloads

    /// <summary>
    ///     Vector Addition
    /// </summary>
    public static Vector operator +(Vector left, Vector right)
    {
        return new Vector(
            left.I + right.I,
            left.J + right.J,
            left.K + right.K,
            left.H
        );
    }

    /// <summary>
    ///     Vector Subtraction
    /// </summary>
    public static Vector operator -(Vector left, Vector right)
    {
        return new Vector(
            left.I - right.I,
            left.J - right.J,
            left.K - right.K,
            left.H
        );
    }

    /// <summary>
    ///     Negative Overload
    /// </summary>
    public static Vector operator -(Vector vector)
    {
        return new Vector(
            -vector.I,
            -vector.J,
            -vector.K,
            vector.H
        );
    }

    /// <summary>
    ///     Vector by Scalar multiplication
    /// </summary>
    public static Vector operator *(Vector left, double right)
    {
        return new Vector(
            left.I * right,
            left.J * right,
            left.K * right,
            left.H
        );
    }

    /// <summary>
    ///     Vector by Scalar multiplication
    /// </summary>
    public static Vector operator *(Vector left, Vector right)
    {
        return new Vector(
            left.I * right.I,
            left.J * right.J,
            left.K * right.K,
            left.H
        );
    }

    /// <summary>
    ///     Vector by Matrix multiplication
    /// </summary>
    public static Vector operator *(Vector left, Matrix right)
    {
        var leftMatrix = new Matrix(1, 4);
        leftMatrix.Insert(0, left.I, left.J, left.K, left.H);
        leftMatrix *= right;
        return new Vector(
            leftMatrix[0, 0],
            leftMatrix[0, 1],
            leftMatrix[0, 2],
            leftMatrix[0, 3]
        );
    }

    /// <summary>
    ///     Vector by Scalar division
    /// </summary>
    public static Vector operator /(Vector left, double right)
    {
        return new Vector(
            left.I / right,
            left.J / right,
            left.K / right,
            left.H
        );
    }

    /// <summary>
    ///     Vector by Scalar division
    /// </summary>
    public static Vector operator /(Vector left, Vector right)
    {
        return new Vector(
            left.I / right.I,
            left.J / right.J,
            left.K / right.K,
            left.H
        );
    }

    /// <summary>
    ///     Equality
    /// </summary>
    public static bool operator ==(Vector left, Vector right)
    {
        // compare individual components
        if (left.I == right.I && left.J == right.J && left.K == right.K && left.H == right.H)
        {
            // the vectors are equal
            return true;
        }
        else
        {
            // they aren't
            return false;
        }
    }

    /// <summary>
    ///     Inequality
    /// </summary>
    public static bool operator !=(Vector left, Vector right)
    {
        // compare individual components
        if (left.I == right.I && left.J == right.J && left.K == right.K && left.H == right.H)
        {
            // the vectors are equal
            return false;
        }
        else
        {
            // they aren't
            return true;
        }
    }

    #endregion // Operator Overloads

    #region Member Functions

    /// <summary>
    ///     Modulus / Length
    /// </summary>
    public double Mod()
    {
        return Math.Sqrt((I * I) + (J * J) + (K * K));
    }

    /// <summary>
    ///     Unit direction vector
    /// </summary>
    public Vector Unit()
    {
        var mod = Mod();
        return new Vector(I / mod, J / mod, K / mod, H);
    }

    /// <summary>
    ///     Homogenise the vector - [i j k 1]
    /// </summary>
    public void Homogenise()
    {
        I /= H;
        J /= H;
        K /= H;
        H = 1;
    }

    #endregion // Member Functions

    #region Static Member Functions

    /// <summary>
    ///     Dot Product of two vectors
    /// </summary>
    public static double Dot(Vector left, Vector right)
    {
        return (left.I * right.I) + (left.J * right.J) + (left.K * right.K);
    }

    /// <summary>
    ///     Cross Product of two vectors
    /// </summary>
    public static Vector Cross(Vector left, Vector right)
    {
        return new Vector(
            (left.J * right.K) - (left.K * right.J),
            (left.K * right.I) - (left.I * right.K),
            (left.I * right.J) - (left.J * right.I),
            left.H
        );
    }

    #endregion // Static Member Functions

    public override string ToString()
    {
        return $"[{I} {J} {K} {H}]";
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is Vector other && this == other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(I, J, K, H);
    }
}
