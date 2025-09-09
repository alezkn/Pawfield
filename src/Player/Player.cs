using Godot;

namespace Pawfield.Player;

public partial class Player : CharacterBody2D
{
    // Export attribute to make the speed adjustable in the Godot editor.
    [Export]
    private float PlayerSpeed = 350f;

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
                GD.Print($"Colidiu com {node.Name}.");
            }
        }
    }
}
