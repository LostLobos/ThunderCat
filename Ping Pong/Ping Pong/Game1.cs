using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace PingPong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        const int POSICAO_BARRA = 70;
        float VELOCIDADE_BOLA = 8f;
        const float TECLADO_VELOCIDADE_BARRA = 10f; // Fator de Multiplicação para a velocidade da Barra.
        const int PLACAR_MAXIMO = 10;
        const float frameRate = 1f;
        // TESTANDO O CÓDIGO DA ZUERA AQUI HUE
        GraphicsDeviceManager graphics; // Quando inicia um Projeto MonoGame, essa variável já vem, tem a ver com montar a tela no monitor.
        SpriteBatch spriteBatch; // Essa variável vai ser a Textura do que vamos desenhar na Tela.
        public static int Altura, Largura; // Altura e Largura da tela.
        Jogador jogador1,jogador2; // Variaveis do tipo Objeto, elas herdam tudo da Classe Jogador.
        Bola bola; // " " "
        Texture2D MeioTextura,Menu,Logo; // Variável do Tipo Textura 2D, serve para Carregar uma Imagem nela.
        SpriteFont Texto; // Variavel do Tipo SprinteFont, serve para Escrever Texto na Tela
        Vector2 ganhador; // Variável do Tipo Vector2(X,Y) para determinar a posição. 

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Largura = GraphicsDevice.Viewport.Width; // Pega a Largura da tela que por Default é 800 Pixels.
            Altura = GraphicsDevice.Viewport.Height; // Pega a Altura da tela que por Default é 480 Pixels.

            jogador1 = new Jogador(); // Cria uma nova Instacia para a variavel jogador
            jogador2 = new Jogador(); // " " "
            bola = new Bola(); // " " "

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw Texturas.
            spriteBatch = new SpriteBatch(GraphicsDevice); // Isso já vem...

            jogador1.Textura = Content.Load<Texture2D>("Paddle"); // Aqui é carregado a imagem Paddle.png ali do lado em Content, ela é a barra do jogador.
            jogador2.Textura = Content.Load<Texture2D>("Paddle"); // Idem
            jogador1.Posicao = new Vector2(POSICAO_BARRA, Altura / 2 - jogador1.Textura.Height / 2);
            jogador2.Posicao = new Vector2(Largura - jogador2.Textura.Width - POSICAO_BARRA, Altura / 2 - jogador2.Textura.Height / 2);
 
            bola.Textura = Content.Load<Texture2D>("Ball"); // Idem
            
            bola.Launch(VELOCIDADE_BOLA); // Essa função está em Bola.cs

            MeioTextura = Content.Load<Texture2D>("Middle");

            Texto = Content.Load<SpriteFont>("Fonte");

            Menu = Content.Load<Texture2D>("Ping Pong menu");
            Logo = Content.Load<Texture2D>("Logo");

            Som.CarregarSom(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        protected override void Update(GameTime gameTime)
        {
            Random random = new Random();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                jogador1.pontuacao = 0;
                jogador2.pontuacao = 0;     // Essa parte detecta se o Jogador apertou ESC e "reseta" o jogo.
                VELOCIDADE_BOLA = 8f;
                bola.Launch(VELOCIDADE_BOLA);
            }
            else if (GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                // Abre o MENU    
            }
            // TODO: Add your update logic here

            Largura = GraphicsDevice.Viewport.Width; // Pega a Largura da Tela do JOGO.
            Altura = GraphicsDevice.Viewport.Height; // Pega a Altura da Tela do JOGO.

            Vector2 player1Velocity = Input.TecladoTecla(PlayerIndex.One) * TECLADO_VELOCIDADE_BARRA; // Calcula a Movimentação, normalmente vai ser -10 para W.
            Vector2 player2Velocity = Input.TecladoTecla(PlayerIndex.Two) * TECLADO_VELOCIDADE_BARRA; // Ou 10 para S.

            bola.Move(bola.Velocity);

            jogador1.Move(player1Velocity);
            jogador2.Move(player2Velocity);

            if (bola.Posicao.X < 0 && VELOCIDADE_BOLA != 0) // Verifica se a bola Saiu da tela e marcou pontuação, na esquerda.
            {
                bola.Launch(VELOCIDADE_BOLA);
                jogador2.pontuacao++;
            }
            else if (bola.Posicao.X > Largura && VELOCIDADE_BOLA != 0) // Verifica se a bola Saiu da tela e marcou pontuação, na direita.
            {
                bola.Launch(VELOCIDADE_BOLA);
                jogador1.pontuacao++;
            }

            if (jogador1.Bounds.Intersects(bola.Bounds)) // Verifica se a Barra do Jogador Interceptou a bolinha. 
            {
                Som.SomBarra.Play();
                if (random.Next(7) == 5 || random.Next(5) == 3) // Aqui ele faz tipo uma probabilidade da bola acelerar/desacelerar eventualmente, além de mudar a direção dela.
                    bola.Velocity.X = Math.Abs(bola.Velocity.X) + 10;
                //else if (random.Next(5) == 2 && bola.Velocity.X > 8f)
                    //bola.Velocity.X = Math.Abs(bola.Velocity.X) - 1;
                else
                    bola.Velocity.X = Math.Abs(bola.Velocity.X);
            }

            if (jogador2.Bounds.Intersects(bola.Bounds)) // " " "
            {
                Som.SomBarra.Play();
                if (random.Next(7) == 5 || random.Next(5) == 3) // " " "
                    bola.Velocity.X = -Math.Abs(bola.Velocity.X) + 10;
                //else if (random.Next(7) == 2 && bola.Velocity.X < -8f)
                //bola.Velocity.X = -Math.Abs(bola.Velocity.X);
                else
                    bola.Velocity.X = -Math.Abs(bola.Velocity.X) - 1;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            spriteBatch.Draw(Menu, new Rectangle(0, 0, Largura, Altura), Color.Black);
          //  spriteBatch.Draw(Logo, new Vector2(Largura/2,Altura/4),new Rectangle(Largura / 2, Altura / 4, 300, 300), Color.White,1f,new Vector2(0,0),SpriteEffects.None,1f);
           
            spriteBatch.End();

            spriteBatch.Begin(); // Começar a desenhar na Tela.
            spriteBatch.DrawString(Texto, Convert.ToString(jogador2.pontuacao), new Vector2(Largura / 2 + Altura / 4, Altura / 2 - Altura / 2), Color.Blue);
            spriteBatch.DrawString(Texto, Convert.ToString(jogador1.pontuacao), new Vector2(Largura / 2 - Altura / 3, Altura / 2 - Altura / 2), Color.Blue); 
            jogador1.Draw(spriteBatch);
            jogador2.Draw(spriteBatch);
            bola.Draw(spriteBatch);
            spriteBatch.Draw(MeioTextura, new Rectangle(Largura / 2 - MeioTextura.Width / 2, 0, MeioTextura.Width, Altura), null, Color.White);
            spriteBatch.DrawString(Texto, "FPS: " + Convert.ToString(Math.Round(frameRate / (float)gameTime.ElapsedGameTime.TotalSeconds)), new Vector2(5,Altura - 25), Color.BlueViolet, 0, new Vector2(0, 0), 0.45f, SpriteEffects.None, 1.0f);
            spriteBatch.End(); // Fim do Desenho.

            if (jogador1.pontuacao >= PLACAR_MAXIMO || jogador2.pontuacao >= PLACAR_MAXIMO)
            {
                if (jogador1.pontuacao >= PLACAR_MAXIMO)
                    ganhador = new Vector2(Largura / 4 - Altura / 3, Altura / 3);
                else
                    ganhador = new Vector2(Largura / 3 + Altura / 3, Altura / 3);

                spriteBatch.Begin();
                spriteBatch.DrawString(Texto, "Fucking Vencedor!", ganhador, Color.CornflowerBlue, 0, new Vector2(0,0), 0.5f, SpriteEffects.None, 1.0f);
                spriteBatch.DrawString(Texto, "Aperte ESC para reiniciar a Partida!", new Vector2(Largura / 12, Altura / 2 + Largura / 8), Color.FloralWhite, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 1.0f);
                spriteBatch.End();

                VELOCIDADE_BOLA = 0f;
            }

            base.Draw(gameTime);
        }
    }
}