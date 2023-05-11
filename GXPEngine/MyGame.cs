using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

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

		Player player = new Player();
		layer2.AddChild(player);

		Rope rope = new Rope(25, player);
		layer1.AddChild(rope);

		planet mars = new planet("spurral.png", 1, 200, 200, layer1);
		layer2.AddChild(mars);


		//temporarily -> will be put in a class 
		Asteroid asteroid;
        float asteroidSkin = Utils.Random(0, 3);
		switch (asteroidSkin)
		{
			case 0:
                asteroid = new Asteroid("asteroid1.png");
                AddChild(asteroid);
                break;
			case 1:
                asteroid = new Asteroid("asteroid2.png");
                AddChild(asteroid);
                break;
			case 2:
                asteroid = new Asteroid("asteroid3.png");
                AddChild(asteroid);
                break;
        }
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() {
		// Empty
	}

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}