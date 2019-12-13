using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SuperFantasyMagicProject
{
    class DemonFlower : Character
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DemonFlower()
        {
            texturePath = "Enemies/Demon flowers/Purple/Animation 1/DemonFlower1.";

            baseHealth = 30;
            baseMana = 50;
            baseCritical = 0.05;

            Strength = 4;       //Every point of Strength adds 10 to MaxHealth, and 2 to Damage.
            Agility = 8;        //Every two points of Agility adds 1 to TurnSpeed.
            Intelligence = 2;   //Every point of Intelligence adds 10 to MaxMana, and 0.1 to Critical

            CurrentHealth = MaxHealth;
            CurrentMana = MaxMana;
        }

        public override void LoadContent(ContentManager content)
        {
            //Load textures.
            textures = new Texture2D[3];

            for (int i = 0; i < textures.Length; i++)
            {
                textures[i] = content.Load<Texture2D>(texturePath + (i + 1));
            }

            texture = textures[0];

            //Set origin.
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override int Attack()
        {
            //Attack at random against Player
            //Attack random enemy in an array
            return 0;
        }

        public override void SpecialAttack()
        {
            //Applies Screatch against Player (100% of the time)
            //Reduce Player Strength and Health
        }

        public override void TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            CurrentHealth -= dmg;
        }
    }
}
