using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Roboquatic
{
    class FileIO
    {
        //Enum
        private enum Enemies
        {
            Empty,
            Base,
            Aiming,
            Static,
            Homing
        }
        //Fields
        private Random rng;
        private List<Enemies[,]> formations = new List<Enemies[,]>();
        private StreamReader load;
        private StringReader loadFormation;
        private StreamWriter save;
        private int viewportHeight;
        private int viewportWidth;
        private Texture2D baseEnemySprite;
        private Texture2D baseEnemyProjectileSprite;
        private Texture2D aimedEnemySprite;
        private Texture2D staticEnemySprite;
        private Texture2D homingEnemySprite;

        //Constructor
        public FileIO(Random rng, int viewportHeight, int viewportWidth, Texture2D baseEnemySprite, Texture2D baseEnemyProjectileSprite, 
            Texture2D aimedEnemySprite, Texture2D staticEnemySprite, Texture2D homingEnemySprite)
        {
            this.rng = rng;
            this.viewportHeight = viewportHeight;
            this.viewportWidth = viewportWidth;

            this.baseEnemySprite = baseEnemySprite;
            this.baseEnemyProjectileSprite = baseEnemyProjectileSprite;
            this.aimedEnemySprite = aimedEnemySprite;
            this.staticEnemySprite = staticEnemySprite;
            this.homingEnemySprite = homingEnemySprite;
        }

        //Methods
        
        /* These were the methods for saving and loading the character, but we decided to not implement this 
         * 
        public void SaveCharacter(string fileName)
        {
            try
            {
                save = new StreamWriter(fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                save.Close();
                throw new NotImplementedException();
            }
        }

        public void LoadCharacter(string fileName)
        {
            try
            {
                load = new StreamReader(fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                load.Close();
                throw new NotImplementedException();
            }
        }
        */

        public void LoadFormation(string fileName)
        {
            try
            {
                //Takes file and reads the number of formations in the document
                load = new StreamReader(fileName);
                int numberOfFormations = int.Parse(load.ReadLine());

                //This outer for loop will run based on the number above
                for (int i = 0; i < numberOfFormations; i++)
                {
                    //This gets the area of the formation
                    string areaString = load.ReadLine();
                    string[] areaNumbers = areaString.Split(',');
                    int formationWidth = int.Parse(areaNumbers[0]);
                    int formationHeight = int.Parse(areaNumbers[1]);

                    //Creates a 2D bool array to show where enemies are in the list
                    formations.Add(new Enemies[formationWidth, formationHeight]);

                    //Iterates through the size of array to places each enemy type based on the symbol or empty.
                    for (int j = 0; j < formationHeight; j++)
                    {
                        string line = load.ReadLine();
                        char[] characters = line.ToCharArray();
                        for (int k = 0; k < formationWidth; k++)
                        {
                            int filled = characters[k];
                            
                            switch (filled)
                            {
                                //111 is o (empty)
                                case 111:
                                    formations[i][k, j] = Enemies.Empty;
                                    break;
                                //120 is x (base)
                                case 120:
                                    formations[i][k, j] = Enemies.Base;
                                    break;
                                //97 is a (aiming)
                                case 97:
                                    formations[i][k, j] = Enemies.Aiming;
                                    break;
                                //115 is s (static)
                                case 115:
                                    formations[i][k, j] = Enemies.Static;
                                    break;
                                //104 is h (homing)
                                case 104:
                                    formations[i][k, j] = Enemies.Homing;
                                    break;
                                
                                default:
                                    break;
                            }
                        }
                        //In the end creates a 2D array with the locations of enemies
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if(loadFormation != null && load != null)
            {
                load.Close();
            }
        }

        
        public List<Enemy> AddFormation(int index, int offset)
        {
            //Creates a list to return later with the enemies that need to be added
            List<Enemy> enemiesToAdd = new List<Enemy>();
            //Finds the formation that you want to added based on the index parameter
            Enemies[,] enemies = formations[index];

            //The two for loops iterate through the enemies 2d array
            for (int i = 0; i < enemies.GetLength(0); i++)
            {
                for (int j = 0; j < enemies.GetLength(1); j++)
                {
                    switch (enemies[i, j])
                    {
                        //Depending on the enum it adds that type of enemy
                        case Enemies.Base:
                            //creates the enemy and makes the rectangle based on where it is in the 2d array
                            enemiesToAdd.Add(new BaseEnemy(baseEnemySprite, new Rectangle(viewportWidth + (i * 70), offset + (j * 69), 64, 64), 2, 120, 
                                baseEnemyProjectileSprite, new Rectangle(0, 0, 64, 52)));
                            break;

                        case Enemies.Aiming:
                            enemiesToAdd.Add(new AimingEnemy(aimedEnemySprite, new Rectangle(viewportWidth + (i * 70), offset + (j * 69), 64, 64), 2, 120,
                                baseEnemyProjectileSprite, new Rectangle(0, 0, 62, 40)));
                            break;

                        case Enemies.Static:
                            enemiesToAdd.Add(new StaticEnemy(staticEnemySprite, new Rectangle(viewportWidth + (i * 70), offset + (j * 69), 64, 64), 4, new Rectangle(0, 0, 62, 56)));
                            break;

                        case Enemies.Homing:
                            enemiesToAdd.Add(new RangedHomingEnemy(homingEnemySprite, new Rectangle(viewportWidth + (i * 70), offset + (j * 69), 64, 64), 2, 240,
                                baseEnemyProjectileSprite, new Rectangle(0, 0, 62, 36)));
                            break;

                        default:
                            //For when the enum is Enemies.Empty
                            break;
                    }
                }
            }

            return enemiesToAdd;
        }

        //Functionally the same as the method before except the formation chosen is random and the offset is random
        public List<Enemy> AddRandomFormation()
        {
            List<Enemy> enemiesToAdd = new List<Enemy>();
            Enemies[,] enemies = formations[rng.Next(0,formations.Count - 1)];
            int randomOffset = rng.Next(0, viewportHeight / 2);

            for (int i = 0; i < enemies.GetLength(0); i++)
            {
                for (int j = 0; j < enemies.GetLength(1); j++)
                {
                    switch (enemies[i, j])
                    {
                        case Enemies.Base:
                            enemiesToAdd.Add(new BaseEnemy(baseEnemySprite, new Rectangle(viewportWidth + (i * 70), randomOffset + (j * 69), 64, 64), 2, 120, 
                                baseEnemyProjectileSprite, new Rectangle(0, 0, 64, 52)));
                            break;

                        case Enemies.Aiming:
                            enemiesToAdd.Add(new AimingEnemy(aimedEnemySprite, new Rectangle(viewportWidth + (i * 70), randomOffset + (j * 69), 64, 64), 2, 120, 
                                baseEnemyProjectileSprite, new Rectangle(0, 0, 62, 40)));
                            break;

                        case Enemies.Static:
                            enemiesToAdd.Add(new StaticEnemy(staticEnemySprite, new Rectangle(viewportWidth + (i * 70), randomOffset + (j * 69), 64, 64), 4, new Rectangle(0, 0, 62, 56)));
                            break;

                        case Enemies.Homing:
                            enemiesToAdd.Add(new RangedHomingEnemy(homingEnemySprite, new Rectangle(viewportWidth + (i * 70), randomOffset + (j * 69), 64, 64), 2, 240,
                                baseEnemyProjectileSprite, new Rectangle(0, 0, 62, 36)));
                            break;

                        default:
                            //For when the enum is Enemies.Empty
                            break;
                    }
                }
            }

            return enemiesToAdd;
        }
    }
}
