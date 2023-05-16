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
    private AnimationSprite backButton = new AnimationSprite("BackButton.png", 2, 1);
    private AnimationSprite continueButton = new AnimationSprite("ContinueButton.png", 2, 1);
    private AnimationSprite level1Button = new AnimationSprite("Level1Button.png", 2, 1);
    private AnimationSprite level2Button = new AnimationSprite("Level2Button.png", 2, 1);
    private AnimationSprite level3Button = new AnimationSprite("Level3Button.png", 2, 1);
    private AnimationSprite menuButton = new AnimationSprite("MenuButton.png", 2, 1);
    private AnimationSprite optionsButton = new AnimationSprite("OptionsButton.png", 2, 1);
    private AnimationSprite playAgainButton = new AnimationSprite("PlayAgainButton.png", 2, 1);
    private AnimationSprite playButton = new AnimationSprite("PlayButton.png", 2, 1);
    private AnimationSprite proceedButton = new AnimationSprite("ProceedButton.png", 2, 1);
    private AnimationSprite quitButton = new AnimationSprite("QuitButton.png", 2, 1);
    public MainMenu(string menuScreen)
    {
        currentMenu = menuScreen;
        CheckWhichScreen();
    }
    void Update()
    {
       // Console.WriteLine("X: " + Input.mouseX + " Y: " + Input.mouseY);
        MyGame myGame = (MyGame)game;

        switch (currentMenu)
        {
            case "HomeScreen":
                //hovering or Clicked select level:
                if (Input.mouseX >= playButton.x - playButton.width / 2 && Input.mouseX <= playButton.x + playButton.width / 2 && Input.mouseY >= playButton.y - playButton.height / 2 && Input.mouseY <= playButton.y + playButton.height / 2)
                {
                    //make animation:
                    playButton.SetFrame(1);
                    if (Input.GetMouseButtonDown(0))
                    {
                        myGame.DeleteLayers();
                        myGame.MakeMenu("SelectLevel");
                        this.Destroy();
                    }
                }
                else playButton.SetFrame(0);

                //Clicked options:
                if (Input.mouseX >= optionsButton.x - optionsButton.width / 2 && Input.mouseX <= optionsButton.x + optionsButton.width / 2 && Input.mouseY >= optionsButton.y - optionsButton.height / 2 && Input.mouseY <= optionsButton.y + optionsButton.height / 2)
                {
                    optionsButton.SetFrame(1);
                }
                else optionsButton.SetFrame(0);
                
                //Clicked quit:
                if (Input.mouseX >= quitButton.x - quitButton.width / 2 && Input.mouseX <= quitButton.x + quitButton.width / 2 && Input.mouseY >= quitButton.y - quitButton.height / 2 && Input.mouseY <= quitButton.y + quitButton.height / 2)
                {
                    quitButton.SetFrame(1);
                    if (Input.GetMouseButtonDown(0)) myGame.Destroy();
                }
                else quitButton.SetFrame(0);
                break;
            case "GameOver":
                //clicked restart:
                if (Input.mouseX >= playAgainButton.x - playAgainButton.width / 2 && Input.mouseX <= playAgainButton.x + playAgainButton.width / 2 && Input.mouseY >= playAgainButton.y - playAgainButton.height / 2 && Input.mouseY <= playAgainButton.y + playAgainButton.height / 2)
                {
                    playAgainButton.SetFrame(1);
                    if (Input.GetMouseButtonDown(0))
                    {
                        myGame.DeleteLayers();
                        myGame.StartLevel(1);
                        this.Destroy();
                    }
                }
                else playAgainButton.SetFrame(0);

                //clicked back:
                if (Input.mouseX >= backButton.x - backButton.width / 2 && Input.mouseX <= backButton.x + backButton.width / 2 && Input.mouseY >= backButton.y - backButton.height / 2 && Input.mouseY <= backButton.y + backButton.height / 2)
                {
                    backButton.SetFrame(1);
                    if (Input.GetMouseButtonDown(0))
                    {
                        myGame.DeleteLayers();
                        myGame.MakeMenu("HomeScreen");
                        this.Destroy();
                    }
                }
                else backButton.SetFrame(0);
                break;
            case "SelectLevel":
                //clicked level 1:
                if (Input.mouseX >= level1Button.x - level1Button.width/2 && Input.mouseX <= level1Button.x + level1Button.width/2 && Input.mouseY >= level1Button.y - level1Button.height/2 && Input.mouseY <= level1Button.y + level1Button.height/2)
                {
                    level1Button.SetFrame(1);
                    if (Input.GetMouseButtonDown(0))
                    {
                        myGame.DeleteLayers();
                        myGame.StartLevel(1);
                        this.Destroy();
                    }
                }
                else level1Button.SetFrame(0);

                //clicked level 2:
                if (Input.mouseX >= level2Button.x - level2Button.width / 2 && Input.mouseX <= level2Button.x + level2Button.width / 2 && Input.mouseY >= level2Button.y - level2Button.height / 2 && Input.mouseY <= level2Button.y + level2Button.height / 2)
                {
                    level2Button.SetFrame(1);
                    if (Input.GetMouseButtonDown(0))
                    {
                        myGame.DeleteLayers();
                        myGame.StartLevel(2);
                        this.Destroy();
                    }
                }
                else level2Button.SetFrame(0);

                //clicked level 3:
                if (Input.mouseX >= level3Button.x - level3Button.width / 2 && Input.mouseX <= level3Button.x + level3Button.width / 2 && Input.mouseY >= level3Button.y - level3Button.height / 2 && Input.mouseY <= level3Button.y + level3Button.height / 2)
                {
                    level3Button.SetFrame(1);
                    if (Input.GetMouseButtonDown(0))
                    {
                        myGame.DeleteLayers();
                        myGame.StartLevel(3);
                        this.Destroy();
                    }
                }
                else level3Button.SetFrame(0);

                //clicked back:
                if (Input.mouseX >= 840 && Input.mouseX <= 1075 && Input.mouseY >= 880 && Input.mouseY <= 980)
                {
                    backButton.SetFrame(1);
                    if (Input.GetMouseButtonDown(0))
                    {
                        myGame.DeleteLayers();
                        myGame.MakeMenu("HomeScreen");
                        this.Destroy();
                    }
                }
                else backButton.SetFrame(0);
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
                playButton.SetOrigin(playButton.width / 2, playButton.height / 2);
                playButton.SetXY(965, 427.5f);
                myGame.buttonLayer.LateAddChild(playButton);
                optionsButton.SetOrigin(optionsButton.width / 2, optionsButton.height / 2);
                optionsButton.SetXY(965, 645);
                myGame.buttonLayer.AddChild(optionsButton);
                quitButton.SetOrigin(quitButton.width / 2, quitButton.height / 2);
                quitButton.SetXY(965, 862.5f);
                myGame.buttonLayer.LateAddChild(quitButton);
                break;
            case "GameOver":
                menu = new Sprite("GameOver.png");
                myGame.backgroundLayer.LateAddChild(menu);
                backButton.SetOrigin(backButton.width / 2, backButton.height / 2);
                backButton.SetXY(957.5f, 725);
                myGame.buttonLayer.AddChild(backButton);
                playAgainButton.SetOrigin(playAgainButton.width / 2, playAgainButton.height / 2);
                playAgainButton.SetXY(957.5f, 500);
                myGame.buttonLayer.AddChild(playAgainButton);
                break;
            case "SelectLevel":
                menu = new Sprite("SelectLevel.png");
                myGame.backgroundLayer.LateAddChild(menu);
                backButton.SetOrigin(backButton.width / 2, backButton.height / 2);
                backButton.SetXY(957.5f, 930);
                myGame.buttonLayer.AddChild(backButton);
                level1Button.SetOrigin(level1Button.width / 2, level1Button.height / 2);
                level1Button.SetXY(350, 550);
                myGame.buttonLayer.AddChild(level1Button);
                level2Button.SetOrigin(level1Button.width / 2, level1Button.height / 2);
                level2Button.SetXY(950, 550);
                myGame.buttonLayer.AddChild(level2Button);
                level3Button.SetOrigin(level3Button.width / 2, level3Button.height / 2);
                level3Button.SetXY(1610, 550);
                myGame.buttonLayer.AddChild(level3Button);
                break;
        }
    }
}