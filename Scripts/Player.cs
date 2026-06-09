using Godot;
using System;

public partial class Player : RigidBody2D
{
	[Signal] public delegate void GameStartedEventHandler();
	[Signal] public delegate void DiedEventHandler();
	[Signal] public delegate void ScoredEventHandler();
	private AnimationPlayer animation_player;
	private AudioStreamPlayer flap_sound;
	private AudioStreamPlayer hit_sound;
	private AudioStreamPlayer score_sound;
	private Ground ground;
	private bool started = false;
	private bool is_alive = true;
	private float flap_force = -340f;
	private float angular_force_down = 5f;
	private float angular_force_up = -8f;
	private float max_rotation_down = 90f;
	private float max_rotation_up = -30f;
	public override void _Ready()
	{
		animation_player = GetNode<AnimationPlayer>("AnimationPlayer");
		flap_sound = GetNode<AudioStreamPlayer>("FlapSound");
		hit_sound = GetNode<AudioStreamPlayer>("HitSound");
		score_sound = GetNode<AudioStreamPlayer>("ScoreSound");
		ground = GetNode<Ground>("../Ground");
	}
    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("flap") && is_alive)
		{
			if (!started)
			{
				StartGame();
			}
			Flap();
		}
		if (RotationDegrees <= max_rotation_up)
		{
			RotationDegrees = max_rotation_up;
			AngularVelocity = 0;
		}
		if (LinearVelocity.Y > 0)
		{
			if (RotationDegrees <= max_rotation_down)
			{
				AngularVelocity = angular_force_down;
			}
			else
			{
				AngularVelocity = 0;
			}
		}
    }
	public void StartGame()
	{
		started = true;
		GravityScale = 1;
		EmitSignal(SignalName.GameStarted);
	}
	public void Flap()
	{
		LinearVelocity = new Vector2(0, flap_force);
		AngularVelocity = angular_force_up;
		animation_player.Play("flap");
		flap_sound.Play();
	}
	public void Die()
	{
		if (is_alive)
		{
			is_alive = false;
			hit_sound.Play();
			EmitSignal(SignalName.Died);
		}
	}
	public void ScorePoint()
	{
		if (is_alive)
		{
			EmitSignal(SignalName.Scored);
			score_sound.Play();
		}
	}
}
