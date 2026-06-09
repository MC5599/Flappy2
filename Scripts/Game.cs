using Godot;
using System;

public partial class Game : Node2D
{
	private BlockSpawner blockSpawner;
	private Hud hud;
	private Ground ground;
	private Player player;
	private int score = 0;
	private int high_score = 0;
	private string save_file_path = "user://save_game.dat";

	public override void _Ready()
	{
		blockSpawner = GetNode<BlockSpawner>("BlockSpawner");
		hud = GetNode<Hud>("Hud");
		ground = GetNode<Ground>("Ground");
		player = GetNode<Player>("Player");
		player.Died += OnDied;
		player.Scored += OnScored;
		player.GameStarted += _on_game_started;
		hud.set_score(score);
		Load_high_score();
	}
	public void _on_game_started()
	{
		blockSpawner.Start();
		hud.HideStartScreen();
	}
	public void OnDied()
	{
		blockSpawner.Stop();
		if (score > high_score)
		{
			high_score = score;
			SaveHighScore();
		}
		hud.ShowGameOverScreen(score, high_score);
		ground.animation_player.Pause();
		GetTree().CallGroup("blocks", "Stop");
	}
	public void OnScored()
	{
		score++;
		hud.set_score(score);
	}
	public void SaveHighScore()
	{
		var save_data = FileAccess.Open(save_file_path, FileAccess.ModeFlags.Write);
		save_data.StoreVar(high_score);
		save_data.Close();
	}
	public void Load_high_score()
	{
		if (FileAccess.FileExists(save_file_path))
		{
			var save_data = FileAccess.Open(save_file_path, FileAccess.ModeFlags.Read);
			high_score = (int)save_data.GetVar();
			save_data.Close();
		}
	}
}
