using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong
{
    class Bola : Objetos
    {
        public Vector2 Velocity;
        public Random random;

        public Bola()
        {
            random = new Random();
        }

        public void Launch(float speed)
        {
            Posicao = new Vector2(Game1.Largura / 2 - Textura.Width / 2, Game1.Altura / 2 - Textura.Height / 2);
            // get a random + or - 60 degrees angle to the right
            float rotation = (float)(Math.PI / 2 + (random.NextDouble() * (Math.PI / 1.5f) - Math.PI / 3));

            Velocity.X = (float)Math.Sin(rotation);
            Velocity.Y = (float)Math.Cos(rotation);

            // 50% chance whether it launches left or right
            if (random.Next(2) == 1)
            {
                Velocity.X *= -1; //launch to the left
            }

            Velocity *= speed;
        }

        public void CheckWallCollision()
        {
            if (Posicao.Y < 0)
            {
            Posicao.Y = 0;
            Velocity.Y *= -1;
            }

            if (Posicao.Y + Textura.Height > Game1.Altura)
            {
                Posicao.Y = Game1.Altura - Textura.Height;
                Velocity.Y *= -1;
            }
        }

        public override void Move(Vector2 movimento)
        {
            base.Move(movimento);
            CheckWallCollision();
        }
    }
}
