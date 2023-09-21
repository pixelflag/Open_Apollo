extends Node2D
@export var idelAnim:Array[Texture]
@export var sideAnim:Array[Texture]
@export var animStep:int = 2

var frame:int
var progress:int

func _ready():
	pass # Replace with function body.

func _process(delta):
	progress += 1
	if progress % animStep == 0:
		frame += 1
		# left
		if PlayerInput.get_value() < 0:
			$Body.texture = sideAnim[frame % sideAnim.size()]
			$Body.set_flip_h(false)
		# rigth
		elif 0 < PlayerInput.get_value():
			$Body.texture = sideAnim[frame % sideAnim.size()]
			$Body.set_flip_h(true)
		# center
		else:
			$Body.texture = idelAnim[frame % idelAnim.size()]
			$Body.set_flip_h(false)


