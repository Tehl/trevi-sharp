namespace Trevi.DataTypes;

/// <summary>
///     Colour Data Type
/// </summary>
/// <remarks>
///     Trevi C++ source is inconsistent in the way it handles the Alpha channel
///     in operator overloads, with assignment operators implemented differently
///     to their non-assignment counterparts. Since C# doesn't allow assignment
///     operators to be overloaded separately, and the original source is likely
///     a typo, for now we'll assume the Alpha channel should mutate in the same
///     way as the other channels during each arithmetic operation.
/// </remarks>
internal class Colour
{
    /// <summary>
    ///     Constructor
    /// </summary>
    public Colour(double red = 0, double green = 0, double blue = 0, double alpha = 0)
    {
        R = red;
        G = green;
        B = blue;
        A = alpha;
    }

    public double R { get; }
    public double G { get; }
    public double B { get; }
    public double A { get; }

    #region Operator Overloads

    /// <summary>
    ///     Addition
    /// </summary>
    public static Colour operator +(Colour left, Colour right)
    {
        return new Colour(
            left.R + right.R,
            left.G + right.G,
            left.B + right.B,
            left.A + right.A
        );
    }

    /// <summary>
    ///     Subtraction
    /// </summary>
    public static Colour operator -(Colour left, Colour right)
    {
        return new Colour(
            left.R - right.R,
            left.G - right.G,
            left.B - right.B,
            left.A - right.A
        );
    }

    /// <summary>
    ///     Multiplication
    /// </summary>
    public static Colour operator *(Colour left, Colour right)
    {
        return new Colour(
            left.R * right.R,
            left.G * right.G,
            left.B * right.B,
            left.A * right.A
        );
    }

    /// <summary>
    ///     Division
    /// </summary>
    public static Colour operator /(Colour left, Colour right)
    {
        return new Colour(
            left.R / right.R,
            left.G / right.G,
            left.B / right.B,
            left.A / right.A
        );
    }

    /// <summary>
    ///     Scalar Multiplication
    /// </summary>
    public static Colour operator *(Colour left, double right)
    {
        return new Colour(
            left.R * right,
            left.G * right,
            left.B * right,
            left.A * right
        );
    }

    /// <summary>
    ///     Division
    /// </summary>
    public static Colour operator /(Colour left, double right)
    {
        return new Colour(
            left.R / right,
            left.G / right,
            left.B / right,
            left.A / right
        );
    }

    #endregion // Operator Overloads
}
