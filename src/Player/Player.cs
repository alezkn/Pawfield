using System;
using System.Reflection.Metadata;
using Godot;

namespace Pawfield.Player;

public partial class Player : CharacterBody2D
{
    // Export attribute to make the speed adjustable in the Godot editor.
    [Export]
    private float PlayerSpeed = 350f;

    [Export]
    private AnimatedSprite2D Animation;

    private string lastAnim = "down_idle";

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    // Not using delta here because MoveAndSlide() handles frame rate independence.
    public override void _PhysicsProcess(double delta)
    {
        HandleMovement();
        HandleCollisions();
    }

    // Method to handle player movement
    // 1. Create a Vector2 with zero values to represent player direction.
    // 2. Check each input and increase/decrease the respective axis.
    // 3. Normalize the vector to ensure consistent speed in all directions.
    // 4. Set the Velocity property that will be used by MoveAndSlide.
    private void HandleMovement()
    {
        Vector2 InputDirection = Vector2.Zero;

        if (Input.IsActionPressed("move_right"))
            InputDirection.X += 1;
        if (Input.IsActionPressed("move_left"))
            InputDirection.X -= 1;
        if (Input.IsActionPressed("move_up"))
            InputDirection.Y -= 1;
        if (Input.IsActionPressed("move_down"))
            InputDirection.Y += 1;

        InputDirection = InputDirection.Normalized();

        Velocity = InputDirection * PlayerSpeed;

        MoveAndSlide();

        if (InputDirection != Vector2.Zero)
        {
            string animName = GetAnimationFromDirection(InputDirection);
            if (Animation.Animation != animName)
                Animation.Play(animName);

            lastAnim = animName; // guarda a última direção
        }
        else
        {
            // Parou de andar → mostra frame "idle" na última direção
            Animation.Animation = lastAnim;
            Animation.Frame = 0; // força o primeiro frame (idle)
            Animation.Stop();
        }
    }

    // Method to handle collisions
    // 1. Loop through all collisions detected in the last MoveAndSlide call.
    // 2. For each collision, check if the collider is a Node.
    // 3. If it is, print the name of the node to the console.
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

    private static string GetAnimationFromDirection(Vector2 dir)
    {
        // arredonda a direção pra evitar valores estranhos tipo (0.71, 0.71)
        Vector2 rounded = new(MathF.Round(dir.X), MathF.Round(dir.Y));

        if (rounded == new Vector2(1, 0))
            return "right_idle";
        if (rounded == new Vector2(-1, 0))
            return "left_idle";
        if (rounded == new Vector2(0, -1))
            return "up_idle";
        if (rounded == new Vector2(0, 1))
            return "down_idle";
        if (rounded == new Vector2(1, -1))
            return "right_up_idle";
        if (rounded == new Vector2(1, 1))
            return "right_down_idle";
        if (rounded == new Vector2(-1, -1))
            return "left_up_idle";
        if (rounded == new Vector2(-1, 1))
            return "left_down_idle";

        return "down"; // fallback
    }
}
