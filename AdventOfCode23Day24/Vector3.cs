

namespace AdventOfCode23Day24;
internal readonly record struct Vector3(decimal X, decimal Y, decimal Z)
{
	public static Vector3 operator -(Vector3 a) => new(-a.X, -a.Y, -a.Z);
	public static Vector3 operator -(Vector3 a, Vector3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

	public static Vector3 operator +(Vector3 a, Vector3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

	public static Vector3 operator *(decimal a, Vector3 b) => new(b.X * a, b.Y * a, b.Z * a);
	public static Vector3 operator *(Vector3 a, decimal b) => new(a.X * b, a.Y * b, a.Z * b);

	internal static Vector3 Cross(Vector3 a, Vector3 b) => new(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
	internal static decimal Dot(Vector3 a, Vector3 b) => (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
}
