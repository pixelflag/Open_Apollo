extends Node2D

@export var max = 30
@export var colmun = 10
@export var size:Vector2 = Vector2(8,8)

var icons:Array[Node2D]

func _ready():
	const lis = preload("res://Scene/LeftIcon.tscn")
	for i in range(max):
		var li = lis.instantiate()
		add_child(li)
		var xx:int = i % colmun
		var yy:int = i / colmun
		li.position = Vector2(xx * size.x, yy * size.y)
		li.visible = false
		icons.append(li)

func SetCount(count:int):
	count = clamp(count, 0, icons.size())
	for i in range(icons.size()):
		icons[i].visible = i < count
