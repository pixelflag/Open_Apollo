extends Node2D

@export var textures:Array[Texture]
@export var length:int = 7

var fontSize:int = 8
var nums:Array[Sprite2D]
	
func _ready():
	for i in range(length):
		var num = Sprite2D.new()
		add_child(num)
		num.name = "n" + str(i)
		num.position = Vector2(-i * fontSize, 0)
		nums.append(num)
	SetNum(0)
	
func SetNum(num:int):
	var leng:int = str(num).length()
	for i in range(length):
		if i == 0:
			nums[i].visible = true
			nums[i].texture = textures[num%10]
		elif i < leng:
			nums[i].visible = true
			var n:int = int(float(num) / pow(10,float(i))) % 10
			nums[i].texture = textures[n]
		else:
			nums[i].visible = false
