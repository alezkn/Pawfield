using Godot;

//Método pra fzr o bicho ter física com a plataforma  
public partial class Player : CharacterBody2D
{
    private float _runSpeed = 350;
    private float _jumpSpeed = -1000;
    private float _gravity = 2500;

    private void GetInput()
    {
        var velocity = Velocity;
        velocity.X = 0;

        var right = Input.IsActionPressed("ui_right");
        var left = Input.IsActionPressed("ui_left");
        var jump = Input.IsActionPressed("ui_select");

        if (IsOnFloor() && jump)
            velocity.Y = _jumpSpeed;
        if (right)
            velocity.X += _runSpeed;
        if (left)
            velocity.X -= _runSpeed;

        Velocity = velocity;
    }

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;
        velocity.Y += _gravity * (float)delta;
        Velocity = velocity;
        GetInput();
        MoveAndSlide();
        for (var i = 0; i < GetSlideCollisionCount(); i++)
        {
            var collision = GetSlideCollision(i);

            if (collision.GetCollider() is CharacterBody2D)
            {
                GD.Print("Colidiu");
                collision.GetCollider().Free();
            }
        }

    }
}
