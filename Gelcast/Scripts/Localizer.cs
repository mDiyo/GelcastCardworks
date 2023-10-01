using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gelcast
{
	public class Localizer
	{
		//Use AddString and GetString please
		public static Dictionary<string, string> keyphrases = new Dictionary<string, string>();
		private static bool initialized;

		public static void Initialize()
		{
			if (!initialized)
			{
				initialized = true;
				ReadData();
			}
		}

		public static void ReadData()
		{
			//This should read in a file depending on which language you use, but I'm lazy
			AddDefaultData();
		}

		private static void AddDefaultData()
		{
			Localizer.AddString(".name", ".name is a terrible localization");
			Localizer.AddString(".description", ".description is a terrible localization");

			Localizer.AddString("card.handle.comfy.wood.name",
			"Ergonomic Wooden Handle");
			Localizer.AddString("card.handle.comfy.wood.description",
			"Ergonomic Wooden Handle Description");
		}

		//This method exists solely to allow easier debugging from the editor
		public static void AddString(string key, string value)
		{
			if (keyphrases.ContainsKey(key))
			{
				Godot.GD.PrintErr("The keyphrase " + key + " was already found in the localizer");
			}
			else
				Localizer.keyphrases.Add(key, value);
		}

		public static string GetString(string unlocalizedInput)
		{
			if (!keyphrases.ContainsKey(unlocalizedInput))
			{
				Godot.GD.PrintErr("The keyphrase " + unlocalizedInput + " was not found in the localizer");
				return String.Empty;
			}
			return keyphrases[unlocalizedInput];
		}
	}
}
