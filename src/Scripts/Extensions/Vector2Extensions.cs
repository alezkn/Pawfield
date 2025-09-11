using System.Collections.Generic;
using Godot;
using Pawfield.Scripts.Constants;

namespace Pawfield.Scripts.Extensions;

public static class Vector2Extensions
{
    private static readonly Dictionary<Vector2, string> directionMap = new()
    {
        { new Vector2(1, 0), Directions.Right },
        { new Vector2(-1, 0), Directions.Left },
        { new Vector2(0, -1), Directions.Up },
        { new Vector2(0, 1), Directions.Down },
        { new Vector2(1, -1), Directions.RightUp },
        { new Vector2(1, 1), Directions.RightDown },
        { new Vector2(-1, -1), Directions.LeftUp },
        { new Vector2(-1, 1), Directions.LeftDown },
    };

//This chama o input
    public static string GetDirection(this Vector2 input)
    {
        return directionMap.TryGetValue(input, out var dir) ? dir : Directions.Down;
    }
}
