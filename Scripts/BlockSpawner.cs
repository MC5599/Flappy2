using Godot;
using System;

public partial class BlockSpawner : Node2D
{
	private Timer spawn_timer;
	private int max_y_position = -150;
	private int min_y_position = 50;
	public override void _Ready()
	{
		spawn_timer = GetNode<Timer>("SpawnTimer");
	}
	public void _on_spawn_timer_timeout()
	{
		PackedScene block_scene = GD.Load<PackedScene>("res://Scenes/blocks.tscn");
		Blocks block = block_scene.Instantiate<Blocks>();
		AddChild(block);

		block.Position = new Vector2(0, (float)GD.RandRange(max_y_position, min_y_position));
	}
	public void Start()
	{
		spawn_timer.Start();
	}
	public void Stop()
	{
		spawn_timer.Stop();
	}
}
