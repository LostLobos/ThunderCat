using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong
{
    public class Objetos
    {
        public Vector2 Posicao;
        public Texture2D Textura;

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Textura, Posicao, Color.White);
        }
        public virtual void Move(Vector2 movimento)
        {
            Posicao += movimento;
        }
    }
}