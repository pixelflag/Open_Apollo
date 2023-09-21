extends Node
class_name Sound

static var I:Sound

@export var bgmVolume:float = 0.5
@export var seVolume:float = 1.0

var s_ok = preload("res://sound/ok.wav")
var s_launch = preload("res://sound/launch.wav")
var s_clash = preload("res://sound/clash.wav")
var s_randing = preload("res://sound/randing.wav")

var bgm_finish = preload("res://sound/finish.wav")
var bgm_space = preload("res://sound/space.wav")

func _ready():
	I = self
	$Bgm.volume_db = bgmVolume

func _play_bgm(type:int):
	match type:
		BgmType.Space:
			$Bgm.stream  = bgm_space
		BgmType.Finish:
			$Bgm.stream  = bgm_finish
	$SoundEffect.volume_db = bgmVolume
	$Bgm.play()

func _stop_bgm():
	$Bgm.stop()

func _play_se(type:int, volume:float):
	match type:
		SeType.Ok:
			$SoundEffect.stream  = s_ok
		SeType.Launch:
			$SoundEffect.stream  = s_launch
		SeType.Clash:
			$SoundEffect.stream  = s_clash
		SeType.Randing:
			$SoundEffect.stream  = s_randing
	$SoundEffect.volume_db = volume * seVolume
	$SoundEffect.play()
