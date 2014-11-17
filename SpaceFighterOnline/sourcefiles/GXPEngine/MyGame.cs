using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{
    Sprite _background;
    Level _level;
	public MyGame () : base(1280, 960, true)
	{
        _background = new Sprite("sky.png");
        AddChild(_background);

        string level = "Level.tmx";
        _level = new Level(level);
        AddChild(_level);

        game.Remove(this);
        game.Add(this);
	}
	
	void Update () {

	}

	static void Main() {
		new MyGame().Start();
	}
}

