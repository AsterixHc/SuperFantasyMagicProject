using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Screen
{
    class ShopScreen : GameScreen
    {
        public override void HandleInput()
        {
            throw new NotImplementedException();
        }

        public void PurchaseItem()
        {
            //List of Items
        }

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
