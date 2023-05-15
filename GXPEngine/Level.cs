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
                            /*
                            Tiles tile = new Tiles(Tileset, TilesetRows, TilesetCollumbs, leveldata.Layers[i].Name == "Background" ? false : true);
                            tile.SetFrame(tileNumber - 1);
                            tile.x = col * tile.width;
                            tile.y = row * tile.height;

                            if (dist(player1.x, player1.y, tile.x, tile.y) < 1500)
                            {
                                AddChild(tile);
                            }

                            */

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