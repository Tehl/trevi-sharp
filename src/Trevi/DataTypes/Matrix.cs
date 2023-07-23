using System.Diagnostics;
using System.Text;

namespace Trevi.DataTypes;

/// <summary>
///     Matrix Library
/// </summary>
internal class Matrix
{
    private readonly int _rows;
    private readonly int _columns;
    private readonly double[] _data;

    /// <summary>
    ///     Constructor
    /// </summary>
    public Matrix(int rows = 4, int columns = 4)
    {
        _rows = rows;
        _columns = columns;

        _data = new double[_rows * _columns];
        for (var i = 0; i < (_rows * _columns); i++)
        {
            _data[i] = 0;
        }
    }

    /// <summary>
    ///     Copy constructor
    /// </summary>
    public Matrix(Matrix source)
    {
        _rows = source._rows;
        _columns = source._columns;

        _data = new double[_rows * _columns];
        for (var i = 0; i < (_rows * _columns); i++)
        {
            _data[i] = source._data[i];
        }
    }

    #region Operator Overloads

    /// <summary>
    ///     Addition operator
    /// </summary>
    public static Matrix operator +(Matrix left, Matrix right)
    {
        Debug.Assert(left._rows == right._rows);
        Debug.Assert(left._columns == right._columns);

        var result = new Matrix(left._rows, left._columns);
        for (var i = 0; i < (left._rows * left._columns); i++)
        {
            result._data[i] = left._data[i] + right._data[i];
        }
        return result;
    }

    /// <summary>
    ///     Subtraction operator
    /// </summary>
    public static Matrix operator -(Matrix left, Matrix right)
    {
        Debug.Assert(left._rows == right._rows);
        Debug.Assert(left._columns == right._columns);

        var result = new Matrix(left._rows, left._columns);
        for (var i = 0; i < (left._rows * left._columns); i++)
        {
            result._data[i] = left._data[i] - right._data[i];
        }
        return result;
    }

    /// <summary>
    ///     Scalar multiplication operator
    /// </summary>
    public static Matrix operator *(Matrix left, double right)
    {
        var result = new Matrix(left._rows, left._columns);
        for (var i = 0; i < (left._rows * left._columns); i++)
        {
            result._data[i] = left._data[i] * right;
        }
        return result;
    }

    /// <summary>
    ///     Scalar division operator
    /// </summary>
    public static Matrix operator /(Matrix left, double right)
    {
        var result = new Matrix(left._rows, left._columns);
        for (var i = 0; i < (left._rows * left._columns); i++)
        {
            result._data[i] = left._data[i] / right;
        }
        return result;
    }

    /// <summary>
    ///     Matrix multiplication
    ///     http://mathworld.wolfram.com/MatrixMultiplication.html
    /// </summary>
    public static Matrix operator *(Matrix left, Matrix right)
    {
        Debug.Assert(left._columns == right._rows);

        var result = new Matrix(left._rows, left._columns);

        for (var i = 0; i < left._rows; i++)
        {
            for (var j = 0; j < left._columns; j++)
            {
                result[i, j] = 0;
                for (var k = 0; k < left._columns; k++)
                {
                    result[i, j] += left[i, k] * right[k, j];
                }
            }
        }

        return result;
    }

    /// <summary>
    ///     Element access overload
    /// </summary>
    public double this[int row, int column]
    {
        get
        {
            Debug.Assert(row >= 0);
            Debug.Assert(row < _rows);
            Debug.Assert(column >= 0);
            Debug.Assert(column < _columns);

            return _data[(row * _columns) + column];
        }
        set
        {
            Debug.Assert(row >= 0);
            Debug.Assert(row < _rows);
            Debug.Assert(column >= 0);
            Debug.Assert(column < _columns);

            _data[(row * _columns) + column] = value;
        }
    }

    /// <summary>
    ///     Row access overload
    /// </summary>
    public Vector this[int row]
    {
        get
        {
            Debug.Assert(row >= 0);
            Debug.Assert(row < _rows);

            return new Vector(
                this[row, 0],
                this[row, 1],
                this[row, 2],
                this[row, 3]
            );
        }
    }

    /// <summary>
    ///     Equality test overload
    /// </summary>
    public static bool operator ==(Matrix left, Matrix right)
    {
        if (left._rows == right._rows && left._columns == right._columns)
        {
            for (var i = 0; i < (left._rows * left._columns); i++)
            {
                if (left._data[i] != right._data[i])
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    /// <summary>
    ///     Inequality test overload
    /// </summary>
    public static bool operator !=(Matrix left, Matrix right)
    {
        if (left._rows == right._rows && left._columns == right._columns)
        {
            for (var i = 0; i < (left._rows * left._columns); i++)
            {
                if (left._data[i] != right._data[i])
                {
                    return true;
                }
            }
        }
        else
        {
            return true;
        }
        return false;
    }

    #endregion // Operator Overloads

    #region Member Functions

    /// <summary>
    ///     Insert the contents of a vector into a specified row
    /// </summary>
    public void Insert(int row, Vector vector)
    {
        Debug.Assert(_columns == 4);
        Debug.Assert(row < _rows);
        Debug.Assert(row >= 0);

        this[row, 0] = vector.I;
        this[row, 1] = vector.J;
        this[row, 2] = vector.K;
        this[row, 3] = vector.H;
    }

    /// <summary>
    ///     Insert a list of doubles into a specified row
    /// </summary>
    public void Insert(int row, double i, double j, double k, double h)
    {
        Debug.Assert(_columns == 4);
        Debug.Assert(row < _rows);
        Debug.Assert(row >= 0);

        this[row, 0] = i;
        this[row, 1] = j;
        this[row, 2] = k;
        this[row, 3] = h;
    }

    /// <summary>
    ///     Calculate the transpose of the matrix
    /// </summary>
    public Matrix Transpose()
    {
        var result = new Matrix(_rows, _columns);
        for (var i = 0; i < _columns; i++)
        {
            for (var j = 0; j < _rows; j++)
            {
                result[i, j] = this[j, i];
            }
        }
        return result;
    }

    /// <summary>
    ///     Calculate the inverse of the matrix
    ///     http://en.wikipedia.org/wiki/Invertible_matrix
    /// </summary>
    public Matrix Inverse()
    {
        if (_rows != _columns)
        {
            throw new InvalidOperationException("Cannot invert non-square matrix");
        }

        if (_rows == 2)
        {
            // easy inversion
            var result = new Matrix(2, 2);

            result[0, 0] = this[1, 1];
            result[0, 1] = -this[0, 1];
            result[1, 0] = -this[1, 0];
            result[1, 1] = this[0, 0];

            result /= (this[0, 0] * this[1, 1]) - (this[0, 1] * this[1, 0]);

            return result;
        }
        else if (_rows == 4)
        {
            // blockwise inversion
            var result = new Matrix(4, 4);

            // sub-matrices
            var A = new Matrix(2, 2);
            A[0, 0] = this[0, 0];
            A[0, 1] = this[0, 1];
            A[1, 0] = this[1, 0];
            A[1, 1] = this[1, 1];
            var B = new Matrix(2, 2);
            B[0, 0] = this[0, 2];
            B[0, 1] = this[0, 3];
            B[1, 0] = this[1, 2];
            B[1, 1] = this[1, 3];
            var C = new Matrix(2, 2);
            C[0, 0] = this[2, 0];
            C[0, 1] = this[2, 1];
            C[1, 0] = this[3, 0];
            C[1, 1] = this[3, 1];
            var D = new Matrix(2, 2);
            D[0, 0] = this[2, 2];
            D[0, 1] = this[2, 3];
            D[1, 0] = this[3, 2];
            D[1, 1] = this[3, 3];

            // calculations
            var W = (A - (B * D.Inverse() * C)).Inverse();
            var X = A.Inverse() * B * (D - (C * A.Inverse() * B)).Inverse() * -1;
            var Y = D.Inverse() * C * (A - (B * D.Inverse() * C)).Inverse() * -1;
            var Z = (D - (C * A.Inverse() * B)).Inverse();

            // composit
            result[0, 0] = W[0, 0];
            result[0, 1] = W[0, 1];
            result[0, 2] = X[0, 0];
            result[0, 3] = X[0, 1];
            result[1, 0] = W[1, 0];
            result[1, 1] = W[1, 1];
            result[1, 2] = X[1, 0];
            result[1, 3] = X[1, 1];
            result[2, 0] = Y[0, 0];
            result[2, 1] = Y[0, 1];
            result[2, 2] = Z[0, 0];
            result[2, 3] = Z[0, 1];
            result[3, 0] = Y[1, 0];
            result[3, 1] = Y[1, 1];
            result[3, 2] = Z[1, 0];
            result[3, 3] = Z[1, 1];

            return result;
        }
        else
        {
            // needs to be 2x2 or 4x4
            throw new InvalidOperationException("Cannot invert a matrix of this size");
        }
    }

    #endregion // Member Functions

    #region Static member functions

    /// <summary>
    ///     Create a transformation matrix to translate points
    /// </summary>
    public static Matrix Translate(double x, double y, double z)
    {
        var result = new Matrix(4, 4);
        result.Insert(0, new Vector(1, 0, 0, 0));
        result.Insert(1, new Vector(0, 1, 0, 0));
        result.Insert(2, new Vector(0, 0, 1, 0));
        result.Insert(3, new Vector(x, y, z, 1));
        return result;
    }

    /// <summary>
    ///     Create a transformation matrix to rotate points around
    ///     a vector
    /// </summary>
    public static Matrix Rotate(double angle, Vector direction)
    {
        var cosA = Math.Cos(angle * 0.0174532925);
        var sinA = Math.Sin(angle * 0.0174532925);

        var result = new Matrix(4, 4);

        result[0, 0] = cosA + ((1 - cosA) * direction.I * direction.I);
        result[1, 0] = ((1 - cosA) * direction.I * direction.J) - (sinA * direction.K);
        result[2, 0] = ((1 - cosA) * direction.I * direction.K) + (sinA * direction.J);

        result[0, 1] = ((1 - cosA) * direction.J * direction.I) + (sinA * direction.K);
        result[1, 1] = cosA + ((1 - cosA) * direction.J * direction.J);
        result[2, 1] = ((1 - cosA) * direction.J * direction.K) - (sinA * direction.I);

        result[0, 2] = ((1 - cosA) * direction.K * direction.I) - (sinA * direction.J);
        result[1, 2] = ((1 - cosA) * direction.K * direction.J) + (sinA * direction.I);
        result[2, 2] = cosA + ((1 - cosA) * direction.K * direction.K);

        result[3, 3] = 1;

        return result;
    }

    /// <summary>
    ///     Create a transformation matrix to scale points
    /// </summary>
    public static Matrix Scale(double x, double y, double z)
    {
        var result = new Matrix(4, 4);
        result.Insert(0, new Vector(x, 0, 0, 0));
        result.Insert(1, new Vector(0, y, 0, 0));
        result.Insert(2, new Vector(0, 0, z, 0));
        result.Insert(3, new Vector(0, 0, 0, 1));
        return result;
    }

    #endregion // Static member functions

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < _rows; i++)
        {
            _ = sb.Append("[");
            for (var j = 0; j < _columns; j++)
            {
                _ = sb.Append(this[i, j] + " ");
            }
            _ = sb.AppendLine("]");
        }
        return sb.ToString();
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is Matrix other && this == other);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(_rows);
        hash.Add(_columns);
        for (var i = 0; i < _data.Length; i++)
        {
            hash.Add(_data[i]);
        }
        return hash.ToHashCode();
    }
}
