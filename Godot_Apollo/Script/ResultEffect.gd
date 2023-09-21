extends Node2D
class_name ResultEffect

@export var Parfect:Array[Texture]
@export var Great:Array[Texture]
@export var Good:Array[Texture]

@export var showlife:int = 60
@export var animStep:int = 2
@export var upSpeed:int = -1

var progress:int
var result:int

func _initialize(_result:int):
	result = _result
	progress = showlife

func _process(delta):
	match result:
		RandingResult.Parfect:
			get_node("Sprite").texture = Parfect[(int)(progress / animStep) % Parfect.size()]
		RandingResult.Great:
			get_node("Sprite").texture = Great[(int)(progress / animStep) % Great.size()]
		RandingResult.Good:
			get_node("Sprite").texture = Good[(int)(progress / animStep) % Good.size()]
		RandingResult.Clash:
			get_node("Sprite").texture = null

	position.y = position.y + upSpeed
	progress -= 1
	if progress == 0:
		free()
