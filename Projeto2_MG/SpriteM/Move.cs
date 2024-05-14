using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto2_MG.SpriteM
{
    public class Move : Sprite
    {
        public int Speed { get; set; } = 300;

        public Move(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            
        }
    }

}
