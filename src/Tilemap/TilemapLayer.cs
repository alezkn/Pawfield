using Godot;
using System;

public partial class TilemapLayer : TileMapLayer
{
    public override void _Ready()
    {
        var filledTiles = GetUsedCells();

        // This was supposed to detect the tiles that are empty on the tilemap
        // and paint them automatically for us w the empty sprite
        // but idk why it's not working 

        foreach (Vector2I filledTile in filledTiles)
        {
            var neighboringTiles = GetSurroundingCells(filledTile);

            foreach (Vector2I neighbor in neighboringTiles)
            {
                // If the tile value is empty
                if (GetCellSourceId(neighbor) == -1)
                {
                    SetCell(neighbor, 0, Vector2I.Zero);
                    // Set the source to 0
                }
            }
        }
    }
}
