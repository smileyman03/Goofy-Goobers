using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
public class Asteroid : Sprite
{
    public Vector2 velocity = new Vector2(2, 0);
    public float mass = 1f;
    public Asteroid(string image) : base(image)
    {
        SetOrigin(width / 2, height / 2);
        SetXY(0, game.height/2);
    }
    void Update()
    {
        x += velocity.x;
        y += velocity.y;
    }
    void OnCollision(GameObject other)
    {
        if (other is Asteroid)
        {
            Asteroid asteroid = (Asteroid)other;

            //Calculate CoM:
            Vector2 asteroidMomentum = asteroid.velocity.GetMomentum(asteroid.mass);
            Vector2 thisMomentum = velocity.GetMomentum(mass);
            Vector2 centerOfMass = (asteroidMomentum + thisMomentum) / (asteroid.mass + mass);

            //Apply bounce to other asteroid
            asteroid.velocity = centerOfMass - 1 * (asteroid.velocity - centerOfMass);

            //Apply bounce to this asteroid
            velocity = centerOfMass - 1 * (velocity - centerOfMass);
        }

        if (other is Planet)
        {
            Planet planet = (Planet)other;
            Vector2 main;
            Vector2 pos;
            float angleX;
            main.x = x;
            main.y = y;

            pos.x = planet.x;
            pos.y = planet.y;

            pos = pos - main;

            if (pos.Length() < planet.width / 2 + width / 2)
            {
                main = -velocity;
                if (pos.GetAngleDegrees() > main.GetAngleDegrees())
                {
                    angleX = pos.GetAngleDegrees() - main.GetAngleDegrees();
                    main.SetAngleDegrees(main.GetAngleDegrees() + angleX * 2);
                }
                else
                {
                    angleX = pos.GetAngleDegrees() - pos.GetAngleDegrees();
                    main.SetAngleDegrees(main.GetAngleDegrees() - angleX * 2);
                }
                velocity = main;
            }
        }

        if (other is Gravity)
        {
            Gravity pull = (Gravity)other;
            Vector2 center;
            Vector2 ship;

            ship.x = x;
            ship.y = y;

            center.x = pull.x;
            center.y = pull.y;

            if (ship.DistanceTo(center) < pull.width / 2 + width / 2)
            {
                Console.WriteLine("collide");
                velocity += (center - ship).Normalized() * 0.1f * pull.pullStrength;
            }
        }
    }
}