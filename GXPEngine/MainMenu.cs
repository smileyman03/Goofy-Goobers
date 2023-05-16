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
    private AnimationSprite level1Button = new AnimationSprite("Level1Button.png", 2, 1);
    private AnimationSprite level2Button = new AnimationSprite("Level2Button.png", 2, 1);
    private AnimationSprite level3Button = new AnimationSprite("Level3Button.png", 2, 1);
    private AnimationSprite menuButton = new AnimationSprite("MenuButton.png", 2, 1);
    private AnimationSprite optionsButton = new AnimationSprite("OptionsButton.png", 2, 1);
    private AnimationSprite playAgainButton = new AnimationSprite("PlayAgainButton.png", 2, 1);
    private AnimationSprite playButton = new AnimationSprite("PlayButton.png", 2, 1);
    private AnimationSprite proceedButton = new AnimationSprite("ProceedButton.png", 2, 1);
    private AnimationSprite quitButton = new AnimationSprite("QuitButton.png", 2, 1);
    private AnimationSprite tutorialButton = new AnimationSprite("TutorialButton.png", 2, 1);
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
                        myGame.StartLevel(myGame.level);
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

                //clicked tutorial:
                if (Input.mouseX >= tutorialButton.x - tutorialButton.width / 2 && Input.mouseX <= tutorialButton.x + tutorialButton.width / 2 && Input.mouseY >= tutorialButton.y - tutorialButton.height / 2 && Input.mouseX <= tutorialButton.x + tutorialButton.height / 2)
                {
                    tutorialButton.SetFrame(1);
                    if (Input.GetMouseButtonDown(0))
                    {
                        myGame.DeleteLayers();
                        myGame.StartLevel(4);
                        this.Destroy();
                    }
                }
                else tutorialButton.SetFrame(0);
                //clicked back:
                if (Input.mouseX >= backButton.x - backButton.width / 2 && Input.mouseX <= backButton.x + backButton.width && Input.mouseY >= backButton.y - backButton.height && Input.mouseY <= backButton.y + backButton.height)
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
            case "WinScreen":
                //hovering or Clicked play again:
                if (Input.mouseX >= playAgainButton.x - playAgainButton.width / 2 && Input.mouseX <= playAgainButton.x + playAgainButton.width / 2 && Input.mouseY >= playAgainButton.y - playAgainButton.height / 2 && Input.mouseY <= playAgainButton.y + playAgainButton.height / 2)
                {
                    //make animation:
                    playAgainButton.SetFrame(1);
                    if (Input.GetMouseButtonDown(0))
                    {
                        myGame.DeleteLayers();
                        myGame.StartLevel(myGame.level);
                        this.Destroy();
                    }
                }
                else playAgainButton.SetFrame(0);

                //Clicked options:
                if (Input.mouseX >= proceedButton.x - proceedButton.width / 2 && Input.mouseX <= proceedButton.x + proceedButton.width / 2 && Input.mouseY >= proceedButton.y - proceedButton.height / 2 && Input.mouseY <= proceedButton.y + proceedButton.height / 2)
                {
                    proceedButton.SetFrame(1);
                    if (Input.GetMouseButtonDown(0))
                    {
                        myGame.DeleteLayers();
                        myGame.StartLevel(myGame.level + 1);
                        this.Destroy();
                    }
                }
                else proceedButton.SetFrame(0);

                //Clicked quit:
                if (Input.mouseX >= menuButton.x - menuButton.width / 2 && Input.mouseX <= menuButton.x + menuButton.width / 2 && Input.mouseY >= menuButton.y - menuButton.height / 2 && Input.mouseY <= menuButton.y + menuButton.height / 2)
                {
                    menuButton.SetFrame(1);
                    if (Input.GetMouseButtonDown(0))
                    {
                        myGame.DeleteLayers();
                        myGame.MakeMenu("HomeScreen");
                        this.Destroy();
                    }
                }
                else menuButton.SetFrame(0);
                break;
            case "LastWinScreen":
                //clicked restart:
                if (Input.mouseX >= playAgainButton.x - playAgainButton.width / 2 && Input.mouseX <= playAgainButton.x + playAgainButton.width / 2 && Input.mouseY >= playAgainButton.y - playAgainButton.height / 2 && Input.mouseY <= playAgainButton.y + playAgainButton.height / 2)
                {
                    playAgainButton.SetFrame(1);
                    if (Input.GetMouseButtonDown(0))
                    {
                        myGame.DeleteLayers();
                        myGame.StartLevel(myGame.level);
                        this.Destroy();
                    }
                }
                else playAgainButton.SetFrame(0);

                //clicked back:
                if (Input.mouseX >= menuButton.x - menuButton.width / 2 && Input.mouseX <= menuButton.x + menuButton.width / 2 && Input.mouseY >= menuButton.y - menuButton.height / 2 && Input.mouseY <= menuButton.y + menuButton.height / 2)
                {
                    menuButton.SetFrame(1);
                    if (Input.GetMouseButtonDown(0))
                    {
                        myGame.DeleteLayers();
                        myGame.MakeMenu("HomeScreen");
                        this.Destroy();
                    }
                }
                else menuButton.SetFrame(0);
                break;
        }
    }

    void CheckWhichScreen()
    {
        MyGame myGame = (MyGame)game;
        switch (currentMenu)
        {
            case "HomeScreen":
                //menu screen
                menu = new Sprite("HomeScreen.png");
                myGame.backgroundLayer.LateAddChild(menu);

                //play button
                playButton.SetOrigin(playButton.width / 2, playButton.height / 2);
                playButton.SetXY(965, 427.5f);
                myGame.buttonLayer.LateAddChild(playButton);

                //options button
                optionsButton.SetOrigin(optionsButton.width / 2, optionsButton.height / 2);
                optionsButton.SetXY(965, 645);
                myGame.buttonLayer.LateAddChild(optionsButton);

                //quit button
                quitButton.SetOrigin(quitButton.width / 2, quitButton.height / 2);
                quitButton.SetXY(965, 862.5f);
                myGame.buttonLayer.LateAddChild(quitButton);
                break;
            case "GameOver":
                //home screen
                menu = new Sprite("GameOver.png");
                myGame.backgroundLayer.LateAddChild(menu);

                //back button
                backButton.SetOrigin(backButton.width / 2, backButton.height / 2);
                backButton.SetXY(957.5f, 725);
                myGame.buttonLayer.LateAddChild(backButton);

                //play again button
                playAgainButton.SetOrigin(playAgainButton.width / 2, playAgainButton.height / 2);
                playAgainButton.SetXY(957.5f, 500);
                myGame.buttonLayer.LateAddChild(playAgainButton);
                break;
            case "SelectLevel":
                //home screen
                menu = new Sprite("SelectLevel.png");
                myGame.backgroundLayer.LateAddChild(menu);

                //back button
                backButton.SetOrigin(backButton.width / 2, backButton.height / 2);
                backButton.SetXY(957.5f, 930);
                myGame.buttonLayer.LateAddChild(backButton);

                //level 1 button
                level1Button.SetOrigin(level1Button.width / 2, level1Button.height / 2);
                level1Button.SetXY(350, 550);
                myGame.buttonLayer.LateAddChild(level1Button);

                //level 2 button
                level2Button.SetOrigin(level1Button.width / 2, level1Button.height / 2);
                level2Button.SetXY(950, 550);
                myGame.buttonLayer.LateAddChild(level2Button);

                //level 3 button
                level3Button.SetOrigin(level3Button.width / 2, level3Button.height / 2);
                level3Button.SetXY(1610, 550);
                myGame.buttonLayer.LateAddChild(level3Button);

                //tutorial button
                tutorialButton.SetOrigin(tutorialButton.width / 2, tutorialButton.height / 2);
                tutorialButton.SetXY(950, 320);
                myGame.buttonLayer.LateAddChild(tutorialButton);
                break;
            case "WinScreen":
                //menu screen
                menu = new Sprite("WinScreen.png");
                myGame.backgroundLayer.LateAddChild(menu);

                //play again button
                playAgainButton.SetOrigin(playAgainButton.width / 2, playAgainButton.height / 2);
                playAgainButton.SetXY(965, 427.5f);
                myGame.buttonLayer.LateAddChild(playAgainButton);

                //proceed button
                proceedButton.SetOrigin(proceedButton.width / 2, proceedButton.height / 2);
                proceedButton.SetXY(965, 645);
                myGame.buttonLayer.LateAddChild(proceedButton);

                //menu button
                menuButton.SetOrigin(menuButton.width / 2, menuButton.height / 2);
                menuButton.SetXY(965, 862.5f);
                myGame.buttonLayer.LateAddChild(menuButton);
                break;
            case "LastWinScreen":
                //menu screen
                menu = new Sprite("GameOver.png");
                myGame.backgroundLayer.LateAddChild(menu);

                //menu button
                menuButton.SetOrigin(backButton.width / 2, backButton.height / 2);
                menuButton.SetXY(957.5f, 725);
                myGame.buttonLayer.LateAddChild(menuButton);

                //play again button
                playAgainButton.SetOrigin(playAgainButton.width / 2, playAgainButton.height / 2);
                playAgainButton.SetXY(957.5f, 500);
                myGame.buttonLayer.LateAddChild(playAgainButton);
                break;
        }
    }
}