using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    class Level : GameObject
    {




        private string Tileset = "Black_tiled_but_different.png"; // <---- Add your tileset image right here (there must be no dead space inbetween the tiles)

        private int TilesetCollumbs;
        private int TilesetRows;
        Player player1;


        public Level(string filename, Player player)
        {
            Map leveldata = MapParser.ReadMap(filename);
            player1 = player;
            Sprite Tilesets = new Sprite(Tileset, false, false); // creates a sprite object to get the width and height of the total tileset
            TilesetRows = Tilesets.width / leveldata.TileWidth; // devides the width of the tilesets total width by that of the tiles
            TilesetCollumbs = Tilesets.height / leveldata.TileHeight; // ^ but for height

            SpawnTiles(leveldata);
            SpawnObjects(leveldata);
        }



        void SpawnTiles(Map leveldata)
        {
            if (leveldata.Layers == null || leveldata.Layers.Length == 0)
                return;
            for (int i = 0; i < leveldata.Layers.Length; i++) // loops through all the layers and creates Tiles
            {
                Layer mainlayer = leveldata.Layers[i];
                short[,] tileNumbers = mainlayer.GetTileArray();
                for (int row = 0; row < mainlayer.Height; row++)
                {
                    for (int col = 0; col < mainlayer.Width; col++)
                    {
                        int tileNumber = tileNumbers[col, row];
                        if (tileNumber > 0)
                        {
                            
                            //32 is the tile size in tiled
                            int sX = col * 32;
                            int sY = row * 32;
                           
                            MyGame myGame = (MyGame)game;
                          
                            string selectedObjected = leveldata.Layers[i].Name;
                            switch (selectedObjected)
                            {
                                case ("planetBig"):

                                    Planet planet = new Planet("mars.png", 50, sX, sY, myGame.gravityLayer, 10);
                                    myGame.collisionStuff.LateAddChild(planet);
                                    break;
                                case ("planetMedium"):
                                    Planet planet1 = new Planet("spurral.png", 40, sX, sY, myGame.gravityLayer, 15);
                                    myGame.collisionStuff.LateAddChild(planet1);
                                    break;
                                case ("planetSmall"):
                                    Planet planet2 = new Planet("jupurter.png", 60, sX, sY, myGame.gravityLayer, 5);
                                    myGame.collisionStuff.LateAddChild(planet2);
                                    break;
                                case ("astroid1"):
                                    Asteroid asteroid = new Asteroid("asteroid1.png");
                                    myGame.collisionStuff.LateAddChild(asteroid);
                                    break;

                                case ("astroid2"):
                                    Asteroid asteroid1 = new Asteroid("asteroid2.png");
                                    myGame.collisionStuff.LateAddChild(asteroid1);
                                    break;

                                case ("astroid3"):
                                    Asteroid asteroid2 = new Asteroid("asteroid3.png");
                                    myGame.collisionStuff.LateAddChild(asteroid2);
                                    break;

                                case ("repair"):
                                    RepairPickup repair = new RepairPickup(sX, sY);
                                    myGame.pickupLayer.LateAddChild(repair);
                                    break;

                                case ("shield"):
                                    ShieldPickup shield = new ShieldPickup(sX, sY);
                                    myGame.pickupLayer.LateAddChild(shield);
                                    break;

                                case ("boost"):
                                    BoostPickup boost = new BoostPickup(sX, sY);
                                    myGame.pickupLayer.LateAddChild(boost);
                                    break;

                                case ("fuel"):
                                    FuelPickup fuel = new FuelPickup(sX, sY);
                                    myGame.pickupLayer.LateAddChild(fuel);
                                    break;

                                case ("player"):
                                    Player player = new Player();
                                    Rope rope = new Rope(25, player);
                                    Enemy enemy = new Enemy();
                                    myGame.collisionStuff.LateAddChild(player);
                                    myGame.ropeLayer.LateAddChild(rope);
                                    myGame.collisionStuff.LateAddChild(enemy);
                                    break;

                            
                            }








                        }
                    }
                }
            }

        }



        void SpawnObjects(Map leveldata) // checks for objects and their location
        {

            if (leveldata.ObjectGroups == null || leveldata.ObjectGroups.Length == 0)
                return;

            ObjectGroup objects = leveldata.ObjectGroups[0];
            if (objects.Objects == null)
                return;

            foreach (TiledObject obj in objects.Objects)
            {


            }
        }

    }

}