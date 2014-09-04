using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
 
namespace PingPong
{
    public static class Input
    {
        public static Vector2 TecladoTecla(PlayerIndex playerIndex)
        {
            Vector2 direction = Vector2.Zero;
            KeyboardState keyboardState = Keyboard.GetState();

            if (playerIndex == PlayerIndex.One)
            {
                if (keyboardState.IsKeyDown(Keys.W))
                    direction.Y += -1;
                if (keyboardState.IsKeyDown(Keys.S))
                    direction.Y += 1;
            }

            if (playerIndex == PlayerIndex.Two)
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                    direction.Y += -1;
                if (keyboardState.IsKeyDown(Keys.Down))
                    direction.Y += 1;
            }
            return direction;
        }
 
    }
}