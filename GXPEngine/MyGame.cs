using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {
	public Pivot screen;
	public Pivot background = new Pivot();
	public Pivot pickupLayer = new Pivot();
	public Pivot ropeLayer = new Pivot();
	public Pivot gravityLayer = new Pivot();
	public Pivot collisionStuff = new Pivot();
	public MyGame() : base(1920, 1080, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		targetFps = 16;

		// Add Layers:
		screen = new Pivot();
		screen.x = 0;
		AddChild(screen);
		screen.AddChild(background);
		screen.AddChild(pickupLayer);
		screen.AddChild(ropeLayer);
		screen.AddChild(collisionStuff);
		screen.AddChild(gravityLayer);

		// Add Pickups:
		BoostPickup boostPickup = new BoostPickup(600, 200);
		pickupLayer.AddChild(boostPickup);

		RepairPickup repairPickup = new RepairPickup(600, 800);
		pickupLayer.AddChild(repairPickup);

		ShieldPickup shieldPickup = new ShieldPickup(1200, 200);
		pickupLayer.AddChild(shieldPickup);

		FuelPickup fuelPickup = new FuelPickup(1200, 800);
		pickupLayer.AddChild(fuelPickup);

        // Add Player:
        Player player = new Player();
        collisionStuff.AddChild(player);

		// Add Rope
		Rope rope = new Rope(25, player);
		ropeLayer.AddChild(rope);

		// Add Enemy:
		Enemy enemy = new Enemy();
		collisionStuff.AddChild(enemy);

		// Add mars
		planet mars = new planet("spurral.png", 1, 600, 600, gravityLayer, -1);
		collisionStuff.AddChild(mars);


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