extends Sprite2D

var progress:int
var wait:int = 30

func _ready():
	SetScore(0)
	progress = 0

func _process(delta):
	progress += 1
	if progress % wait == 0:
		$PushSpace.visible = !$PushSpace.visible
		
func SetScore(score:int):
	$ScoreNum.SetNum(score)
	
func SetLeft(score:int):
	$Left.SetCount(score)

func Title():
	$Title.visible = true
	$GameOver.visible = false
	$PushSpace.visible = true
	set_process(true)
	
func Result():
	$Title.visible = false
	$GameOver.visible = true
	$PushSpace.visible = true
	set_process(true)

func Hide():
	$Title.visible = false
	$GameOver.visible = false
	$PushSpace.visible = false
	set_process(false)
