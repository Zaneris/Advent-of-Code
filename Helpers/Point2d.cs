namespace AdventOfCode.Helpers;

public readonly struct Point2d
{
    public long Y { get; init; }
    public long X { get; init; }

    public Point2d(long y, long x)
    {
        Y = y;
        X = x;
    }

    public override string ToString()
    {
        return $"{Y.ToString()},{X.ToString()}";
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Y, X);
    }

    private bool Equals(Point2d other)
    {
        return Y.Equals(other.Y) && X.Equals(other.X);
    }

    public override bool Equals(object? obj)
    {
        return obj is Point2d other && Equals(other);
    }

    public static Point2d operator +(Point2d a, Point2d b)
    {
        return new Point2d(a.Y + b.Y, a.X + b.X);
    }

    public static Point2d operator -(Point2d a, Point2d b)
    {
        return new Point2d(a.Y - b.Y, a.X - b.X);
    }

    public static bool operator !=(Point2d a, Point2d b)
    {
        return !a.Equals(b);
    }

    public static bool operator ==(Point2d a, Point2d b)
    {
        return a.Equals(b);
    }

    public static implicit operator string(Point2d p) => p.ToString();
}
