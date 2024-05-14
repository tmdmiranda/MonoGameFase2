using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Projeto2_MG.Main
{
    public class TextureHandler
    {
        //--store our "slice" rects--//
        Rectangle[] slices;

        public TextureHandler(int texWidth)
        {
            //--init array--//
            slices = new Rectangle[texWidth];

            //--for clarity in slice loop--//
            int texHeight = texWidth;

            //--loop through creating a "slice" for each texture x--//
            for (int x = 0; x < texWidth; x += texHeight)
            {
                for (int y = 0; y < texHeight; y += texWidth)
                //tex width and height are always equal so safe to use tex width instead of height here
                slices[x] = new Rectangle(x, y, texWidth, texHeight);
            }
        }



        public Rectangle[] getSlices()
        {
            return slices;
        }
    }
}
