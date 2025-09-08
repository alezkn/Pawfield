using System;
using Godot;

public partial class NewScript : Node
{
    [Export]
    public int PropiedadManeira { get; set; }

    public override void _Ready()
    {
        GD.Print("Node is ready!");
    }

    public override void _Process(double delta)
    {
        GD.Print("_Process!");
    }
}
