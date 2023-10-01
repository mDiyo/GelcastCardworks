# plugin.gd
@tool
extends EditorPlugin

var plugin


func _enter_tree():
	print("hoi, im gelcast 0.1")
	plugin = preload("res://addons/gelcast/my_inspector_plugin.gd").new()
	add_inspector_plugin(plugin)


func _exit_tree():
	remove_inspector_plugin(plugin)
