namespace AdventOfCode.Helpers;

public struct Point
{
    public long Y { get; init; }
    public long X { get; init; }

    public Point(long y, long x)
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

    private bool Equals(Point other)
    {
        return Y.Equals(other.Y) && X.Equals(other.X);
    }

    public override bool Equals(object? obj)
    {
        return obj is Point other && Equals(other);
    }

    public static Point operator +(Point a, Point b)
    {
        return new Point(a.Y + b.Y, a.X + b.X);
    }

    public static Point operator -(Point a, Point b)
    {
        return new Point(a.Y - b.Y, a.X - b.X);
    }

    public static bool operator !=(Point a, Point b)
    {
        return !a.Equals(b);
    }

    public static bool operator ==(Point a, Point b)
    {
        return a.Equals(b);
    }

    public static implicit operator string(Point p) => p.ToString();
}
