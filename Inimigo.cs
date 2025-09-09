using Godot;
using System;

public partial class Inimigo : CharacterBody2D
{
    public int PV
    {
        get; set;
    }
    = 100;
    public override void _PhysicsProcess(double delta)
    {
        var collision = MoveAndCollide(Position * (float)delta);
        if (collision != null && collision.GetCollider() is CharacterBody2D)
        {
            GD.Print("Colidiu");
        }
    }
}
