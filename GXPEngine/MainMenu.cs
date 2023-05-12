using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
public class MainMenu : GameObject
{
    public String currentMenu = "startingMenu";
    public MainMenu(string menuScreen)
    {
        currentMenu = menuScreen;
    }

    void Update()
    {
        CheckWhichScreen();
    }

    void CheckWhichScreen()
    {
        switch (currentMenu)
        {
            case "startingMenu":
                //do something
                break;
            case "gameOver":
                //do something
                break;
        }
    }
}