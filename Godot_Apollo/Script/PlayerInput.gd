class_name PlayerInput
static var left:bool
static var right:bool

static func get_value():
	var value = 0
	if left:
		value -=1
	if right:
		value +=1
	return value
