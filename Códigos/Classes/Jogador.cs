using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong
{
    class Jogador : Objetos
    {
        public void ColisaoBarra()
        {
        }
        public override void Move(Vector2 movimento)
        {
            Posicao += movimento;
        }
    }
}
