using Godot;
using Pawfield.Scripts.Constants;
using Pawfield.Scripts.Extensions;

namespace Pawfield.Player;

public partial class Player : CharacterBody2D
{
    [Export]
    private float PlayerSpeed = 350f;

    [Export]
    private AnimatedSprite2D PlayerAnimation;
    private string PlayerLookingAt = Directions.Down;
    private string PlayerState = States.Idling;

    public override void _PhysicsProcess(double delta)
    {
        HandleMovement();
        HandleCollisions();
        HandleAnimation();
    }

    private void HandleMovement()
    {
        Vector2 inputDirection = Vector2.Zero;

        if (Input.IsActionPressed("move_right"))
            inputDirection.X += 1;
        if (Input.IsActionPressed("move_left"))
            inputDirection.X -= 1;
        if (Input.IsActionPressed("move_up"))
            inputDirection.Y -= 1;
        if (Input.IsActionPressed("move_down"))
            inputDirection.Y += 1;

        Velocity = inputDirection.Normalized() * PlayerSpeed;
        MoveAndSlide();

        if (inputDirection == Vector2.Zero)
        {
            PlayerState = States.Idling;
            return;
        }

        PlayerLookingAt = inputDirection.GetDirection();
        PlayerState = States.Walking;
    }

    private void HandleCollisions()
    {
        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            if (GetSlideCollision(i).GetCollider() is Node node)
            {
                GD.Print($"Collided with {node.Name}.");
            }
        }
    }

    private void HandleAnimation()
    {
        PlayerAnimation.Play($"{PlayerLookingAt}{PlayerState}");
    }
}
