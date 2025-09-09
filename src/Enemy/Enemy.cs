using Godot;

namespace Pawfield.Enemy;

public partial class Enemy : CharacterBody2D
{
    public int PV { get; set; } = 100;

    public override void _PhysicsProcess(double delta) { }
}
