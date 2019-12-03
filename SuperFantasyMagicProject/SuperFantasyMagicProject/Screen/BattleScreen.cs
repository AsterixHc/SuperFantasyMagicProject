﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperFantasyMagicProject.Playable_Characters;

namespace SuperFantasyMagicProject.Screen
{
    class BattleScreen : GameScreen
    {

        private int expValue;
        public int ExpValue { get => expValue; }

        //Background image for the splash screen.
        private Texture2D background;
        private Texture2D enemy0Sprite, enemy1Sprite, enemy2Sprite, player0Sprite, player1Sprite, player2Sprite;


        //Positions for screen elements (players, enemies)
        Vector2 player0Position = new Vector2(182, 160);
        Vector2 player1Position = new Vector2(182, 400);
        Vector2 player2Position = new Vector2(182, 640);
        Vector2 enemy0Position = new Vector2(1610, 160);
        Vector2 enemy1Position = new Vector2(1610, 400);
        Vector2 enemy2Position = new Vector2(1610, 640);

        //Path to the background image.
        private string path = "BattleScreen/Background";

        //Array for holding players
        private Character[] players = new Character[3];

        //Array for holding enemies
        private Character[] enemies = new Character[3];

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
        public BattleScreen(Character player0, Character player1, Character player2, Character enemy0, Character enemy1, Character enemy2)
        {
            players[0] = player0;
            players[1] = player1;
            players[2] = player2;
            enemies[0] = enemy0;
            enemies[1] = enemy1;
            enemies[2] = enemy2;            
            players[0].Position = player0Position;
            players[1].Position = player1Position;
            players[2].Position = player2Position;
            enemies[0].Position = enemy0Position;
            enemies[1].Position = enemy1Position;
            enemies[2].Position = enemy2Position;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            background = gameScreenContent.Load<Texture2D>(path);
            enemy0Sprite = gameScreenContent.Load<Texture2D>(enemies[0].Path);
            enemy1Sprite = gameScreenContent.Load<Texture2D>(enemies[1].Path);
            enemy2Sprite = gameScreenContent.Load<Texture2D>(enemies[2].Path);
            player0Sprite = gameScreenContent.Load<Texture2D>(players[0].Path);
            player1Sprite = gameScreenContent.Load<Texture2D>(players[1].Path);
            player2Sprite = gameScreenContent.Load<Texture2D>(players[2].Path);

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
            spriteBatch.Draw(enemy0Sprite, enemies[0].Position, Color.White);
            spriteBatch.Draw(enemy1Sprite, enemies[1].Position, Color.White);
            spriteBatch.Draw(enemy2Sprite, enemies[2].Position, Color.White);
            spriteBatch.Draw(player0Sprite, players[0].Position, Color.White);
            spriteBatch.Draw(player1Sprite, players[1].Position, Color.White);
            spriteBatch.Draw(player2Sprite, players[2].Position, Color.White);
        }

        void ResolveCombat(int type,int target,int dmg)
        {
            if(type == 1)
            {
                enemies[target].TakeDamage(dmg);
            }
            else if(type == 2)
            {
                players[target].TakeDamage(dmg);
            }
        }

        void AttackOpponent(int type, int self, int target)
        {
            if(type == 1)
            {
                ResolveCombat(type,target,players[self].Damage);
            }
            else if(type == 2)
            {
                ResolveCombat(type,target,enemies[self].Damage);
            }
        }
    }
}
