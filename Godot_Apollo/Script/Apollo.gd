extends Node2D
class_name Apollo

@export var accele:float = 0.1
@export var friction:float = 0.98

var speed:Vector2
var progress:int = 0

var x: float:
	get:
		return position.x
	set(value):
		position.x = value

var y: float:
	get:
		return position.y
	set(value):
		position.y = value

var state:int
enum State{Fall,Randing,Launch,Clash}

func _ready():
	_boost(false, false, false)
	progress = 0
	state = State.Fall

func _process(delta):
	match state:
		State.Fall:
			_boost(PlayerInput.left, PlayerInput.right, false)
			speed.x += PlayerInput.get_value() * accele
			speed.x *= friction
		State.Randing:
			if progress == 10:
				Sound.I._play_se(SeType.Launch, 0.5)
				get_node("Body").texture = defaultSprite
				_boost(false, false, true)
				progress = 0
				state = State.Launch
			progress += 1
		State.Launch:
			speed.y -= 1
			if position.y < -128:
				queue_free()
		State.Clash:
			speed.y += 0.5
			get_node("Body").texture = _get_roling_sprite()
			if 128 < position.y :
				queue_free()
	x += speed.x
	y += speed.y

func _Randing():
	Sound.I._play_se(SeType.Randing, 0.5)
	get_node("Body").texture = randingSprite
	_boost(false, false, false)

	speed.x = 0
	speed.y = 0
	state = State.Randing

func _Clash():
	Sound.I._play_se(SeType.Clash, 0.5)
	_boost(false, false, false)
	speed.x = 0
	speed.y = -5
	state = State.Clash

# View ----------
@export var defaultSprite:Texture
@export var randingSprite:Texture
@export var rolingSprites:Array[Texture]

func _boost(left:bool, right:bool, up:bool):
	get_node("Left").visible = left
	get_node("Right").visible = right
	get_node("Up").visible = up

var rolingFrame:int = 0
var rolingCount:int = 0
var rolingStep:int = 2

func _get_roling_sprite() -> Texture:
	rolingCount += 1

	if rolingCount % rolingStep == 0:
		rolingFrame += 1

	return rolingSprites[rolingFrame % rolingSprites.size()]
