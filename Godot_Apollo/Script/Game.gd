extends Node2D

@export var resultEffectPosition:Vector2 = Vector2(0, 20)

@export var initGravity:float = 0.5
@export var gravityLevelupDistance:int = 120

@export var initSpawnTempo:int = 200
@export var minSpawnTempo:int = 30
@export var spawnSpeedLevelupDistance:int = 90

@export var initSpawnRange:int = 0
@export var maxSpawnRange:int = 120
@export var spawnRangeLevelupDistance:int = 60

@export var targetGroundY:float = 36
@export var targetGroundThreshold:float = 3

@export var ParfectWidth:float = 2
@export var GreatWidth:float = 8
@export var GoodWidth:float = 24

@export var groundLine:int = 72

@export var scoreTable:Array[int] = [1000,300,100]

var Apollos:Array[Apollo]

var spawnTempo:int = 0
var spawnRange:int = 0
var gravity:float = 0

var score:int = 0
var left:int = 0

var progress:int
var spawnProgress:int

const efs: = preload("res://Scene/ResultEffect.tscn")
const aps = preload("res://Scene/Apollo.tscn")

func _initialize(initLeft:int):
	left = initLeft
	score = 0
	
	score_update_signal.emit(score)
	left_update_signal.emit(left)

	gravity = initGravity
	spawnTempo = initSpawnTempo
	spawnRange = initSpawnRange

func _process(delta):
	for i in range(Apollos.size()-1, -1, -1):
		var apo = Apollos[i]
		var screenXEnd:int = 168
		if apo.x < -screenXEnd:
			apo.x = screenXEnd
		if apo.x > screenXEnd:
			apo.x =	-screenXEnd
		apo.y += gravity

		if (targetGroundY <= apo.y && apo.y <= targetGroundY + targetGroundThreshold):
			if (-ParfectWidth < apo.x && apo.x < ParfectWidth):
				_Rand_target(apo, RandingResult.Parfect)
			elif (-GreatWidth < apo.x && apo.x < GreatWidth):
				_Rand_target(apo, RandingResult.Great)
			elif (-GoodWidth < apo.x && apo.x < GoodWidth):
				_Rand_target(apo, RandingResult.Good)
		elif (groundLine < apo.y ):
			_Clash(apo)

	progress += 1
	spawnProgress -= 1

	if spawnProgress <= 0:
		_Spawn_apollo()
		spawnProgress = spawnTempo
		
	if progress % spawnSpeedLevelupDistance == 0:
		spawnTempo = max(minSpawnTempo, spawnTempo - 10)
	if progress % spawnRangeLevelupDistance == 0:
		spawnRange = min(maxSpawnRange, spawnRange + 10)
	if progress % gravityLevelupDistance == 0:
		gravity += 0.1

func _Spawn_apollo():
	var rng = RandomNumberGenerator.new()
	var apo = aps.instantiate()
	get_tree().get_root().get_node("Main").add_child(apo)
	
	apo.x  = rng.randf_range(-spawnRange, spawnRange)
	apo.y = -96
	Apollos.append(apo)

func _Rand_target(apo:Apollo, result:int):
	apo.y = targetGroundY
	apo._Randing()
	Apollos.erase(apo)
	
	var ef:ResultEffect = efs.instantiate() as ResultEffect
	get_tree().get_root().get_node("Main").add_child(ef)
	ef._initialize(result)
	ef.position = resultEffectPosition
	
	match result:
		RandingResult.Parfect:
			score += scoreTable[0]
		RandingResult.Great:
			score +=  scoreTable[1]
		RandingResult.Good:
			score +=  scoreTable[2]
		RandingResult.Clash:
			pass
	score_update_signal.emit(score)

func _Clash(apo:Apollo):
	apo._Clash()
	Apollos.erase(apo)
	left -= 1
	left_update_signal.emit(left)
	if left == 0:
		_Game_end()

func _Game_end():
	for i in range(Apollos.size()-1, -1, -1):
		Apollos[i]._Clash()
	game_end_signal.emit()
	queue_free()

signal score_update_signal(score:int)
signal left_update_signal(left:int)
signal game_end_signal()
