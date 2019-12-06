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
    enum BattleTracker { Playerturn,Playerattack, Enemyturn, Enemyattack, start }

    class BattleScreen : GameScreen
    {
        BattleTracker tracker;
        Song song;
        Random rnd;

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
        public int ExpValue { get => expValue; }

        //Background image for the battle screen.
        private Texture2D background;
        private string path = "BattleScreen/Background";

        //Textures for enemy and player characters.
        private Texture2D enemy0Sprite, enemy1Sprite, enemy2Sprite, player0Sprite, player1Sprite, player2Sprite;        private SpriteFont hp;
        private string hpOnScreen = "hpOnScreen";        int targetedPlayer;

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
            HandleInput();
            Enemyturn();
            DefaultAnimate(gameTime);
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

            spriteBatch.DrawString(hp, "HP: " + players[0].CurrentHealth, new Vector2(players[0].Position.X,players[0].Position.Y),Color.Red);
            spriteBatch.DrawString(hp, "HP: " + enemies[0].CurrentHealth, new Vector2(enemies[0].Position.X, enemies[0].Position.Y), Color.Red);
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
                timeElasped = 0;
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

        protected void AttackAnimate(GameTime gameTime)
        {
            
        }
    }
}
