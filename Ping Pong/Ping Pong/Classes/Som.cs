using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace PingPong
{
    public static class Som
    {
        public static SoundEffect SomBarra, SomBola;

        public static void CarregarSom(ContentManager Content)
        {
            SomBola = Content.Load<SoundEffect>("SomBolaColisao");
            SomBarra = Content.Load<SoundEffect>("BarraBolaColisaoSom");
        }

    }
}
