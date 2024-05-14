using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto2_MG.SpriteM
{
    public class Sprite
    {
        protected readonly Texture2D texture;
        protected readonly Vector2 origin;

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }
        public Color Color { get; set; }

        public Sprite(Texture2D tex, Vector2 pos)
        {
            texture = tex;
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            Position = pos;
            Rotation = 0f;
            Scale = 1f;
            Color = Color.White;
        }

        public virtual void Draw()
        {
            
        }
    }

}
