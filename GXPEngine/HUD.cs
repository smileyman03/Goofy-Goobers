using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
public static class HUD
{
    public static EasyDraw health;
    public static EasyDraw fuel;
    public static Sprite DrawHUD()
    {
        return new Sprite("Player_UI.png");
    }

    public static EasyDraw DrawHealth()
    {
        health = new EasyDraw(1920, 1080);
        return health;
    }

    public static EasyDraw DrawFuel()
    {
        fuel = new EasyDraw(1920, 1080);
        return fuel;
    }
    public static void UpdateHUD(float hpAmount, float fuelAmount)
    {
        //Make health bar:
        health.ClearTransparent();
        health.Fill(Color.Green);
        health.ShapeAlign(CenterMode.Min, CenterMode.Min);
        health.Rect(200, 950, (425f / 100f) * hpAmount, 40);
        //Make fuel bar:
        fuel.ClearTransparent();
        fuel.Fill(Color.Red);
        fuel.ShapeAlign(CenterMode.Min, CenterMode.Min);
        fuel.Rect(190, 1000, (375f / 30000f) * fuelAmount, 35);
    }
}