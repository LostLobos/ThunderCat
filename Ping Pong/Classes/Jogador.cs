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
        public int pontuacao = 0;

        public override void Move(Vector2 movimento) // "public override void Move", ele substitui a função "public virtual void Move" existente no "Objetos.cs"
        { 
            // Aqui verifica se a Barra ultrapassou os limites da tela.

            if ((Posicao.Y + Textura.Height) > Game1.Altura && movimento.Y > 0)
            {
            }
            else if ((Posicao.Y - Textura.Height / 20 ) < 0 && movimento.Y < 0)
            {
            }
            else
                Posicao += movimento;

        }
  
    }
}