using Godot;
using System;

public partial class Ground : StaticBody2D
{
    public AnimationPlayer animation_player;
    private Node2D player;
    public override void _Ready()
    {
        animation_player = GetNode<AnimationPlayer>("AnimationPlayer");
        player = GetNode<Node2D>("../Player");
    }
    public void OnDeathZoneBodyEntered(Node2D body)
    {
        if (body is Player player)
        {
            player.Die();
        }
    }
}
