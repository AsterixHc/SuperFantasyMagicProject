using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace SuperFantasyMagicProject.Playable_Characters
{
    class Warrior : Character
    {

        private int targetAttack;
        
        

        public int TargetAttack { get => targetAttack; set => targetAttack = value; }

        public Warrior()
        {
            MaxHealth = WarriorStats.maxHealth;
            CurrentHealth = WarriorStats.currentHealth;
            Mana = WarriorStats.mana;
            Strenght = WarriorStats.strenght;
            Agility = WarriorStats.agility;
            Intelligence = WarriorStats.intelligence;
            Damage = WarriorStats.damage;

            Position = Vector2.Zero;
            Origin = Vector2.Zero;
            Path = "Player/Knight/Standing/KnightStanding1";
        }

        public Warrior(int maxHealth, int currentHealth, int mana, int strenght, int agility, int intelligence, Vector2 position, int damage)
        {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
            Mana = mana;
            Strenght = strenght;
            Agility = agility;
            Intelligence = intelligence;
            Path = "Player/Knight/Standing/KnightStanding1";
            Position = position;
            Origin = Vector2.Zero;
            Damage = damage;
        }



        public override int Attack()
        {
            //Choose an Enemy from enemy array
            //Attack the chosen enemy in the Array
            //Character damage = 20 + (10% * Player.Strength)

            return 0; // Needs to be fixed sooner rather than later!!

            //ScreenManager.currentScreen.
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

        public override void TakeDamage(int dmg)
        {
            //Reduce currentHealth by damage amount
            CurrentHealth -= dmg;
        }
    }
}
