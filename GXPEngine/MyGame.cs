using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;

public class MyGame : Game {
	public Pivot screen;
	public Pivot backgroundLayer = new Pivot();
	public Pivot pickupLayer = new Pivot();
	public Pivot ropeLayer = new Pivot();
	public Pivot gravityLayer = new Pivot();
	public Pivot collisionStuff = new Pivot();
	SoundChannel backgroundSong;
	public MyGame() : base(1920, 1080, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		targetFps = 16;

		// Add Layers:
		screen = new Pivot();
		screen.x = 0;
		AddChild(screen);
		screen.AddChild(backgroundLayer);
		screen.AddChild(pickupLayer);
		screen.AddChild(ropeLayer);
		screen.AddChild(collisionStuff);
		screen.AddChild(gravityLayer);

        // Add home screen:
        MainMenu mainMenu = new MainMenu("HomeScreen");
        backgroundLayer.AddChild(mainMenu);

		// Add song:
		backgroundSong = new Sound("background_song.wav", true).Play();
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() {
		// Empty
	}

	public void LevelOver(string menuScreen)
	{
		DeleteLayers();
		MakeMenu(menuScreen);
    }

	void DeleteLayers()
	{
        //delete background:
        List<GameObject> children = backgroundLayer.GetChildren();
        for (int i = children.Count - 1; i >= 0; i--) children[i].LateDestroy();

        //delete rope:
        children = ropeLayer.GetChildren();
        for (int i = children.Count - 1; i >= 0; i--) children[i].LateDestroy();

        //delete collision stuff:
        children = collisionStuff.GetChildren();
        for (int i = children.Count - 1; i >= 0; i--) children[i].LateDestroy();

        //delete pickup layer:
        children = pickupLayer.GetChildren();
        for (int i = children.Count - 1; i >= 0; i--) children[i].LateDestroy();

        //delete gravity layer:
        children = gravityLayer.GetChildren();
        for (int i = children.Count - 1; i >= 0; i--) children[i].LateDestroy();
    }

	public void MakeMenu(string menuScreen)
	{
		MainMenu mainMenu = new MainMenu(menuScreen);
		backgroundLayer.LateAddChild(mainMenu);
	}

    

	public void StartLevel(int level)
	{
        //delete background:
        List<GameObject> children = backgroundLayer.GetChildren();
        for (int i = children.Count - 1; i >= 0; i--) children[i].LateDestroy();


        //Add Background:
        Background background = new Background("background_lvl1.png");
        backgroundLayer.LateAddChild(background);

        switch (level)
        {
            case (1):
                Level loaded = new Level("level1.tmx");
                
                break;
            case (2):
                Level loaded1 = new Level("level2.tmx");
                break;
            case (3):
                Level loaded2 = new Level("level3.tmx");
                break;
        }
        
        /*
        // Add Pickups:
        BoostPickup boostPickup = new BoostPickup(600, 200);
        pickupLayer.LateAddChild(boostPickup);

        RepairPickup repairPickup = new RepairPickup(600, 800);
        pickupLayer.LateAddChild(repairPickup);

        ShieldPickup shieldPickup = new ShieldPickup(1200, 200);
        pickupLayer.LateAddChild(shieldPickup);

        FuelPickup fuelPickup = new FuelPickup(1200, 800);
        pickupLayer.LateAddChild(fuelPickup);

        // Add Player:
        Player player = new Player();
        collisionStuff.LateAddChild(player);

        // Add Rope
        Rope rope = new Rope(25, player);
        ropeLayer.LateAddChild(rope);

        // Add Enemy:
        Enemy enemy = new Enemy();
        collisionStuff.LateAddChild(enemy);

        // Add mars
        Planet mars = new Planet("spurral.png", 1, 600, 600, gravityLayer, 1);
        collisionStuff.LateAddChild(mars);


        //temporarily -> will be put in a class 
        Asteroid asteroid;
        float asteroidSkin = Utils.Random(0, 3);
        switch (asteroidSkin)
        {
            case 0:
                asteroid = new Asteroid("asteroid1.png");
                collisionStuff.LateAddChild(asteroid);
                break;
            case 1:
                asteroid = new Asteroid("asteroid2.png");
                collisionStuff.LateAddChild(asteroid);
                break;
            case 2:
                asteroid = new Asteroid("asteroid3.png");
                collisionStuff.LateAddChild(asteroid);
                break;
        }
        */
    }

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}