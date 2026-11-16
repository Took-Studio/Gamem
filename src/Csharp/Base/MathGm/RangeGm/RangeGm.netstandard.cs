using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Gamem;

/// <summary>
/// Represents a 2D integer range or coordinate pair.
/// </summary>
public readonly partial struct RangeGm
{
    /// <summary>
    /// The X-coordinate or start value.
    /// </summary>
    public int X { get; }
    /// <summary>
    /// The Y-coordinate or start value.
    /// </summary>
    public int Y { get; }
    /// <summary>
    /// Checks if the range is valid: X &lt;= Y returns true, if not returns false
    /// </summary>
    public readonly bool IsValid
    {
        get
        {
            return IsValidCheck(X, Y);
        }
    }
    /// <summary>
    /// An empty RangeGm (RangeGm with X = 0; Y = 0)
    /// </summary>
    public static RangeGm Empty => new(0, 0);
    /// <summary>
    /// Initializes a new instance of the <see cref="RangeGm"/> struct.
    /// </summary>
    /// <param name="x">The initial X-coordinate or start value.</param>
    /// <param name="y">The initial Y-coordinate or end value.</param>
    public RangeGm(int x, int y)
    {
        if (x > y)
            throw new ArgumentException("X cannot be greater than Y for a valid range.", nameof(x));
        X = x;
        Y = y;
    }
    /// <summary>
    /// IsValid check method
    /// </summary>
    public static bool IsValidCheck(int X, int Y)
    {
        return X <= Y;
    }
    /// <summary>
    /// Returns a string representation of the current range.
    /// </summary>
    /// <returns>A string formatted as "(X, Y)".</returns>
    public override string ToString() => $"({X}, {Y})";
    /// <summary>
    /// Returns the smaller of two 32-bit signed integers.
    /// </summary>
    /// <returns>The smaller of the two parameters.</returns>
    public int Min() => Math.Min(X, Y);
    /// <summary>
    /// Returns the larger of two 32-bit signed integers.
    /// </summary>
    /// <returns>The larger of the two parameters.</returns>
    public int Max() => Math.Max(X, Y);
    /// <summary>
    /// Returns the length of the range.
    /// </summary>
    /// <returns>The range's length</returns>
    public int Length() => Y - X;
    /// <summary>
    /// Returns the center of the range.
    /// </summary>
    /// <returns>The range's center</returns>
    public int Center() => (X + Y) / 2;
    /// <summary>
    /// Checks if the range contains the specified value.
    /// </summary>
    /// <param name="Value">Value to check</param>
    /// <returns>True if the range contains the value, if not false</returns>
    /// <example>
    /// <code>
    /// RangeGm range = new(0, 100);
    /// Console.WriteLine(range.Contains(35)); // true
    /// Console.WriteLine(range.Contains(101)); // false
    /// </code>
    /// </example>
    public bool Contains(int Value) => Value >= X && Value <= Y;
    /// <summary>
    /// Checks if the range contains the specified range.
    /// </summary>
    /// <param name="Other">Value to check</param>
    /// <returns>True if the range contains the value, if not false</returns>
    /// <example>
    /// <code>
    /// RangeGm range = new(0, 100);
    /// Console.WriteLine(range.Contains(35)); // true
    /// Console.WriteLine(range.Contains(101)); // false
    /// </code>
    /// </example>
    public bool Contains(RangeGm Other) => Other.X >= X && Other.Y <= Y;
    /// <summary>
    /// Checks if the range overlaps with the specified range.
    /// </summary>
    /// <param name="Other">Range to check</param>
    /// <returns>True if the ranges overlap, if not false</returns>
    public bool Overlaps(RangeGm Other) => X <= Other.Y && Other.X <= Y;
    /// <summary>
    /// Returns the union of two ranges.
    /// </summary>
    /// <param name="range1">The first range</param>
    /// <param name="range2">The second range</param>
    /// <returns>The union of two ranges</returns>
    public static RangeGm Union(RangeGm range1, RangeGm range2) => new(Math.Min(range1.Min(), range2.Min()), Math.Max(range1.Max(), range2.Max()));
    /// <summary>
    /// Expands the range to include the specified value.
    /// </summary>
    /// <param name="Value">Value to include</param>
    /// <returns>The expanded range</returns>
    public RangeGm Expand(int Value)
    {
        if (Value >= Y)
            return new(X, Value);
        else if (Value <= X)
            return new(Value, Y);
        return this;
    }
    /// <summary>
    /// Returns the intersection of two ranges.
    /// </summary>
    /// <param name="other">The other range</param>
    /// <returns>The intersection of two ranges</returns>
    public RangeGm Intersect(RangeGm other)
    {
        if (Overlaps(other))
        {
            return new RangeGm(Math.Max(X, other.X), Math.Min(Y, other.Y));
        }
        return Empty;
    }
    /// <summary>
    /// Normalizes the range so that X is less than Y.
    /// </summary>
    /// <returns>The normalized range</returns>
    public RangeGm Normalize()
    {
        if (X > Y)
            return new(Y, X);
        return this;
    }
    /// <summary>
    /// Clamps the specified value between the range's bounds.
    /// </summary>
    /// <param name="num">Value to clamp</param>
    /// <returns>The clamped value</returns>
    public int Clamp(int num)
    {
        if (num < X)
            return X;
        if (num > Y)
            return Y;
        return num;
    }
    /// <summary>
    /// Returns a random value between the range's bounds.
    /// </summary>
    /// <returns>A random value</returns>
    public int GetRandomValue() => RandomGm.RandomSharedPolyfill.Shared.Next(X, Y);
    /// <summary>
    /// Returns the distance between two ranges.
    /// </summary>
    /// <param name="other">The other range</param>
    /// <returns>The distance</returns>
    public int DistanceTo(RangeGm other)
    {
        if (Overlaps(other))
            return 0;
        if (Y < other.X)
            return other.X - Y;
        return X - other.Y;
    }
    /// <summary>
    /// Wraps the specified value within the range.
    /// </summary>
    /// <param name="value">Value to wrap</param>
    /// <returns>The wrapped value</returns>
    public int Wrap(int value)
    {
        if (X == Y)
            return X;

        int length = Y - X;
        int offset = (value - X) % length;
        if (offset < 0)
            offset += length;

        return X + offset;
    }
    /// <summary>
    /// Adds one range to specified number.
    /// </summary>
    public static RangeGm operator +(RangeGm range, int num)
    {
        return new RangeGm(
            range.X + num,
            range.Y + num
        );
    }
    /// <inheritdoc cref="operator +(RangeGm, int)"/>
    public static RangeGm operator +(int num, RangeGm range) => range + num;
    /// <summary>
    /// Subtracts the specified number from one range.
    /// </summary>
    public static RangeGm operator -(RangeGm range, int num)
    {
        return new RangeGm(
            range.X - num,
            range.Y - num
        );
    }
    /// <summary>
    /// Subtracts one range from the specified number.
    /// </summary>
    public static RangeGm operator -(int num, RangeGm range)
    {
        int var1 = num - range.X;
        int var2 = num - range.Y;

        return new RangeGm(
            Math.Min(var1, var2),
            Math.Max(var1, var2)
        );
    }
    /// <summary>
    /// Multiplies one range by the specified number.
    /// </summary>
    public static RangeGm operator *(RangeGm range, int num)
    {
        int var1 = range.X * num;
        int var2 = range.Y * num;

        return new RangeGm(
            Math.Min(var1, var2),
            Math.Max(var1, var2)
        );
    }
    /// <inheritdoc cref="operator *(RangeGm, int)"/>
    public static RangeGm operator *(int num, RangeGm range) => range * num;
    /// <summary>
    /// Divides one range by the specified number.
    /// </summary>
    public static RangeGm operator /(RangeGm range, int num)
    {
        int var1 = range.X / num;
        int var2 = range.Y / num;

        return new RangeGm(
            Math.Min(var1, var2),
            Math.Max(var1, var2)
        );
    }
    /// <inheritdoc cref="operator /(RangeGm, int)"/>
    public static RangeGm operator /(int num, RangeGm range)
    {
        int var1 = num / range.X;
        int var2 = num / range.Y;

        return new RangeGm(
            Math.Min(var1, var2),
            Math.Max(var1, var2)
        );
    }
}