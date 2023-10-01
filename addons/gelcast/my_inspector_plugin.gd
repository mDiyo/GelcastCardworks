# my_inspector_plugin.gd
extends EditorInspectorPlugin

var RandomIntEditor = preload("res://addons/gelcast/random_int_editor.gd")

func _can_handle(object):
	# We support all objects in this example.
	return true

func _parse_property(object, type, path, hint, hint_text, usage, wide):
	# We handle properties of type integer.
	if type == TYPE_INT:
		# Create an instance of the custom property editor and register
		# it to a specific property path.
		add_property_editor(path, RandomIntEditor.new())
		# Inform the editor to remove the default property editor for
		# this property type.
		return true
	else:
		return false
