﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Creatures
{
    class Hayo : Character
    {
        public Hayo(Random rnd, int maxHealth, int currentHealth, int mana, int strenght, int agility, int intelligence)
        {
            this.rnd = rnd;
            this.maxHealth = maxHealth;
            this.currentHealth = currentHealth;
            this.mana = mana;
            this.strenght = strenght;
            this.agility = agility;
            this.intelligence = intelligence;
        }

        public override void Attack()
        {
            //Attack at random against Player
            //Attack random enemy in an array

        }

        public override void SpecialAttack()
        {
            //Random chance to Apply "Gust" ( about 30-50% chance to apply)
            //If applied reduce Player Agility
        }

        public override bool TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            throw new NotImplementedException();
        }
    }
}
