﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Playable_Characters
{
    class Mage : Character
    {
        public Mage()
        {
            MaxHealth = MageStats.maxHealth;
            CurrentHealth = MageStats.currentHealth;
            Mana = MageStats.mana;
            Strenght = MageStats.strenght;
            Agility = MageStats.agility;
            Intelligence = MageStats.intelligence;
            Damage = MageStats.damage;
            Critical = MageStats.critical;
            Turnspeed = MageStats.turnSpeed;
        }

        public Mage(int maxHealth, int currentHealth, int mana, int strenght, int agility, int intelligence)
        {
            this.MaxHealth = maxHealth;
            this.CurrentHealth = currentHealth;
            this.Mana = mana;
            this.Strenght = strenght;
            this.Agility = agility;
            this.Intelligence = intelligence;
        }

        public override int Attack()
        {
            //Choose an Enemy from the Enemy Array
            //Attack the chosen enemy in the Array
            return 0;
        }

        public override void SpecialAttack()
        {
            //Healing Self or Allies
            //Heal should be about 20% of the full HP of the character used on
            //Low chance to heal full party
        }

        public override void LevelUp()
        {
            //Checks amount of Exprience gained
            //If Exprience gained is higher than Exprience needed to "Levelup"
            //Trigger LevelUp screen
            //Increase Stats
        }

        public override void Flee()
        {
            //Escape from Combat
        }

        public override void UseItem(Item item)
        {
            //Checks List for items
            //If item selected is on List
            //Call Item Script (f.eks. Item.Potion)
            //Gain Effect
            //Check if Item Effect Gained
        }

        public override void TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            throw new NotImplementedException();
        }
    }
}
