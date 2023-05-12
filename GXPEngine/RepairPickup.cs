using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
public class RepairPickup : Sprite
{
    public RepairPickup(float posX, float posY) : base("RepairPickup.png")
    {
        scale = 0.05f;
        SetOrigin(width / 2, height / 2);
        x = posX;
        y = posY;
    }
}