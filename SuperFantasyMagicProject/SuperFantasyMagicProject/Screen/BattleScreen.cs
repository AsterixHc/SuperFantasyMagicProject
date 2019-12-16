using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SuperFantasyMagicProject.Playable_Characters;
using Microsoft.Xna.Framework.Media;

namespace SuperFantasyMagicProject.Screen
{
    enum BattleState { Battling, Waiting, PlayerWon, PlayerLost}

    class BattleScreen : GameScreen
    {
        //Input
        private KeyboardState previousKS = Keyboard.GetState();
        private KeyboardState newKS;

        //Graphics
        private Texture2D background;
        private string path = "BattleScreen/Background";

        //Text
        private string text;
        private SpriteFont font;
        private string fontPath = "hpOnScreen";
        private Vector2 textDimensions;
        private Color textColor = Color.Red;

        //Audio
        private Song song;
        //Fixed positions for battlers (players, enemies)
        Vector2 player0Position = new Vector2(350, 510);
        Vector2 player1Position = new Vector2(350, 660);
        Vector2 player2Position = new Vector2(350, 910);
        Vector2 enemy0Position = new Vector2(1640, 490);
        Vector2 enemy1Position = new Vector2(1640, 690);
        Vector2 enemy2Position = new Vector2(1640, 890);        

        //Battle flow
        private Random rnd = new Random();
        private Character[] players = new Character[3];
        private Character[] enemies = new Character[3];
        private List<Character> battlersPending = new List<Character>(6);
        private List<Character> battlersDone = new List<Character>(6);
        private List<Character> deadBattlers = new List<Character>(5);
        private Character activeBattler = null;
        private Character targetCharacter;
        private int selectedAttack = 0;
        private BattleState battleState = BattleState.Battling;
        
        public int ExpValue { get; private set; }

        /// <summary>
        /// Constructor that specifies enemies and experience value.
        /// </summary>
        /// <param name="enemy0">The first enemy (top)</param>
        /// <param name="enemy1">The second enemy (middle)</param>
        /// <param name="enemy2">The third enemy (bottom)</param>
        /// /// <param name="exp">The amount of experience points the encounter is worth</param>
        public BattleScreen(Character enemy0, Character enemy1, Character enemy2, int exp)
        {
            ExpValue = exp;

            //Populate arrays/lists
            players[0] = new Rogue();
            players[1] = new Warrior();
            players[2] = new Mage();
            enemies[0] = enemy0;
            enemies[1] = enemy1;
            enemies[2] = enemy2;
            battlersPending.AddRange(players);
            battlersPending.AddRange(enemies);

            //Set starting positions for battlers
            players[0].Position = player0Position;
            players[1].Position = player1Position;
            players[2].Position = player2Position;
            enemies[0].Position = enemy0Position;
            enemies[1].Position = enemy1Position;
            enemies[2].Position = enemy2Position;

            //Remove dead battlers and add to list of dead battlers.
            foreach (Character character in battlersPending)
            {
                if (!character.IsAlive)
                {
                    deadBattlers.Add(character);
                    battlersPending.Remove(character);
                }
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
            //Load background.
            background = gameScreenContent.Load<Texture2D>(path);

            //Load font.
            font = gameScreenContent.Load<SpriteFont>(fontPath);

            //Load song.
            this.song = gameScreenContent.Load<Song>("Final Fantasy VI Battle Theme Extended");
            MediaPlayer.Play(song);
            //Code for music looping
            //MediaPlayer.IsRepeating = true;

            //Load Players and enemies.
            LoadPlayers();
            LoadEnemies();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //If there is no active battler
            if (activeBattler == null)
            {
                //Sort battlers, then make active the entry with the highest speed. 
                battlersPending.Sort((a, b) => b.TurnSpeed.CompareTo(a.TurnSpeed));
                activeBattler = battlersPending[0];
                battlersPending.RemoveAt(0);
            }

            //If active battler is a player character 
            if (players.Contains(activeBattler))
            {
                if (battleState != BattleState.Waiting)
                {
                    battleState = BattleState.Waiting;
                }

                if (selectedAttack == 2)
                {
                    if (activeBattler == players[2])
                    {
                        SpecialAttackMage();
                    }
                    else if (activeBattler == players[1])
                    {
                        SpecialAttackWarrior();
                    }
                    else if (activeBattler == players[0])
                    {
                        SpecialAttackRogue();
                    }
                }

                HandleInput();
            }
            //If active battler is an enemy character
            else
            {
                //Run enemy battle logic.
                EnemyTurn();
            }

            //Update characters. If dead, add to list of dead battlers.
            UpdatePlayers(gameTime);
            UpdateEnemies(gameTime);

            //If game is not awaiting player input
            if (battleState == BattleState.Battling)
            {
                //Mark active battler's turn as over.
                battlersDone.Add(activeBattler);
                //Clear variale, indicating we want a new battler in next update cycle.
                activeBattler = null;

                //Remove dead battlers from turn cycle
                RemoveDeadBattlers();
                
                //If all battlers have had their turn
                if (battlersPending.Count == 0)
                {
                    //Reset turn cycle.
                    battlersPending.AddRange(battlersDone);
                    battlersDone.Clear();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Draw background.
            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            //Draw players.
            foreach (Character player in players)
            {
                player.Draw(spriteBatch);
            }

            //Draw enemies.
            foreach (Character enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }

            //Draw health and mana above battlers.
            DrawBattlerStats(spriteBatch);
        }

        public void HandleInput()
        {
            newKS = Keyboard.GetState();

            if (newKS.IsKeyDown(Keys.A))
            {
                selectedAttack = 1;
            }
            else if (newKS.IsKeyDown(Keys.S))
            {
                selectedAttack = 2;
            }

            if (selectedAttack == 1)
            {
                //Normal attack command
                if (targetCharacter == null)
                {
                    if (newKS.IsKeyDown(Keys.D1) && previousKS.IsKeyUp(Keys.D1) && enemies[0].IsAlive)
                    {
                        targetCharacter = enemies[0];
                    }
                    else if (newKS.IsKeyDown(Keys.D2) && previousKS.IsKeyUp(Keys.D2) && enemies[1].IsAlive)
                    {
                        targetCharacter = enemies[1];
                    }
                    else if (newKS.IsKeyDown(Keys.D3) && previousKS.IsKeyUp(Keys.D3) && enemies[2].IsAlive)
                    {
                        targetCharacter = enemies[2];
                    }
                }
                else
                {
                    if (newKS.IsKeyDown(Keys.D) && previousKS.IsKeyUp(Keys.D))
                    {
                        activeBattler.Attack(targetCharacter);
                        targetCharacter = null;
                        selectedAttack = 0;
                        battleState = BattleState.Battling;
                    }
                }
            }
            previousKS = newKS;
        }
        /// <summary>
        /// Special attack for the player character called "Mage"
        /// if speccial attack selected and mage is the active character this action will be used
        /// healing the selected player character for 20 but can´t go above maxhealth value
        /// </summary>
        private void SpecialAttackMage()
        {

            //Special attack command
            if (selectedAttack == 2)
            {
                if (activeBattler == players[2])
                {
                    if (newKS.IsKeyDown(Keys.D1) && players[0].MaxHealth != players[0].CurrentHealth)
                    {
                        targetCharacter = players[0];
                    }
                    else if (newKS.IsKeyDown(Keys.D2) && players[1].MaxHealth != players[1].CurrentHealth)
                    {
                        targetCharacter = players[1];
                    }
                    else if (newKS.IsKeyDown(Keys.D3) && players[2].MaxHealth != players[2].CurrentHealth)
                    {
                        targetCharacter = players[2];
                    }

                    if (newKS.IsKeyDown(Keys.D))
                    {
                        targetCharacter.CurrentHealth += 20;
                        targetCharacter = null;
                        selectedAttack = 0;
                        battleState = BattleState.Battling;
                    }
                }
            }

        }
        /// <summary>
        /// Special attack for the player character called "Warrior"
        /// if special attack selected and warrior is the active character this action will be used
        /// dealing warrior.dmg/4 to all enemy characters making him deal only 75% of his total damage but across all enemies
        /// </summary>
        private void SpecialAttackWarrior()
        {
            if (selectedAttack == 2)
            {
                int warriorAoEDmg = players[1].Damage / 4;

                if(newKS.IsKeyDown(Keys.D))
                {
                    enemies[0].TakeDamage(warriorAoEDmg);
                    enemies[1].TakeDamage(warriorAoEDmg);
                    enemies[2].TakeDamage(warriorAoEDmg);
                    selectedAttack = 0;
                    battleState = BattleState.Battling;
                }
            }
        }
        /// <summary>
        /// Special Attack for the player character called "Rogue"
        /// if special attack selected and rogue is the active character this action will be used
        /// dealing half of Rogue.dmg as damage to the enemy and half as healing to the character
        /// </summary>
        private void SpecialAttackRogue()
        {

            if (selectedAttack == 2)
            {

                if (targetCharacter == null)
                {
                    if (newKS.IsKeyDown(Keys.D1) && enemies[0].IsAlive)
                    {
                        targetCharacter = enemies[0];
                    }
                    else if (newKS.IsKeyDown(Keys.D2) && enemies[1].IsAlive)
                    {
                        targetCharacter = enemies[1];
                    }
                    else if (newKS.IsKeyDown(Keys.D3) && enemies[2].IsAlive)
                    {
                        targetCharacter = enemies[2];
                    }

                }

                if (newKS.IsKeyDown(Keys.D))
                {
                    int rogueLifeSteal = players[0].Damage / 2;

                    targetCharacter.TakeDamage(rogueLifeSteal);
                    players[0].CurrentHealth += rogueLifeSteal;

                    selectedAttack = 0;
                    battleState = BattleState.Battling;
                }
            }
        }

        /// <summary>
        /// Determines an enemy character's action during its turn.
        /// Chooses a random living player character and attacks it.
        /// </summary>
        private void EnemyTurn()
        {
            int randomTarget;
            do
            {
                randomTarget = rnd.Next(0, 3);
            } while (!players[randomTarget].IsAlive);
            activeBattler.Attack(players[randomTarget]);
        }

        /// <summary>
        /// Runs through all characters in players[] and updates them.
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdatePlayers(GameTime gameTime)
        {
            foreach (Character player in players)
            {
                player.Update(gameTime);
                if (!player.IsAlive)
                {
                    deadBattlers.Add(player);
                }
            }
            if (players.All(player => !player.IsAlive))
            {
                battleState = BattleState.PlayerLost;
                ScreenManager.ChangeScreenTo(new GameOverScreen());
            }
        }

        /// <summary>
        /// Runs through all characters in enemies[] and updates them.
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdateEnemies(GameTime gameTime)
        {
            foreach (Character enemy in enemies)
            {
                enemy.Update(gameTime);
                if (!enemy.IsAlive)
                {
                    deadBattlers.Add(enemy);
                }
            }
            if (enemies.All(enemy => !enemy.IsAlive))
            {
                battleState = BattleState.PlayerWon;
                //Maybe screen transition here
            }
        }

        /// <summary>
        /// Runs through all characters in players[] and loads them.
        /// </summary>
        private void LoadPlayers()
        {
            foreach (Character player in players)
            {
                player.LoadContent(gameScreenContent);
            }
        }

        /// <summary>
        /// Runs through all characters in enemies[] and loads them.
        /// </summary>
        private void LoadEnemies()
        {
            foreach (Character enemy in enemies)
            {
                enemy.LoadContent(gameScreenContent);
            }
        }

        /// <summary>
        /// Draws health and mana of all battlers above their respective sprites.
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawBattlerStats(SpriteBatch spriteBatch)
        {
            foreach (Character player in players)
            {
                //Draw Health.
                textColor = Color.Red;
                text = "HP: " + player.CurrentHealth;
                textDimensions = font.MeasureString(text);
                spriteBatch.DrawString(font, text, new Vector2(player.Position.X - (textDimensions.X / 2),
                        player.Position.Y - 80), textColor);

                //Draw Mana.
                textColor = Color.Blue;
                text = "MP: " + player.CurrentMana;
                textDimensions = font.MeasureString(text);
                spriteBatch.DrawString(font, text, new Vector2(player.Position.X - (textDimensions.X / 2),
                        player.Position.Y - (80 - textDimensions.Y)), textColor);
            }

            foreach (Character enemy in enemies)
            {
                //Draw Health.
                textColor = Color.Red;
                text = "HP: " + enemy.CurrentHealth;
                textDimensions = font.MeasureString(text);
                spriteBatch.DrawString(font, text, new Vector2(enemy.Position.X - (textDimensions.X / 2),
                        enemy.Position.Y - 80), textColor);

                //Draw Mana.
                textColor = Color.Blue;
                text = "MP: " + enemy.CurrentMana;
                textDimensions = font.MeasureString(text);
                spriteBatch.DrawString(font, text, new Vector2(enemy.Position.X - (textDimensions.X / 2),
                        enemy.Position.Y - (80 - textDimensions.Y)), textColor);
            }
        }

        /// <summary>
        /// Iterates through the list of dead battlers, and removes them from the next turn cycle.
        /// </summary>
        private void RemoveDeadBattlers()
        {
            foreach (Character battler in deadBattlers)
            {
                if (battlersPending.Contains(battler))
                {
                    battlersPending.Remove(battler);
                }

                if (battlersDone.Contains(battler))
                {
                    battlersDone.Remove(battler);
                }
            }
        }
    }
}
