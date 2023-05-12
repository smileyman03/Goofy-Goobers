using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
public class FuelPickup : Sprite
{
    public FuelPickup(float posX, float posY) : base("FuelPickup.png")
    {
        scale = 0.05f;
        SetOrigin(width / 2, height / 2);
        x = posX;
        y = posY;
    }
}