using Projeto2_MG.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto2_MG.Control.Guns
{
    public class Rifle: Gun
    {
        public Rifle()
        {
            cooldown = 0.1f;
            maxAmmo = 30;
            Ammo = maxAmmo;
            reloadTime = 2f;
        }
    }
}
