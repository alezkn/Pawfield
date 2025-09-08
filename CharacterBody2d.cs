using Godot;

public partial class CharacterBody2d : CharacterBody2D
{
    [Export]
    public int Speed { get; set; } = 30;
    private Vector2 _position;

    public override void _Ready()
    {
        _position = Position;

        GD.Print("CharacterBody2d ready.");
    }

    public override void _PhysicsProcess(double delta)
    {
        GD.Print(Input.IsKeyPressed(Key.W));

        if (Input.IsKeyPressed(Key.W))
        {
            _position.Y -= Speed;
        }

        if (Input.IsKeyPressed(Key.S))
        {
            _position.Y += Speed;
        }

        if (Input.IsKeyPressed(Key.A))
        {
            _position.X -= Speed;
        }

        if (Input.IsKeyPressed(Key.D))
        {
            _position.X += Speed;
        }

        Position = _position;
    }
}
