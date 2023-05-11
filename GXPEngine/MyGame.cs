using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;
public class MyGame : Game {
	public Pivot screen;
	public Pivot background = new Pivot();
	public Pivot ropeLayer = new Pivot();
	public Pivot collisionStuff = new Pivot();
	public MyGame() : base(1920, 1080, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		targetFps = 16;

		// Add Layers:
		screen = new Pivot();
		screen.x = 0;
		AddChild(screen);
		screen.AddChild(background);
		screen.AddChild(ropeLayer);
		screen.AddChild(collisionStuff);

		// Add Player:
		Player player = new Player();
		collisionStuff.AddChild(player);

		// Add Rope that consists of 25 segments:
		Rope rope = new Rope(25, player);
		ropeLayer.AddChild(rope);

		// Add asteroid that's 1 of 3 random skins:
		Asteroid asteroid;
        float asteroidSkin = Utils.Random(0, 3);
		switch (asteroidSkin)
		{
			case 0:
                asteroid = new Asteroid("asteroid1.png");
                collisionStuff.AddChild(asteroid);
                break;
			case 1:
                asteroid = new Asteroid("asteroid2.png");
                collisionStuff.AddChild(asteroid);
                break;
			case 2:
                asteroid = new Asteroid("asteroid3.png");
                collisionStuff.AddChild(asteroid);
                break;
        }

		// Add enemy:
		Enemy enemy = new Enemy();
        collisionStuff.AddChild(enemy);
	}
	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
	void Update()
	{
	}
}