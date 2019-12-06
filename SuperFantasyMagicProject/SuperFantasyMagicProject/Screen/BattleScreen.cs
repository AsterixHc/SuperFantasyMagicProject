﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SuperFantasyMagicProject.Playable_Characters;
using Microsoft.Xna.Framework.Media;

namespace SuperFantasyMagicProject.Screen
{
    enum BattleTracker { Playerturn,Playerattack, Enemyturn, Enemyattack, start }

    class BattleScreen : GameScreen
    {
        BattleTracker tracker;
        Song song;
        Random rnd;

        private int expValue;
        int enemyTarget = 0;
        public int ExpValue { get => expValue; private set => expValue = value; }

        //Background image for the battle screen.
        private Texture2D background;
        private string path = "BattleScreen/Background";

        //Textures for enemy and player characters.
        private Texture2D enemy0Sprite, enemy1Sprite, enemy2Sprite, player0Sprite, player1Sprite, player2Sprite;        private SpriteFont hpPlayer1;
        private string hpOnScreen = "hpOnScreen";
        private int hp = 100;                int targetedPlayer;

        //Fixed positions for screen elements (players, enemies)
        Vector2 player0Position = new Vector2(220, 220);
        Vector2 player1Position = new Vector2(220, 450);
        Vector2 player2Position = new Vector2(220, 700);
        Vector2 enemy0Position = new Vector2(1710, 220);
        Vector2 enemy1Position = new Vector2(1710, 460);
        Vector2 enemy2Position = new Vector2(1710, 700);        

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
        /// Constructor that specifies enemies and players.
        /// </summary>
        /// <param name="player0">The first player (top)</param>
        /// <param name="player1">The second player (middle)</param>
        /// <param name="player2">The third player (bottom)</param>
        /// <param name="enemy0">The first enemy (top)</param>
        /// <param name="enemy1">The second enemy (middle)</param>
        /// <param name="enemy2">The third enemy (bottom)</param>
        /// /// <param name="exp">The amount of experience points the encounter is worth</param>
        public BattleScreen(Character enemy0, Character enemy1, Character enemy2, int exp)
        {
            players[0] = new Rogue();
            players[1] = new Warrior();
            players[2] = new Mage();
            enemies[0] = enemy0;
            enemies[1] = enemy1;
            enemies[2] = enemy2;
            ExpValue = exp;
            players[0].Position = player0Position;
            players[1].Position = player1Position;
            players[2].Position = player2Position;
            enemies[0].Position = enemy0Position;
            enemies[1].Position = enemy1Position;
            enemies[2].Position = enemy2Position;
            tracker = BattleTracker.start;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            background = gameScreenContent.Load<Texture2D>(path);
            this.song = gameScreenContent.Load<Song>("Final Fantasy VI Battle Theme Extended");
            MediaPlayer.Play(song);
            //Code for music looping
            //MediaPlayer.IsRepeating = true;

            //Load textures (players/enemies).
            player0Sprite = gameScreenContent.Load<Texture2D>(players[0].Path);
            player1Sprite = gameScreenContent.Load<Texture2D>(players[1].Path);
            player2Sprite = gameScreenContent.Load<Texture2D>(players[2].Path);
            enemy0Sprite = gameScreenContent.Load<Texture2D>(enemies[0].Path);
            enemy1Sprite = gameScreenContent.Load<Texture2D>(enemies[1].Path);
            enemy2Sprite = gameScreenContent.Load<Texture2D>(enemies[2].Path);            hpPlayer1 = gameScreenContent.Load<SpriteFont>(hpOnScreen);
            //Set origins (players/enemies).
            players[0].Origin = new Vector2(player0Sprite.Width / 2, player0Sprite.Height / 2);
            players[1].Origin = new Vector2(player1Sprite.Width / 2, player1Sprite.Height / 2);
            players[2].Origin = new Vector2(player2Sprite.Width / 2, player2Sprite.Height / 2);
            enemies[0].Origin = new Vector2(enemy0Sprite.Width / 2, enemy0Sprite.Height / 2);
            enemies[1].Origin = new Vector2(enemy1Sprite.Width / 2, enemy1Sprite.Height / 2);
            enemies[2].Origin = new Vector2(enemy2Sprite.Width / 2, enemy2Sprite.Height / 2);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //Player turn.
            HandleInput();
            foreach (Character character in enemies)
            {
                character.Update(gameTime);
            }

            if (enemies.All(alive => false))
            {
                AllocateExp();
                if (RogueStats.HasLevelUp || WarriorStats.HasLevelUp || MageStats.HasLevelUp)
                {
                    ScreenManager.ChangeScreenTo(new LevelUpScreen());
                }
            }

            //Enemy turn.
            Enemyturn();
            foreach (Character character in players)
            {
                character.Update(gameTime);
            }

            if (players.All(alive => false))
            {
                //TODO: Add functionality
                //Death screen
            }            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            spriteBatch.Draw(player0Sprite, players[0].Position, new Rectangle(0, 0, player0Sprite.Width, player0Sprite.Height),
                    Color.White, 0, players[0].Origin, 1f, SpriteEffects.None, 1f);
            spriteBatch.Draw(player1Sprite, players[1].Position, new Rectangle(0, 0, player1Sprite.Width, player1Sprite.Height),
                    Color.White, 0, players[1].Origin, 1f, SpriteEffects.None, 1f);
            spriteBatch.Draw(player2Sprite, players[2].Position, new Rectangle(0, 0, player2Sprite.Width, player2Sprite.Height),
                    Color.White, 0, players[2].Origin, 1f, SpriteEffects.None, 1f);
            spriteBatch.Draw(enemy0Sprite, enemies[0].Position, new Rectangle(0 , 0, enemy0Sprite.Width, enemy0Sprite.Height),
                    Color.White, 0, enemies[0].Origin, 1f, SpriteEffects.None, 1f);
            spriteBatch.Draw(enemy1Sprite, enemies[1].Position, new Rectangle(0, 0, enemy1Sprite.Width, enemy1Sprite.Height),
                    Color.White, 0, enemies[1].Origin, 1f, SpriteEffects.None, 1f);
            spriteBatch.Draw(enemy2Sprite, enemies[2].Position, new Rectangle(0, 0, enemy2Sprite.Width, enemy2Sprite.Height),
                    Color.White, 0, enemies[2].Origin, 1f, SpriteEffects.None, 1f);

            spriteBatch.DrawString(hpPlayer1, "HP: " + players[0].CurrentHealth, new Vector2(players[0].Position.X,players[0].Position.Y),Color.Red);
            spriteBatch.DrawString(hpPlayer1, "HP: " + enemies[0].CurrentHealth, new Vector2(enemies[0].Position.X, enemies[0].Position.Y), Color.Red);
        }

        void PlayerTarget(int chosenPlayer, int targetedEnemy)
        {
            if(tracker != BattleTracker.Playerturn)
            {
                return;
            }

            tracker = BattleTracker.Playerattack;
            PlayerAttack(0,targetedEnemy,chosenPlayer);
        }

        void PlayerAttack(int playerDamageAmount, int targetedEnemy, int chosenPlayer)
        {
            if(tracker != BattleTracker.Playerattack)
            {
                return;
            }

            playerDamageAmount = players[chosenPlayer].Damage;

            enemies[targetedEnemy].TakeDamage(playerDamageAmount);

            tracker = BattleTracker.Enemyturn;
            Console.WriteLine(enemies[targetedEnemy].CurrentHealth);
            enemyTarget = 0;
            //Console.ReadKey();
            
        }

        public override void HandleInput()
        {

            if(tracker != BattleTracker.start)
            {
                return;
            }

            KeyboardState keyboard = Keyboard.GetState();
            

            if(keyboard.IsKeyDown(Keys.D1))
            {
                enemyTarget = 1;
                Console.WriteLine(enemyTarget);
            }

            if(keyboard.IsKeyDown(Keys.D) && enemyTarget > 0)
            {
                Console.WriteLine("PlayerTargetLaunched");
                enemyTarget--;
                tracker = BattleTracker.Playerturn;
                PlayerTarget(0, enemyTarget);
            }
            
        }

        void Enemyturn()
        {
            if(tracker != BattleTracker.Enemyturn)
            {
                return;
            }

            //targetedPlayer = rnd.Next(0,3);
            targetedPlayer = 0;
            EnemyAttack(targetedPlayer,0,0);
            tracker = BattleTracker.Enemyattack;
        }

        void EnemyAttack(int targetedPlayer, int chosenEnemy, int enemyDamageAmount)
        {

            if(tracker != BattleTracker.Enemyattack)
            {
                return;
            }

            enemyDamageAmount = enemies[chosenEnemy].Damage;
            
            players[targetedPlayer].TakeDamage(enemyDamageAmount);
            tracker = BattleTracker.start;
            
        }

        /// <summary>
        /// Allocates the experience gained from battle encounter to player characters.
        /// </summary>
        private void AllocateExp()
        {
            RogueStats.Experience += (ExpValue / 3);
            WarriorStats.Experience += (ExpValue / 3);
            MageStats.Experience += (ExpValue / 3);
        }
    }
}
