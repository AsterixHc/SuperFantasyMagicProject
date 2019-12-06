using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SuperFantasyMagicProject.Playable_Characters;

namespace SuperFantasyMagicProject.Screen
{
    enum BattleTracker { Playerturn, Playerattack, Enemyturn, Enemyattack, Start, SpeedEvaluationPlayer, SpeedEvaluationEnemy }

    class BattleScreen : GameScreen
    {
        BattleTracker tracker;

        Random rnd = new Random();

        private int expValue;
        int enemyTarget = 0;
        public int ExpValue { get => expValue; }

        //Background image for the battle screen.
        private Texture2D background;
        private string path = "BattleScreen/Background";

        //Textures for enemy and player characters.
        private Texture2D enemy0Sprite, enemy1Sprite, enemy2Sprite, player0Sprite, player1Sprite, player2Sprite;        private SpriteFont hpPlayer1;
        private string hpOnScreen = "hpOnScreen";        int targetedPlayer;

        int playerSpeed;
        int enemySpeed;

        private bool firstTurn = false;
        private bool secoundTurn = false;
        private bool thirdTurn = false;
        private bool fourthTurn = false;
        private bool fifthTurn = false;
        private bool sixthTurn = false;

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
            tracker = BattleTracker.Start;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            background = gameScreenContent.Load<Texture2D>(path);

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
            //EncounterTurnSystemReset();
            PlayerSpeedCheck();
            HandleInput();
            Enemyturn();
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

            spriteBatch.DrawString(hpPlayer1, "Player 1 HP: " + players[0].CurrentHealth, new Vector2(players[0].Position.X - (player0Sprite.Width / 2), players[0].Position.Y - player0Sprite.Height), Color.Red);
            spriteBatch.DrawString(hpPlayer1, "Player 2 HP: " + players[1].CurrentHealth, new Vector2(players[1].Position.X - (player1Sprite.Width / 2) + 10, players[1].Position.Y - (player1Sprite.Height/2)), Color.Red);
            spriteBatch.DrawString(hpPlayer1, "Player 3 HP: " + players[2].CurrentHealth, new Vector2(players[2].Position.X - (player2Sprite.Width / 2), players[2].Position.Y - player2Sprite.Height), Color.Red);
            spriteBatch.DrawString(hpPlayer1, "Enemy 1 HP: " + enemies[0].CurrentHealth, new Vector2(enemies[0].Position.X - (enemy0Sprite.Width / 5), enemies[0].Position.Y - (enemy0Sprite.Height / 2)), Color.Red);
            spriteBatch.DrawString(hpPlayer1, "Enemy 2 HP: " + enemies[1].CurrentHealth, new Vector2(enemies[1].Position.X - (enemy1Sprite.Width / 5), enemies[1].Position.Y - (enemy1Sprite.Height / 2)), Color.Red);
            spriteBatch.DrawString(hpPlayer1, "Enemy 3 HP: " + enemies[2].CurrentHealth, new Vector2(enemies[2].Position.X - (enemy2Sprite.Width / 5), enemies[2].Position.Y - (enemy2Sprite.Height / 2)), Color.Red);

            spriteBatch.DrawString(hpPlayer1,"TurnCounter: " + tracker,new Vector2(ScreenManager.ScreenDimensions.X/2, ScreenManager.ScreenDimensions.Y/2),Color.Green);
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
            Console.WriteLine("PlayerSpeed =" + chosenPlayer);
            enemies[targetedEnemy].TakeDamage(playerDamageAmount);

            tracker = BattleTracker.SpeedEvaluationPlayer;
            Console.WriteLine(enemies[targetedEnemy].CurrentHealth);
            enemyTarget = 0;
            //Console.ReadKey();

            if (playerSpeed == 1)
            {
                Console.WriteLine("Player did damage!");
                EncounterTurnTwo();
            }
            else if ((enemySpeed == 1 && playerSpeed == 1) || playerSpeed == 2)
            {
                Console.WriteLine("Player did damage!");
                EncounterTurnThree();
            }
            else if ((enemySpeed == 2 && playerSpeed == 1) || (enemySpeed == 1 && playerSpeed == 2) || playerSpeed == 3)
            {
                Console.WriteLine("Player did damage!");
                EncounterTurnFour();
            }
            else if ((playerSpeed == 3 && enemySpeed == 1) || (enemySpeed == 3 && playerSpeed == 1) || (playerSpeed == 2 && enemySpeed == 2))
            {
                Console.WriteLine("Player did damage!");
                EncounterTurnFive();
            }
            else if ((enemySpeed == 2 && playerSpeed == 3) || (enemySpeed == 3 && playerSpeed == 2))
            {
                Console.WriteLine("Player did damage!");
                EncounterTurnSix();
            }
        }

        public override void HandleInput()
        {

            if(tracker != BattleTracker.Start)
            {
                return;
            }

            KeyboardState keyboard = Keyboard.GetState();

            if(keyboard.IsKeyDown(Keys.D1))
            {
                enemyTarget = 1;
                //Console.WriteLine(enemyTarget);
            }

            if(keyboard.IsKeyDown(Keys.D2))
            {
                enemyTarget = 2;
            }

            if(keyboard.IsKeyDown(Keys.D3))
            {
                enemyTarget = 3;
            }

            if(keyboard.IsKeyDown(Keys.D) && enemyTarget > 0)
            {
                Console.WriteLine("PlayerTargetLaunched");
                enemyTarget--;
                tracker = BattleTracker.Playerturn;
                PlayerTarget(playerSpeed, enemyTarget);
            }
            
        }

        void Enemyturn()
        {
            if(tracker != BattleTracker.Enemyturn)
            {
                return;
            }

            targetedPlayer = rnd.Next(0,3);
            //targetedPlayer = 0;
            tracker = BattleTracker.Enemyattack;
            EnemyAttack(targetedPlayer,enemySpeed,0);
        }

        void EnemyAttack(int targetedPlayer, int chosenEnemy, int enemyDamageAmount)
        {

            if(tracker != BattleTracker.Enemyattack)
            {
                return;
            }
            
            enemyDamageAmount = enemies[chosenEnemy].Damage;
            Console.WriteLine("EnemySpeed =" + chosenEnemy);
            players[targetedPlayer].TakeDamage(enemyDamageAmount);
            tracker = BattleTracker.SpeedEvaluationEnemy;

            if(enemySpeed == 1)
            {
                Console.WriteLine("Enemy Turn Two Active!");
                EncounterTurnTwo();
            }
            else if((enemySpeed == 1 && playerSpeed == 1) || enemySpeed == 2)
            {
                Console.WriteLine("Enemy Turn Three Active!");
                EncounterTurnThree();
            }
            else if((enemySpeed == 2 && playerSpeed == 1) || (enemySpeed == 1 && playerSpeed == 2) || enemySpeed == 3)
            {
                Console.WriteLine("Enemy Turn Four Active!");
                EncounterTurnFour();
            }
            else if((enemySpeed == 3 && playerSpeed == 1) || (playerSpeed == 3 && enemySpeed == 1) || (enemySpeed == 2 && playerSpeed == 2))
            {
                Console.WriteLine("Enemy did damage!");
                EncounterTurnFive();
            }
            else if((enemySpeed == 2 && playerSpeed == 3) || (enemySpeed == 3 && playerSpeed == 2))
            {
                Console.WriteLine("Enemy did damage!");
                EncounterTurnSix();
            }
            
        }

        void PlayerSpeedCheck()
        {


            //Console.WriteLine(players[0].Turnspeed);
            //Console.WriteLine(players[1].Turnspeed);
            //Console.WriteLine(players[2].Turnspeed);
            //Console.ReadKey();

            //if (players[0].Turnspeed > players[1].Turnspeed && players[0].Turnspeed > players[2].Turnspeed)
            //{
            //    playerSpeed = players[0].Turnspeed;
            //}
            //else if (players[1].Turnspeed > players[2].Turnspeed && players[1].Turnspeed > players[0].Turnspeed)
            //{
            //    playerSpeed = players[1].Turnspeed;
            //}
            //else if (players[2].Turnspeed > players[1].Turnspeed && players[2].Turnspeed > players[0].Turnspeed)
            //{
            //    playerSpeed = players[2].Turnspeed;
            //}

            for (int i = 0; i < players.Length - 1; i++)
            {
                for (int j = 0; j < players.Length - 1; j++)
                {
                    if(players[j].Turnspeed < players[j+1].Turnspeed)
                    {

                        Character temp = players[j];
                        players[j] = players[j + 1];
                        players[j + 1] = temp;

                    }
                }
            }

            //Console.WriteLine(players[0].Turnspeed);
            //Console.WriteLine(players[1].Turnspeed);
            //Console.WriteLine(players[2].Turnspeed);
            //Console.ReadKey();
            EnemySpeedCheck();

        }

        void EnemySpeedCheck()
        {

            //if(enemies[0].Turnspeed > enemies[1].Turnspeed && enemies[0].Turnspeed > enemies[2].Turnspeed)
            //{
            //    enemySpeed = enemies[0].Turnspeed;
            //}
            //else if(enemies[1].Turnspeed > enemies[2].Turnspeed && enemies[1].Turnspeed > enemies[0].Turnspeed)
            //{
            //    enemySpeed = enemies[1].Turnspeed;
            //}
            //else if(enemies[2].Turnspeed > enemies[1].Turnspeed && enemies[2].Turnspeed > enemies[0].Turnspeed)
            //{
            //    enemySpeed = enemies[2].Turnspeed;
            //}

            for (int i = 0; i < enemies.Length - 1; i++)
            {
                for (int j = 0; j < enemies.Length - 1; j++)
                {
                    if (enemies[j].Turnspeed < enemies[j + 1].Turnspeed)
                    {

                        Character temp = enemies[j];
                        enemies[j] = enemies[j + 1];
                        enemies[j + 1] = temp;

                    }
                }
            }

            EncounterTurnOne();
        }

        void EncounterTurnOne()
        {
            if (firstTurn != true)
            {
                sixthTurn = false;
                firstTurn = true;
                if (players[0].Turnspeed > enemies[0].Turnspeed)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if (enemies[0].Turnspeed > players[0].Turnspeed)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
            }
        }

        void EncounterTurnTwo()
        {
            if (firstTurn == true && secoundTurn != true)
            {
                firstTurn = false;
                secoundTurn = true;
                if (players[0].Turnspeed > enemies[1].Turnspeed && playerSpeed == 0)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if (enemies[0].Turnspeed > players[1].Turnspeed && enemySpeed == 0)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
                else if (players[1].Turnspeed > enemies[0].Turnspeed && playerSpeed == 1)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if (enemies[1].Turnspeed > players[0].Turnspeed && enemySpeed == 1)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
            }
        }

        void EncounterTurnThree()
        {
            if (secoundTurn == true && thirdTurn != true)
            {
                secoundTurn = false;
                thirdTurn = true;
                if (players[0].Turnspeed > enemies[1].Turnspeed && playerSpeed == 0)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if (enemies[0].Turnspeed > players[1].Turnspeed && enemySpeed == 0)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
                else if (players[0].Turnspeed > enemies[2].Turnspeed && playerSpeed == 0)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if (enemies[0].Turnspeed > players[2].Turnspeed && enemySpeed == 0)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
                else if (players[1].Turnspeed > enemies[0].Turnspeed && playerSpeed == 1)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if (enemies[1].Turnspeed > players[0].Turnspeed && enemySpeed == 1)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
                else if (players[1].Turnspeed > enemies[1].Turnspeed && playerSpeed == 1)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if (enemies[1].Turnspeed > players[1].Turnspeed && enemySpeed == 1)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
                else if (players[2].Turnspeed > enemies[0].Turnspeed && playerSpeed == 2)
                {
                    tracker = BattleTracker.Playerturn;
                    playerSpeed++;
                }
                else if (enemies[2].Turnspeed > players[0].Turnspeed && enemySpeed == 2)
                {
                    tracker = BattleTracker.Playerturn;
                    enemySpeed++;
                }
            }
        }

        void EncounterTurnFour()
        {
            if (thirdTurn == true && fourthTurn != true)
            {
                thirdTurn = false;
                fourthTurn = true;
                if (enemySpeed == 3)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if (playerSpeed == 3)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
                else if (players[2].Turnspeed > enemies[1].Turnspeed && playerSpeed == 2)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if (enemies[2].Turnspeed > players[1].Turnspeed && enemySpeed == 2)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
                else if (players[1].Turnspeed > enemies[2].Turnspeed && playerSpeed == 1)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if (enemies[1].Turnspeed > players[2].Turnspeed && enemySpeed == 1)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
            }
        }

        void EncounterTurnFive()
        {
            if (fourthTurn == true && fifthTurn != true)
            {
                fourthTurn = false;
                fifthTurn = true;
                if (enemySpeed == 3 && playerSpeed == 1)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if (playerSpeed == 3 && enemySpeed == 1)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
                else if (players[2].Turnspeed > enemies[2].Turnspeed && playerSpeed == 2)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if (enemies[2].Turnspeed > players[2].Turnspeed && enemySpeed == 2)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
            }
        }

        void EncounterTurnSix()
        {
            if (fifthTurn == true && sixthTurn != true)
            {
                fifthTurn = false;
                sixthTurn = true;
                if (enemySpeed == 3 && playerSpeed == 2)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if (playerSpeed == 3 && enemySpeed == 2)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
            }
        }

        void EncounterTurnSystemReset()
        {
            if(playerSpeed == 3 && enemySpeed == 3)
            {
                playerSpeed = 0;
                enemySpeed = 0;
            }
        }

        void EncounterStart()
        {

            //if(playerSpeed > enemySpeed)
            //{
            //    tracker = BattleTracker.Start;
            //}
            //else if(enemySpeed > playerSpeed)
            //{
            //    tracker = BattleTracker.Enemyturn;
            //}

            if(sixthTurn == true && playerSpeed == 3 && enemySpeed == 3)
            {
                playerSpeed = 0;
                enemySpeed = 0;
                firstTurn = false;
                secoundTurn = false;
                thirdTurn = false;
                fourthTurn = false;
                fifthTurn = false;
                sixthTurn = false;
            }

            if(sixthTurn == true)
            {
                return;
            }

            if(firstTurn != true)
            {
                firstTurn = true;
                if(players[0].Turnspeed > enemies[0].Turnspeed)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if(enemies[0].Turnspeed > players[0].Turnspeed)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
            }
            else if(firstTurn == true && secoundTurn != true)
            {
                secoundTurn = true;
                if(players[0].Turnspeed > enemies[1].Turnspeed && playerSpeed == 0)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if(enemies[0].Turnspeed > players[1].Turnspeed && enemySpeed == 0)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
                else if(players[1].Turnspeed > enemies[0].Turnspeed && playerSpeed == 1)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if(enemies[1].Turnspeed > players[0].Turnspeed && enemySpeed == 1)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
            }
            else if(secoundTurn == true && thirdTurn != true)
            {
                thirdTurn = true;
                if (players[0].Turnspeed > enemies[1].Turnspeed && playerSpeed == 0)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if (enemies[0].Turnspeed > players[1].Turnspeed && enemySpeed == 0)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
                else if(players[0].Turnspeed > enemies[2].Turnspeed && playerSpeed == 0)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if(enemies[0].Turnspeed > players[2].Turnspeed && enemySpeed == 0)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
                else if (players[1].Turnspeed > enemies[0].Turnspeed && playerSpeed == 1)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if (enemies[1].Turnspeed > players[0].Turnspeed && enemySpeed == 1)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
                else if(players[1].Turnspeed > enemies[1].Turnspeed && playerSpeed == 1)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if(enemies[1].Turnspeed > players[1].Turnspeed && enemySpeed == 1)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
                else if(players[2].Turnspeed > enemies[0].Turnspeed && playerSpeed == 2)
                {
                    tracker = BattleTracker.Playerturn;
                    playerSpeed++;
                }
                else if(enemies[2].Turnspeed > players[0].Turnspeed && enemySpeed == 2)
                {
                    tracker = BattleTracker.Playerturn;
                    enemySpeed++;
                }
            }
            else if(thirdTurn == true && fourthTurn != true)
            {
                fourthTurn = true;
                if(enemySpeed == 3)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed = 1;
                }
                else if(playerSpeed == 3)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed = 1;
                }
                else if(players[2].Turnspeed > enemies[1].Turnspeed && playerSpeed == 2)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if(enemies[2].Turnspeed > players[1].Turnspeed && enemySpeed == 2)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
                else if(players[1].Turnspeed > enemies[2].Turnspeed && playerSpeed == 1)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if(enemies[1].Turnspeed > players[2].Turnspeed && enemySpeed == 1)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
            }
            else if(fourthTurn == true && fifthTurn != true)
            {
                fifthTurn = true;
                if(enemySpeed == 3 && playerSpeed == 1)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if(playerSpeed == 3 && enemySpeed == 1)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
                else if(players[2].Turnspeed > enemies[2].Turnspeed && playerSpeed == 2)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if(enemies[2].Turnspeed > players[2].Turnspeed && enemySpeed == 2)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
            }
            else if(fifthTurn == true && sixthTurn != true)
            {
                sixthTurn = true;
                if(enemySpeed == 3 && playerSpeed == 2)
                {
                    tracker = BattleTracker.Start;
                    playerSpeed++;
                }
                else if(playerSpeed == 3 && enemySpeed ==2)
                {
                    tracker = BattleTracker.Enemyturn;
                    enemySpeed++;
                }
            }

        }
    }
}
