using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFantasyMagicProject.Playable_Characters
{
    class Warrior : Character
    {
        public Warrior()
        {

        }
        
        public Warrior(int maxHealth, int currentHealth, int mana, int strenght, int agility, int intelligence)
        {
            this.maxHealth = maxHealth;
            this.currnetHealth = currentHealth;
            this.mana = mana;
            this.strenght = strenght;
            this.agility = agility;
            this.intelligence = intelligence;
        }

        public override void Attack()
        {
            //Choose an Enemy from enemy array
            //Choose Enemy from enemy array
            //Character damage = 20 + (10% * Player.Strength)
        }

        public override void SpecialAttack()
        {
            //Attack all Enemies
            //DMG should be about 25% of the basic attack
            //Over all dmg should be about 75% of a full basic attack
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

        public override bool TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            throw new NotImplementedException();
        }
    }
}
