extends Node2D

@export var initLeft:int = 11
const gameScene = preload("res://Scene/Game.tscn")

var state:int
enum State
{
	Title,
	Play,
	Result,
}

func _ready():
	state = State.Title
	$Menu.Title()
	print("ready?")
	
func _start_game():
	var game = gameScene.instantiate()
	add_child(game)
	
	game.score_update_signal.connect(on_score_update)
	game.left_update_signal.connect(on_left_update)
	game.game_end_signal.connect(on_game_end)
	game._initialize(initLeft)
	
func on_score_update(score:int):
	$Menu.SetScore(score)
	
func on_left_update(left:int):
	$Menu.SetLeft(left)

func on_game_end():
	Sound.I._stop_bgm()
	Sound.I._play_bgm(BgmType.Finish)
	$Menu.Result()
	state = State.Result

func _process(delta):
	PlayerInput.left = Input.is_action_pressed("ui_left")
	PlayerInput.right = Input.is_action_pressed("ui_right")
	
	match state:
		State.Title:
			if Input.is_action_just_pressed("ui_accept"):
				$Menu.Hide()
				Sound.I._play_bgm(BgmType.Space)
				Sound.I._play_se(SeType.Ok, 0.5)
				_start_game()
				state = State.Play
		State.Play:
			pass
		State.Result:
			if Input.is_action_just_pressed("ui_accept"):
				$Menu.Title()
				state = State.Title
	
