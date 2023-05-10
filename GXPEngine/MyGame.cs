using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;
public class MyGame : Game {
	public Pivot screen;
	public Pivot layer1 = new Pivot();
	public Pivot layer2 = new Pivot();
	public Pivot layer3 = new Pivot();
	public MyGame() : base(1920, 1080, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		targetFps = 16;

		// Add Layers:
		screen = new Pivot();
		screen.x = 0;
		AddChild(screen);
		screen.AddChild(layer1);
		screen.AddChild(layer2);
		screen.AddChild(layer3);

		// Add Player:
		Player player = new Player();
		layer3.AddChild(player);

		// Add Rope that consists of 25 segments:
		Rope rope = new Rope(25, player);
		layer2.AddChild(rope);

		// Add asteroid that's 1 of 3 random skins:
		Asteroid asteroid;
        float asteroidSkin = Utils.Random(0, 3);
		switch (asteroidSkin)
		{
			case 0:
                asteroid = new Asteroid("asteroid1.png");
                layer1.AddChild(asteroid);
                break;
			case 1:
                asteroid = new Asteroid("asteroid2.png");
                layer1.AddChild(asteroid);
                break;
			case 2:
                asteroid = new Asteroid("asteroid3.png");
                layer1.AddChild(asteroid);
                break;
        }

		// Add enemy:
		Enemy enemy = new Enemy();
		layer3.AddChild(enemy);
	}
	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
	void Update()
	{
	}
}