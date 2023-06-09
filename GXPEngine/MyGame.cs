using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;
using System.Drawing.Imaging;

public class MyGame : Game {
	public Pivot screen;
	public Pivot backgroundLayer = new Pivot();
    public Pivot buttonLayer = new Pivot();
	public Pivot pickupLayer = new Pivot();
	public Pivot ropeLayer = new Pivot();
	public Pivot gravityLayer = new Pivot();
	public Pivot collisionStuff = new Pivot();
    public Pivot hudLayer = new Pivot();
    public Pivot hudImageLayer = new Pivot();
	SoundChannel backgroundSong;
    Sprite hud;
    public int currentLevel;
	public MyGame() : base(1920, 1080, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		targetFps = 16;

		// Add Layers:
		screen = new Pivot();
		screen.x = 0;
		AddChild(screen);
        screen.AddChild(backgroundLayer);
        screen.AddChild(buttonLayer);
		screen.AddChild(pickupLayer);
		screen.AddChild(ropeLayer);
		screen.AddChild(collisionStuff);
		screen.AddChild(gravityLayer);
        screen.AddChild(hudLayer);
        screen.AddChild(hudImageLayer);

        // Add home screen:
        MainMenu mainMenu = new MainMenu("HomeScreen");
        backgroundLayer.AddChild(mainMenu);

		// Add song:
		backgroundSong = new Sound("background_song.wav", true).Play();
	}

    public void UpdateLayers(float posX, float posY)
    {
        //Update background:
        

        //Update hud:
        hud.x = posX;
        hud.y = posY;
    }
	public void LevelOver(string menuScreen)
	{
		DeleteLayers();
        if (menuScreen == "WinScreen" && currentLevel == 3)
        {
            MakeMenu("LastWinScreen");
        }
        else MakeMenu(menuScreen);
    }

	public void DeleteLayers()
	{
        //delete background:
        List<GameObject> children = backgroundLayer.GetChildren();
        for (int i = children.Count - 1; i >= 0; i--) children[i].LateDestroy();

        //delete buttons:
        children = buttonLayer.GetChildren();
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

        //delete hud layer
        children = hudLayer.GetChildren();
        for (int i = children.Count - 1; i >= 0; i--) children[i].LateDestroy();

        //delete hud image layer
        children = hudImageLayer.GetChildren();
        for (int i = children.Count - 1; i >= 0; i--) children[i].LateDestroy();
    }

	public void MakeMenu(string menuScreen)
	{
		MainMenu mainMenu = new MainMenu(menuScreen);
        backgroundLayer.LateAddChild(mainMenu);
    }

	public void StartLevel(int level)
	{
        switch (level)
        {
            case (1):
                Level loaded = new Level("level1.tmx");
                currentLevel = 1;
                break;
            case (2):
                Level loaded1 = new Level("level2.tmx");
                currentLevel = 2;
                break;
            case (3):
                Level loaded2 = new Level("level3.tmx");
                currentLevel = 3;
                break;
            case (4):
                Player player = new Player(500, 500);
                Rope rope = new Rope(25, player);
                Enemy enemy = new Enemy(500, 500 + 30);
                Asteroid textW = new Asteroid("PressWTut.png", width/2, height + 100, 0, -2);
                Asteroid textAD = new Asteroid("PressAandDTut.png", width / 2 - 200, -500, 0.3f, 2);
                Asteroid textHit = new Asteroid("ArrowTut.png", width + 300, -330, -0.8f, 0.8f);
                Asteroid astroid = new Asteroid("Asteroid1.png", width + 500, -500, -0.8f, 0.8f);
                Background background = new Background("background_lvl1.png",player);
                background.SetOrigin(width / 2, height / 2);
                background.SetScaleXY(1.1f);
                background.SetXY(width / 2, height / 2);
                backgroundLayer.AddChild(background);
                collisionStuff.AddChild(textHit);
                collisionStuff.AddChild(astroid);
                collisionStuff.AddChild(textW);
                collisionStuff.AddChild(textAD);
                collisionStuff.AddChild(player);
                ropeLayer.AddChild(rope);
                collisionStuff.AddChild(enemy);
                level = 0;
                break;
        }

        // ADD HUD:
        hud = HUD.DrawHUD();
        hud.SetOrigin(width / 2, height/2);
        hudImageLayer.AddChild(hud);
        EasyDraw health = HUD.DrawHealth();
        hudLayer.AddChild(health);
        EasyDraw fuel = HUD.DrawFuel();
        hudLayer.AddChild(fuel);

        //Add Background:
        
        

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