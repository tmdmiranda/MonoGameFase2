using Projeto2_MG.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto2_MG.Control.Guns
{
    public abstract class Gun
    {
        protected float cooldown;
        protected float cooldownLeft;
        protected int maxAmmo;
        public int Ammo { get; protected set; }
        protected float reloadTime;
        public bool Reloading { get; protected set; }

        protected Gun()
        {
            cooldownLeft = 0f;
            Reloading = false;
        }
    }
}
