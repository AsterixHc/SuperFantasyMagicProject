using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SuperFantasyMagicProject
{
    class Sangshi : Character
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Sangshi()
        {
            TexturePath = "";
            Position = Vector2.Zero;
            Origin = Vector2.Zero;

            baseHealth = 30;
            baseMana = 50;
            baseCritical = 0.05;

            Strength = 4;       //Every point of Strength adds 10 to MaxHealth, and 2 to Damage.
            Agility = 8;        //Every two points of Agility adds 1 to TurnSpeed.
            Intelligence = 2;   //Every point of Intelligence adds 10 to MaxMana, and 0.1 to Critical

            CurrentHealth = MaxHealth;
            CurrentMana = MaxMana;
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
