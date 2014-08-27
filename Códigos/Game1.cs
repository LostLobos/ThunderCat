#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace PingPong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        const int VELOCIDADE_BARRA = 70;  
        const float VELOCIDADE_BOLA = 3f;
        const float TECLADO_VELOCIDADE_BARRA = 10f;
        GraphicsDeviceManager graphics; // Quando inicia um Projeto MonoGame, essa variável já vem, tem a ver com montar a tela no monitor.
        SpriteBatch spriteBatch; // Essa variável vai ser a Textura do que vamos desenhar na Tela.
        public static int Altura, Largura; // Altura e Largura da tela.
        Jogador jogador1,jogador2; // Variaveis do tipo Objeto, elas herdam tudo da Classe Jogador.
        Bola bola; // " " "


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
            Altura = GraphicsDevice.Viewport.Height; // Pega a Altura da tela que por Default é 400 Pixels.

            jogador1 = new Jogador(); 
            jogador2 = new Jogador();
            bola = new Bola();

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

            jogador1.Textura = Content.Load<Texture2D>("Paddle");
            jogador2.Textura = Content.Load<Texture2D>("Paddle");
            jogador1.Posicao = new Vector2(VELOCIDADE_BARRA, Altura / 2 - jogador1.Textura.Height / 2);
            jogador2.Posicao = new Vector2(Largura - jogador2.Textura.Width - VELOCIDADE_BARRA, Altura / 2 - jogador2.Textura.Height / 2);
 
            bola.Textura = Content.Load<Texture2D>("Ball");
            
            bola.Launch(VELOCIDADE_BOLA);
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
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            Largura = GraphicsDevice.Viewport.Width;
            Altura = GraphicsDevice.Viewport.Height;

            bola.Move(bola.Velocity);

            Vector2 player1Velocity = Input.TecladoTecla(PlayerIndex.One) * TECLADO_VELOCIDADE_BARRA;
            Vector2 player2Velocity = Input.TecladoTecla(PlayerIndex.Two) * TECLADO_VELOCIDADE_BARRA;

            jogador1.Move(player1Velocity);
            jogador2.Move(player2Velocity);

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
            jogador1.Draw(spriteBatch);
            jogador2.Draw(spriteBatch);
            bola.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
