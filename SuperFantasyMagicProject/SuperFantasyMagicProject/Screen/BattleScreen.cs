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

        protected float fps=4;
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
        private string hpOnScreen = "hpOnScreen";        
                private SpriteFont hp;
                int targetedPlayer;

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

            spriteBatch.DrawString(hp, "Player 1 HP: " + players[0].CurrentHealth, new Vector2(players[0].Position.X - (player0Sprite.Width / 2), players[0].Position.Y - player0Sprite.Height), Color.Red);
            spriteBatch.DrawString(hp, "Player 2 HP: " + players[1].CurrentHealth, new Vector2(players[1].Position.X - (player1Sprite.Width / 2) + 10, players[1].Position.Y - (player1Sprite.Height/2)), Color.Red);
            spriteBatch.DrawString(hp, "Player 3 HP: " + players[2].CurrentHealth, new Vector2(players[2].Position.X - (player2Sprite.Width / 2), players[2].Position.Y - player2Sprite.Height), Color.Red);
            spriteBatch.DrawString(hp, "Enemy 1 HP: " + enemies[0].CurrentHealth, new Vector2(enemies[0].Position.X - (enemy0Sprite.Width / 5), enemies[0].Position.Y - (enemy0Sprite.Height / 2)), Color.Red);
            spriteBatch.DrawString(hp, "Enemy 2 HP: " + enemies[1].CurrentHealth, new Vector2(enemies[1].Position.X - (enemy1Sprite.Width / 5), enemies[1].Position.Y - (enemy1Sprite.Height / 2)), Color.Red);
            spriteBatch.DrawString(hp, "Enemy 3 HP: " + enemies[2].CurrentHealth, new Vector2(enemies[2].Position.X - (enemy2Sprite.Width / 5), enemies[2].Position.Y - (enemy2Sprite.Height / 2)), Color.Red);

            spriteBatch.DrawString(hp,"TurnCounter: " + tracker,new Vector2(ScreenManager.ScreenDimensions.X/2, ScreenManager.ScreenDimensions.Y/2),Color.Green);
            spriteBatch.DrawString(hp, "HP: " + players[0].CurrentHealth, new Vector2(players[0].Position.X,players[0].Position.Y),Color.Red);
            
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
