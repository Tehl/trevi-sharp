namespace Trevi.DataTypes;

/// <summary>
///     Material Data Type
/// </summary>
internal class Material
{
    /// <summary>
    ///     Constructor
    /// </summary>
    public Material(Colour diffuse, Colour ambient, Colour specular, Colour emit, double shininess)
    {
        Diffuse = diffuse;
        Ambient = ambient;
        Specular = specular;
        Emit = emit;
        Shininess = shininess;
    }

    /// <summary>
    ///     Default constructor
    /// </summary>
    public Material()
    {
        Diffuse = new Colour(0, 0, 0);
        Ambient = new Colour(0, 0, 0);
        Specular = new Colour(0, 0, 0);
        Emit = new Colour(0, 0, 0);
        Shininess = 1.0;
    }

    public Colour Diffuse { get; }
    public Colour Ambient { get; }
    public Colour Specular { get; }
    public Colour Emit { get; }
    public double Shininess { get; }
}
