using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Projeto2_MG.Main;
using Projeto2_MG.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection.Emit;

namespace Projeto2_MG.Main
{
    public class MapLevel
    {
        public char[,] map;
        public int tileSize = 64;
        public List<Point> tiles;

        void LoadMap(string levelFile)
        {
            tiles = new List<Point>();
            string[] linhas = File.ReadAllLines($"Content/{levelFile}");
            int lineCount = linhas.Length;
            int colCount = linhas[0].Length;

            map = new char[colCount, lineCount];
            for (int x = 0; x < colCount; x++) 
            {
                for (int y = 0; y < lineCount; y++)
                {

                }
            }
            
        }

      /*  void drawMap(SpriteBatch _spriteBatch)
        {
            Rectangle position = new Rectangle(0, 0, tileSize, tileSize);
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    position.X = x * tileSize;
                    position.Y = y * tileSize;
                    switch (map[x, y])
                    {
                        case '.':
                            _spriteBatch.Draw(dot, position, Color.White);
                            break;
                        case 'X':
                            _spriteBatch.Draw(wall, position, Color.White);
                            break;
                    }
                }
            }
        }*/

    }
}
