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
using Microsoft.Xna.Framework.Media;

namespace SuperFantasyMagicProject.Screen
{
    enum BattleTracker { Playerturn, Playerattack, Enemyturn, Enemyattack, Start, SpeedEvaluationPlayer, SpeedEvaluationEnemy }

    class BattleScreen : GameScreen
    {
        BattleTracker tracker;

        Random rnd = new Random();
        Song song;

        //Players
        protected Texture2D[] knightStanding;
        protected Texture2D[] jeremyStanding;
        protected Texture2D[] marthaStanding;

        //Enemies
        protected Texture2D[] batStanding;
        protected Texture2D[] demonFlowerStanding;
        protected Texture2D[] hayuStanding;
        protected Texture2D[] hornetStanding;
        protected Texture2D[] sangshiStanding;
        protected Texture2D[] scorpionStanding;

        protected float fps = 4;
        private float timeElasped;
        private int currentIndex;

        private int expValue;
        int enemyTarget = 0;
        public int ExpValue { get => expValue; private set => expValue = value; }

        //Background image for the battle screen.
        private Texture2D background;
        private string path = "BattleScreen/Background";

        //Textures for enemy and player characters.
        private Texture2D enemy0Sprite, enemy1Sprite, enemy2Sprite, player0Sprite, player1Sprite, player2Sprite;        private SpriteFont hpPlayer1;
        private string hpOnScreen = "hpOnScreen";        private SpriteFont hp;
                int targetedPlayer;

        int playerSpeed;
        int enemySpeed;

        bool speedChecked = false;
        bool combatStarted = false;

        bool firstTurn = false;
        bool secoundTurn = false;
        bool thirdTurn = false;
        bool fourthTurn = false;
        bool fifthTurn = false;
        bool sixthTurn = false;

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
            tracker = BattleTracker.Start;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            background = gameScreenContent.Load<Texture2D>(path);
            this.song = gameScreenContent.Load<Song>("Final Fantasy VI Battle Theme Extended");
            MediaPlayer.Play(song);
            //Code for music looping
            //MediaPlayer.IsRepeating = true;

            //The size definition of the arrays for the creatures/characters
            knightStanding = new Texture2D[4];
            jeremyStanding = new Texture2D[3];
            marthaStanding = new Texture2D[3];
            batStanding = new Texture2D[3];
            demonFlowerStanding = new Texture2D[4];
            hayuStanding = new Texture2D[3];
            hornetStanding = new Texture2D[3];
            sangshiStanding = new Texture2D[3];
            scorpionStanding = new Texture2D[3];

            //Loads the sprites of the Jeremy into an array
            for (int i = 0; i < jeremyStanding.Length; i++)
            {
                jeremyStanding[i] = gameScreenContent.Load<Texture2D>("Player/Jeremy/Jeremy blonde/JeremyBlondWalk/JeremyBlondWalkRight" + (i + 1));
            }

            //Loads the sprites of the Knight into an array
            for (int i = 0; i < knightStanding.Length; i++)
            {
                knightStanding[i] = gameScreenContent.Load<Texture2D>("Player/Knight/Standing/KnightStanding" + (i + 1));
            }

            //Loads the sprites of the Martha into an array
            for (int i = 0; i < marthaStanding.Length; i++)
            {
                marthaStanding[i] = gameScreenContent.Load<Texture2D>("Player/Martha/Martha blonde/MarthaBlondeWalk/MarthaBlondeWalkRight" + (i + 1));
            }

            //Loads the sprites of the Bat into an array
            for (int i = 0; i < marthaStanding.Length; i++)
            {
                batStanding[i] = gameScreenContent.Load<Texture2D>("Enemies/Bat/Pink/Animation 1/PinkBat1." + (i + 1));
            }

            //Loads the sprites of the Demon Flower into an array
            for (int i = 0; i < demonFlowerStanding.Length; i++)
            {
                demonFlowerStanding[i] = gameScreenContent.Load<Texture2D>("Enemies/Demon flowers/Purple/Animation 1/DemonFlower1." + (i + 1));
            }

            //Loads the sprites of the Hayu into an array
            for (int i = 0; i < hayuStanding.Length; i++)
            {
                hayuStanding[i] = gameScreenContent.Load<Texture2D>("Enemies/Hayu/Blue/Animation 1/Hayu1." + (i + 1));
            }

            //Loads the sprites of the Hornet into an array
            for (int i = 0; i < hornetStanding.Length; i++)
            {
                hornetStanding[i] = gameScreenContent.Load<Texture2D>("Enemies/Hornet/Yellow/Animation 1/Hornet1." + (i + 1));
            }

            //Loads the sprites of the Sangshi into an array
            for (int i = 0; i < sangshiStanding.Length; i++)
            {
                sangshiStanding[i] = gameScreenContent.Load<Texture2D>("Enemies/Sangshi/Green/Animation 1/Sangshi1." + (i + 1));
            }

            //Loads the sprites of the Scorpion into an array
            for (int i = 0; i < scorpionStanding.Length; i++)
            {
                scorpionStanding[i] = gameScreenContent.Load<Texture2D>("Enemies/Scorpion/Black/Animation 1/Scorpion1." + (i + 1));
            }

            //Load textures (players/enemies/hpOnScreen).
            player0Sprite = jeremyStanding[currentIndex];
            player1Sprite = knightStanding[currentIndex];
            player2Sprite = marthaStanding[currentIndex];
            enemy0Sprite = batStanding[currentIndex];
            enemy1Sprite = batStanding[currentIndex];
            enemy2Sprite = batStanding[currentIndex];            hp = gameScreenContent.Load<SpriteFont>(hpOnScreen);
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
            SpeedCheck();
            DefaultAnimate(gameTime);
            HandleInput();
            Enemyturn();
            EncounterTurnOne();
            EncounterTurnTwo();
            EncounterTurnThree();
            EncounterTurnFour();
            EncounterTurnFive();
            EncounterTurnSix();
            EncounterTurnSystemReset();
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
            spriteBatch.Draw(enemy0Sprite, enemies[0].Position, new Rectangle(0, 0, enemy0Sprite.Width, enemy0Sprite.Height),
                    Color.White, 0, enemies[0].Origin, 1f, SpriteEffects.None, 1f);
            spriteBatch.Draw(enemy1Sprite, enemies[1].Position, new Rectangle(0, 0, enemy1Sprite.Width, enemy1Sprite.Height),
                    Color.White, 0, enemies[1].Origin, 1f, SpriteEffects.None, 1f);
            spriteBatch.Draw(enemy2Sprite, enemies[2].Position, new Rectangle(0, 0, enemy2Sprite.Width, enemy2Sprite.Height),
                    Color.White, 0, enemies[2].Origin, 1f, SpriteEffects.None, 1f);

            spriteBatch.DrawString(hp, "Player 1 HP: " + players[0].CurrentHealth, new Vector2(players[0].Position.X - (player0Sprite.Width / 2), players[0].Position.Y - player0Sprite.Height), Color.Red);
            spriteBatch.DrawString(hp, "Player 2 HP: " + players[1].CurrentHealth, new Vector2(players[1].Position.X - (player1Sprite.Width / 2) + 10, players[1].Position.Y - (player1Sprite.Height / 2)), Color.Red);
            spriteBatch.DrawString(hp, "Player 3 HP: " + players[2].CurrentHealth, new Vector2(players[2].Position.X - (player2Sprite.Width / 2), players[2].Position.Y - player2Sprite.Height), Color.Red);
            spriteBatch.DrawString(hp, "Enemy 1 HP: " + enemies[0].CurrentHealth, new Vector2(enemies[0].Position.X - (enemy0Sprite.Width / 5), enemies[0].Position.Y - (enemy0Sprite.Height / 2)), Color.Red);
            spriteBatch.DrawString(hp, "Enemy 2 HP: " + enemies[1].CurrentHealth, new Vector2(enemies[1].Position.X - (enemy1Sprite.Width / 5), enemies[1].Position.Y - (enemy1Sprite.Height / 2)), Color.Red);
            spriteBatch.DrawString(hp, "Enemy 3 HP: " + enemies[2].CurrentHealth, new Vector2(enemies[2].Position.X - (enemy2Sprite.Width / 5), enemies[2].Position.Y - (enemy2Sprite.Height / 2)), Color.Red);

            spriteBatch.DrawString(hp, "TurnCounter: " + tracker, new Vector2(ScreenManager.ScreenDimensions.X / 2, ScreenManager.ScreenDimensions.Y / 2), Color.Green);
            spriteBatch.DrawString(hp, "HP: " + players[0].CurrentHealth, new Vector2(players[0].Position.X, players[0].Position.Y), Color.Red);

        }

        void PlayerTarget(int chosenPlayer, int targetedEnemy)
        {
            if (tracker != BattleTracker.Playerturn)
            {
                return;
            }

            tracker = BattleTracker.Playerattack;
            PlayerAttack(targetedEnemy,chosenPlayer);
        }

        void PlayerAttack(int targetedEnemy, int chosenPlayer)
        {
            if (tracker != BattleTracker.Playerattack)
            {
                return;
            }

            int playerDamageAmount = players[chosenPlayer].Damage;
            Console.WriteLine("PlayerSpeed =" + chosenPlayer);
            enemies[targetedEnemy].TakeDamage(playerDamageAmount);
            combatStarted = true;
            Console.WriteLine(enemies[targetedEnemy].CurrentHealth);
            enemyTarget = 0;
        }

        public override void HandleInput()
        {

            if (tracker != BattleTracker.Start)
            {
                return;
            }

            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.D1))
            {
                enemyTarget = 1;
            }

            if (keyboard.IsKeyDown(Keys.D2))
            {
                enemyTarget = 2;
            }

            if (keyboard.IsKeyDown(Keys.D3))
            {
                enemyTarget = 3;
            }

            if (keyboard.IsKeyDown(Keys.D) && enemyTarget > 0)
            {
                Console.WriteLine("PlayerTargetLaunched");
                enemyTarget--;
                tracker = BattleTracker.Playerturn;
                PlayerTarget(playerSpeed-1, enemyTarget);
            }

        }

        void Enemyturn()
        {
            if (tracker != BattleTracker.Enemyturn)
            {
                return;
            }

            targetedPlayer = rnd.Next(0,3);
            tracker = BattleTracker.Enemyattack;
            EnemyAttack(targetedPlayer,enemySpeed-1,0);
        }

        void EnemyAttack(int targetedPlayer, int chosenEnemy, int enemyDamageAmount)
        {

            if (tracker != BattleTracker.Enemyattack)
            {
                return;
            }
            combatStarted = true;
            enemyDamageAmount = enemies[chosenEnemy].Damage;
            Console.WriteLine("EnemySpeed =" + chosenEnemy);
            players[targetedPlayer].TakeDamage(enemyDamageAmount);
            Console.WriteLine("player dmg take: " + enemyDamageAmount);
        }

        void SpeedCheck()
        {
            if(speedChecked == false)
            {
                speedChecked = true;
                for (int i = 0; i < players.Length - 1; i++)
                {
                    for (int j = 0; j < players.Length - 1; j++)
                    {
                        if (players[j].Turnspeed < players[j + 1].Turnspeed)
                        {

                            Character temp = players[j];
                            players[j] = players[j + 1];
                            players[j + 1] = temp;

                        }
                    }
                }

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

                combatStarted = true;

            }
        }
        void EncounterTurnOne()
        {
            if(combatStarted == true)
            {
                if (players[0].Turnspeed > enemies[0].Turnspeed && firstTurn == false && playerSpeed == 0)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnOne Active player");
                    firstTurn = true;
                    playerSpeed++;
                    tracker = BattleTracker.Start;
                }
                else if(enemies[0].Turnspeed > players[0].Turnspeed && firstTurn == false && enemySpeed == 0)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnOne Active enemy");
                    firstTurn = true;
                    enemySpeed++;
                    tracker = BattleTracker.Enemyturn;
                }
            }
        }
        void EncounterTurnTwo()
        {
            if (combatStarted == true && firstTurn == true)
            {
                if (enemies[0].Turnspeed > players[1].Turnspeed && secoundTurn == false && enemySpeed == 0)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnTwo Active");
                    secoundTurn = true;
                    enemySpeed++;
                    tracker = BattleTracker.Enemyturn;
                }
                else if (players[0].Turnspeed > enemies[1].Turnspeed && secoundTurn == false && playerSpeed == 0)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnTwo Active");
                    secoundTurn = true;
                    playerSpeed++;
                    tracker = BattleTracker.Start;
                }
                else if(enemies[1].Turnspeed > players[0].Turnspeed && secoundTurn == false && enemySpeed == 1)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnTwo Active");
                    secoundTurn = true;
                    enemySpeed++;
                    tracker = BattleTracker.Enemyturn;
                }
                else if(players[1].Turnspeed > enemies[0].Turnspeed && secoundTurn == false && playerSpeed == 1)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnTwo Active");
                    secoundTurn = true;
                    playerSpeed++;
                    tracker = BattleTracker.Start;
                }
            }
        }
        void EncounterTurnThree()
        {
            if(combatStarted == true && secoundTurn == true)
            {
                if(players[0].Turnspeed > enemies[2].Turnspeed && thirdTurn == false && playerSpeed == 0)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnThree Active");
                    thirdTurn = true;
                    playerSpeed++;
                    tracker = BattleTracker.Start;
                }
                else if(enemies[0].Turnspeed > players[2].Turnspeed && thirdTurn == false && enemySpeed == 0)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnThree Active");
                    thirdTurn = true;
                    enemySpeed++;
                    tracker = BattleTracker.Enemyturn;
                }
                else if(players[1].Turnspeed > enemies[1].Turnspeed && thirdTurn == false && playerSpeed == 1)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnThree Active");
                    thirdTurn = true;
                    playerSpeed++;
                    tracker = BattleTracker.Start;
                }
                else if(enemies[1].Turnspeed > players[1].Turnspeed && thirdTurn == false && enemySpeed == 1)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnThree Active");
                    thirdTurn = true;
                    enemySpeed++;
                    tracker = BattleTracker.Enemyturn;
                }
                else if(players[2].Turnspeed > enemies[0].Turnspeed && thirdTurn == false && playerSpeed == 2)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnThree Active");
                    thirdTurn = true;
                    playerSpeed++;
                    tracker = BattleTracker.Start;
                }
                else if(enemies[2].Turnspeed > players[0].Turnspeed && thirdTurn == false && enemySpeed == 2)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnThree Active");
                    thirdTurn = true;
                    enemySpeed++;
                    tracker = BattleTracker.Enemyturn;
                }
            }
        }
        void EncounterTurnFour()
        {
            if(combatStarted == true && thirdTurn == true)
            {
                if(enemySpeed == 3 && fourthTurn == false)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnFour Active");
                    fourthTurn = true;
                    playerSpeed++;
                    tracker = BattleTracker.Start;
                }
                else if(playerSpeed == 3 && fourthTurn == false)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnFour Active");
                    fourthTurn = true;
                    enemySpeed++;
                    tracker = BattleTracker.Enemyturn;
                }
                else if(players[2].Turnspeed > enemies[1].Turnspeed && fourthTurn == false && playerSpeed == 2)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnFour Active");
                    fourthTurn = true;
                    playerSpeed++;
                    tracker = BattleTracker.Start;
                }
                else if(enemies[2].Turnspeed > players[1].Turnspeed && fourthTurn == false && enemySpeed == 2)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnFour Active");
                    fourthTurn = true;
                    enemySpeed++;
                    tracker = BattleTracker.Enemyturn;
                }
                else if(players[1].Turnspeed > enemies[2].Turnspeed && fourthTurn == false && playerSpeed == 1)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnFour Active");
                    fourthTurn = true;
                    playerSpeed++;
                    tracker = BattleTracker.Start;
                }
                else if(enemies[1].Turnspeed > players[2].Turnspeed && fourthTurn == false && enemySpeed == 1)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnFour Active");
                    fourthTurn = true;
                    enemySpeed++;
                    tracker = BattleTracker.Enemyturn;
                }
            }
        }
        void EncounterTurnFive()
        {
            if(combatStarted == true && fourthTurn == true)
            {
                if(enemySpeed == 3 && playerSpeed == 1 && fifthTurn == false)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnFive Active");
                    fifthTurn = true;
                    playerSpeed = 2;
                    tracker = BattleTracker.Start;
                }
                else if(playerSpeed == 3 && enemySpeed == 1 && fifthTurn == false)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnFive Active");
                    fifthTurn = true;
                    enemySpeed = 2;
                    tracker = BattleTracker.Enemyturn;
                }
                else if(players[2].Turnspeed > enemies[2].Turnspeed && fifthTurn == false && playerSpeed == 2)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnFive Active");
                    fifthTurn = true;
                    playerSpeed = 3;
                    tracker = BattleTracker.Start;
                }
                else if(enemies[2].Turnspeed > players[2].Turnspeed && fifthTurn == false && enemySpeed == 2)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnFive Active");
                    fifthTurn = true;
                    enemySpeed = 3;
                    tracker = BattleTracker.Enemyturn;
                }
            }
        }
        void EncounterTurnSix()
        {
            if(combatStarted == true && fifthTurn == true)
            {
                if(playerSpeed == 3 && enemySpeed == 2 && sixthTurn == false)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnSix Active");
                    sixthTurn = true;
                    enemySpeed = 3;
                    tracker = BattleTracker.Enemyturn;
                }
                else if(enemySpeed == 3 && playerSpeed == 2 && sixthTurn == false)
                {
                    combatStarted = false;
                    Console.WriteLine("EncounterTurnSix Active");
                    sixthTurn = true;
                    playerSpeed = 3;
                    tracker = BattleTracker.Start;
                }
            }
        }
        void EncounterTurnSystemReset()
        {
            if(combatStarted == true && playerSpeed == 3 && enemySpeed == 3)
            {
                firstTurn = false;
                secoundTurn = false;
                thirdTurn = false;
                fourthTurn = false;
                fifthTurn = false;
                sixthTurn = false;
                playerSpeed = 0;
                enemySpeed = 0;
                speedChecked = false;
            }
        }

        /// <summary>
        /// Animates the different sprites (Martha, Jeremy, Knight and Bat)
        /// </summary>
        /// <param name="gameTime"></param>
        protected void DefaultAnimate(GameTime gameTime)
        {
            //Counts the time since the last update
            timeElasped += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Calculate the current index for the array
            currentIndex = (int)(timeElasped * fps);

            //Sets the sprite to the current index for all the arrays
            player0Sprite = jeremyStanding[currentIndex];
            player1Sprite = knightStanding[currentIndex];
            player2Sprite = marthaStanding[currentIndex];
            enemy0Sprite = batStanding[currentIndex];
            enemy1Sprite = hornetStanding[currentIndex];
            enemy2Sprite = demonFlowerStanding[currentIndex];

            //Checks if the animation needs to be reset
            if (currentIndex >= jeremyStanding.Length - 1)
            {
                //Resets the animation
                timeElasped = 0;
                currentIndex = 0;
            }

            if (currentIndex >= knightStanding.Length - 1)
            {
                timeElasped = 0;
                currentIndex = 0;
            }

            if (currentIndex >= marthaStanding.Length - 1)
            {
                timeElasped = 0;
                currentIndex = 0;
            }
            if (currentIndex >= batStanding.Length - 1)
            {
                timeElasped = 1;
                currentIndex = 0;
            }
            if (currentIndex >= demonFlowerStanding.Length - 1)
            {
                timeElasped = 0;
                currentIndex = 0;
            }
            if (currentIndex >= hayuStanding.Length - 1)
            {
                timeElasped = 0;
                currentIndex = 0;
            }
            if (currentIndex >= hornetStanding.Length - 1)
            {
                timeElasped = 0;
                currentIndex = 0;
            }
            if (currentIndex >= sangshiStanding.Length - 1)
            {
                timeElasped = 0;
                currentIndex = 0;
            }
            if (currentIndex >= scorpionStanding.Length - 1)
            {
                timeElasped = 0;
                currentIndex = 0;
            }
        }

    }
}
