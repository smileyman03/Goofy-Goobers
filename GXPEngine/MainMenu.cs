using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
public class MainMenu : GameObject
{
    private String currentMenu = "HomeScreen";
    private Sprite menu;
    public MainMenu(string menuScreen)
    {
        currentMenu = menuScreen;
        CheckWhichScreen();
    }

    void Update()
    {
        Console.WriteLine("X: " + Input.mouseX + " Y: " + Input.mouseY);
        MyGame myGame = (MyGame)game;

        switch (currentMenu)
        {
            case "HomeScreen":
                //Clicked select level:
                if (Input.GetMouseButtonDown(0) && Input.mouseX >= 760 && Input.mouseX <= 1170 && Input.mouseY >= 370 && Input.mouseY <= 485)
                {
                    myGame.MakeMenu("SelectLevel");
                    this.Destroy();
                }

                //Clicked quit:
                if (Input.GetMouseButtonDown(0) && Input.mouseX >= 760 && Input.mouseX <= 1170 && Input.mouseY >= 805 && Input.mouseY <= 930)
                {
                    myGame.Destroy();
                }
                break;
            case "GameOver":
                //clicked restart:
                if (Input.GetMouseButtonDown(0) && Input.mouseX >= 760 && Input.mouseX <= 1170 && Input.mouseY >= 450 && Input.mouseY <= 575)
                {
                    myGame.StartLevel();
                    this.Destroy();
                }

                //clciked back:
                if (Input.GetMouseButtonDown(0) && Input.mouseX >= 760 && Input.mouseX <= 1170 && Input.mouseY >= 660 && Input.mouseY <= 785)
                {
                    myGame.MakeMenu("HomeScreen");
                    this.Destroy();
                }
                break;
            case "SelectLevel":
                //clicked level 1:
                if (Input.GetMouseButtonDown(0) && Input.mouseX >= 150 && Input.mouseX <= 490 && Input.mouseY >= 370 && Input.mouseY <= 620)
                {
                    myGame.StartLevel();
                    this.Destroy();
                }

                //clicked back:
                if (Input.GetMouseButtonDown(0) && Input.mouseX >= 840 && Input.mouseX <= 1075 && Input.mouseY >= 880 && Input.mouseY <= 980)
                {
                    myGame.MakeMenu("HomeScreen");
                    this.Destroy();
                }
                break;
        }
    }

    void CheckWhichScreen()
    {
        MyGame myGame = (MyGame)game;
        switch (currentMenu)
        {
            case "HomeScreen":
                menu = new Sprite("HomeScreen.png");
                myGame.backgroundLayer.LateAddChild(menu);
                break;
            case "GameOver":
                menu = new Sprite("GameOver.png");
                myGame.backgroundLayer.LateAddChild(menu);
                break;
            case "SelectLevel":
                menu = new Sprite("SelectLevel.png");
                myGame.backgroundLayer.LateAddChild(menu);
                break;
        }
    }
}