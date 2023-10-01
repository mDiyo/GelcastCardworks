using Godot;
using System;

namespace Gelcast.Example.Card
{
    public enum ExamType { Contest, Duel, Both }
    public enum ExamSkill { Power, Style }
    public partial class Exam : Resource
    {
        [Export] public ExamType examType;
        [Export] public ExamSkill examSkill;
        [Export] public int potency;
        [Export] public Texture2D texture;
        [Export] public string identifier;
    }
}