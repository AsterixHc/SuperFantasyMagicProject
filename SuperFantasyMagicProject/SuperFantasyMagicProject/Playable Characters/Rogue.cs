﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Playable_Characters
{
    class Rogue : Character
    {
        public Rogue()
        {

        }

        public Rogue(int maxHealth, int currentHealth, int mana, int strenght, int agility, int intelligence)
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

            //Choose an Enemy from the enemy array
            //Choose enemy from enemy array
            return 0;
        }

        public override void SpecialAttack()
        {
            //Item is always the same from the same monster
            //Steal Item from choosen enemy in the enemy array

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
