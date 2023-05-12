using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
public class BoostPickup : Sprite
{
    public BoostPickup(float posX, float posY) : base("BoostPickup.png")
    {
        scale = 0.05f;
        SetOrigin(width / 2, height / 2);
        x = posX;
        y = posY;
    }
}