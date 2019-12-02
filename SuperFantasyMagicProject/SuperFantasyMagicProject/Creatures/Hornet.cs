using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Creatures
{
    class Hornet : Character
    {
        public Hornet(Random rnd, int maxHealth, int currentHealth, int mana, int strenght, int agillity, int intelligence)
        {

        }

        public override void Attack()
        {
            //Attack at random against Player
            //Attack random enemy in an array

        }

        public override void SpecialAttack()
        {
            //Random% chance to paralyze Player ( Something like 10-20% change to apply)
            //Apply paralyze
        }

        public override void TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            throw new NotImplementedException();
        }
    }
}
