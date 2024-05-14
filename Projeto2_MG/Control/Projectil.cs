using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Projeto2_MG.SpriteM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto2_MG.Control
{
    public class Projectil : Move
    {
        public Vector2 Direction { get; set; }
        public float Lifespan { get; private set; }
        public int Damage { get; }

        public Projectil(Texture2D tex, DataProj data) : base(tex, data.Position)
        {
            Speed = data.Speed;
            Rotation = data.Rotation;
            Direction = new((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            Lifespan = data.Lifespan;
            Damage = data.Damage;
        }
    }
}
