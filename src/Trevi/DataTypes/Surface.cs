namespace Trevi.DataTypes;

/// <summary>
///     Surface Data Type
/// </summary>
internal class Surface
{
    /// <summary>
    ///     Constructor
    /// </summary>
    public Surface(bool smooth, double refractiveIndex, double opacity, double reflectivity, Material material)
    {
        Smooth = smooth;
        RefractiveIndex = refractiveIndex;
        Opacity = opacity;
        Reflectivity = reflectivity;
        Material = material;
    }

    /// <summary>
    ///     Default Constructor
    /// </summary>
    public Surface()
    {
        Smooth = false;
        RefractiveIndex = 1.00;
        Opacity = 1.00;
        Reflectivity = 0.05;
        Material = new Material(
            diffuse: new Colour(0.75, 0.75, 0.75),
            ambient: new Colour(0.25, 0.25, 0.25),
            specular: new Colour(1, 1, 1),
            emit: new Colour(0, 0, 0),
            shininess: 0.75
        );
    }

    public bool Smooth { get; }
    public double RefractiveIndex { get; }
    public double Opacity { get; }
    public double Reflectivity { get; }
    public Material Material { get; }
}
