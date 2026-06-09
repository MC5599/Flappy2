using Godot;
using System;

public partial class GameOver : Control
{
	private Label score_text;
	private Label high_score_text;
	public override void _Ready()
	{
		score_text = GetNode<Label>("Panel/VBoxContainer/ScoreText");
		high_score_text = GetNode<Label>("Panel/VBoxContainer/HighScoreText");

	}
	public void InitScreen(int score, int high_score)
	{
		score_text.Text = "Score: " + score.ToString();
		high_score_text.Text = "Best: " + high_score.ToString();
	}
	public void OnRetryPressed()
	{
		GetTree().ReloadCurrentScene();
	}
}
