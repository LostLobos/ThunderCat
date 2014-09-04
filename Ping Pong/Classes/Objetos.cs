using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PingPong
{
    public class Objetos
    {
        public Vector2 Posicao;
        public Texture2D Textura;

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Textura,Posicao,Color.White);
        }
        public virtual void Move(Vector2 movimento)
        {
            Posicao += movimento;
        }

        public Rectangle Bounds
        {
            // Retorna um Objeto Retângulo com os dados do Objeto(Bola ou barra) do tamanho deles, para fazer a Colisão.
            get { return new Rectangle((int)Posicao.X, (int)Posicao.Y, Textura.Width, Textura.Height); }
        }

    }
}