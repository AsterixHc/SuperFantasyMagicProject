using System;
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
        private Texture2D enemy0Sprite;
        private Texture2D enemy1Sprite;
        private Texture2D enemy2Sprite;
        private Texture2D player0Sprite;

        private SpriteFont hpPlayer1;
        private string hpOnScreen = "hpOnScreen";
        private int hp = 100;


        //Path to the background image.
        private string path = "BattleScreen/Background";

        //Array for holding enemies
        Character[] enemies = new Character[3];
        Character[] players = new Character[3];

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
        public BattleScreen(Character enemy0, Character enemy1, Character enemy2, Character player0)
        {
            enemies[0] = enemy0;
            enemies[1] = enemy1;
            enemies[2] = enemy2;
            players[0] = player0;
            players[0].Position = new Vector2(182, 160);
            enemies[0].Position = new Vector2(1610, 160);
            enemies[1].Position = new Vector2(1610, 400);
            enemies[2].Position = new Vector2(1610, 640);
            
        }

        public override void LoadContent()
        {
            base.LoadContent();
            background = gameScreenContent.Load<Texture2D>(path);
            enemy0Sprite = gameScreenContent.Load<Texture2D>(enemies[0].Path);
            enemy1Sprite = gameScreenContent.Load<Texture2D>(enemies[1].Path);
            enemy2Sprite = gameScreenContent.Load<Texture2D>(enemies[2].Path);
            player0Sprite = gameScreenContent.Load<Texture2D>(players[0].Path);

            hpPlayer1 = gameScreenContent.Load<SpriteFont>(hpOnScreen);
                 
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

            spriteBatch.DrawString(hpPlayer1, "HP: " + hp, new Vector2(190,160), Color.Red);
        }
    }
}
