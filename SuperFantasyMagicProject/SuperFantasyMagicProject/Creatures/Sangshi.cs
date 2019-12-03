﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Creatures
{
    class Sangshi : Character
    {
        
        public Sangshi()
        {

        }

        public Sangshi(Random rnd, int maxHealth, int currentHealth, int mana, int strenght, int agility, int intelligence)
        {
            this.rnd = rnd;
            this.MaxHealth = maxHealth;
            this.CurrentHealth = currentHealth;
            this.Mana = mana;
            this.Strenght = strenght;
            this.Agility = agility;
            this.Intelligence = intelligence;
        }

        public override int Attack()
        {
            //Attack at random against Player
            //Attack random enemy in an array
            return 0;
        }

        public override void SpecialAttack()
        {
            //If killed set Health to full
            //If any Ally is alive keep revive
        }

        public override void TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            throw new NotImplementedException();
        }
    }
}
