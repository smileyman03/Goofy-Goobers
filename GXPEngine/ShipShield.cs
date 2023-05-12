using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
public class ShipShield : Sprite
{
    public ShipShield() : base("Ship_Shield.png")
    {
        scale = 0.3f;
        SetOrigin(1.75f * width, 1.75f * height);
    }

    void Update()
    {
    }
}