﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperFantasyMagicProject.Screen
{
    class BattleScreen : GameScreen
    {

        private int expValue;
        public int ExpValue { get => expValue; }
        //Background image for the splash screen.
        private Texture2D background;
        //Path to the background image.
        private string path = "BattleScreen/Background";

        //Array for holding enemies
        Character[] enemies = new Character[3];

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BattleScreen()
        {

        }

        /// <summary>
        /// Constructor that specifies which enemies are present.
        /// </summary>
        /// <param name="enemy0">The first enemy (top)</param>
        /// <param name="enemy1">The second enemy (middle)</param>
        /// <param name="enemy2">The third enemy (bottom)</param>
        public BattleScreen(Character enemy0, Character enemy1, Character enemy2)
        {
            enemies[0] = enemy0;
            enemies[1] = enemy1;
            enemies[2] = enemy2;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            background = gameScreenContent.Load<Texture2D>(path);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
        }
    }
}
