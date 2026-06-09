using Godot;
using System;

public partial class Blocks : CharacterBody2D
{
	[Signal] public delegate void ScoredEventHandler();
	private Player player;
	private int speed = -215;
	public override void _PhysicsProcess(double delta)
	{
		Velocity = new Vector2(speed, 0);
		MoveAndSlide();
		if (GlobalPosition.X < -200)
		{
			QueueFree();
		}
	}
	public void OnBlockBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			player.Die();
		}
	}
	public void OnScoreAreaBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			player.ScorePoint();
		}
	}
	public void Stop()
	{
		speed = 0;
	}
}
