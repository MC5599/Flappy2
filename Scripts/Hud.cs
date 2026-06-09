using Godot;
using System;

public partial class Hud : CanvasLayer
{
	private Label ScoreText;
	private TextureRect start_screen;
	private Control game_over;
	private GameOver game_over_script;
	public override void _Ready()
	{
		ScoreText = GetNode<Label>("ScoreText");
		start_screen = GetNode<TextureRect>("StartMenu");
		game_over = GetNode<Control>("GameOver");
		game_over_script = GetNode<GameOver>("GameOver");
		start_screen.Visible = true;
		game_over.Visible = false;
	}
	public void set_score(int new_score)
	{
		ScoreText.Text = new_score.ToString();
	}
	public void HideStartScreen()
	{
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(start_screen, "modulate:a", 0, 0.5f);
	}
	public void ShowGameOverScreen(int score, int high_score)
	{
		game_over_script.InitScreen(score, high_score);
		game_over.Visible = true;
	}
}
