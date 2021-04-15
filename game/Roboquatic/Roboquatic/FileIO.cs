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
        public FileIO(Random rng, int viewportHeight, int viewportWidth, Texture2D baseEnemySprite, Texture2D baseEnemyProjectileSprite, Texture2D aimedEnemySprite, Texture2D staticEnemySprite, Texture2D homingEnemySprite)
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
            }
        }

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
                        loadFormation = new StringReader(load.ReadLine());
                        for (int k = 0; k < formationWidth; k++)
                        {
                            int filled = loadFormation.Read();
                            
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
            finally
            {
                load.Close();
                loadFormation.Close();
            }
        }

        
        public List<Enemy> AddFormation(int index, int offset)
        {
            List<Enemy> enemiesToAdd = new List<Enemy>();
            Enemies[,] enemies = formations[index];

            for (int i = 0; i < enemies.GetLength(0); i++)
            {
                for (int j = 0; j < enemies.GetLength(1); j++)
                {
                    switch (enemies[i, j])
                    {
                        case Enemies.Base:
                            enemiesToAdd.Add(new BaseEnemy(baseEnemySprite, new Rectangle(viewportWidth + (i * 70), offset + (j * 70), 64, 64), 2, 120, baseEnemyProjectileSprite));
                            break;

                        case Enemies.Aiming:
                            enemiesToAdd.Add(new AimingEnemy(aimedEnemySprite, new Rectangle(viewportWidth + (i * 70), offset + (j * 70), 64, 64), 2, 120, baseEnemyProjectileSprite));
                            break;

                        case Enemies.Static:
                            enemiesToAdd.Add(new StaticEnemy(staticEnemySprite, new Rectangle(viewportWidth + (i * 70), offset + (j * 70), 64, 64), 4));
                            break;

                        case Enemies.Homing:
                            enemiesToAdd.Add(new RangedHomingEnemy(homingEnemySprite, new Rectangle(viewportWidth + (i * 70), offset + (j * 70), 64, 64), 2, 120, baseEnemyProjectileSprite));
                            break;

                        default:
                            break;
                    }
                }
            }

            return enemiesToAdd;
        }

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
                            enemiesToAdd.Add(new BaseEnemy(baseEnemySprite, new Rectangle(viewportWidth + (i * 70), randomOffset + (j * 70), 64, 64), 2, 120, baseEnemyProjectileSprite));
                            break;

                        case Enemies.Aiming:
                            enemiesToAdd.Add(new AimingEnemy(aimedEnemySprite, new Rectangle(viewportWidth + (i * 70), randomOffset + (j * 70), 64, 64), 2, 120, baseEnemyProjectileSprite));
                            break;

                        case Enemies.Static:
                            enemiesToAdd.Add(new StaticEnemy(staticEnemySprite, new Rectangle(viewportWidth + (i * 70), randomOffset + (j * 70), 64, 64), 4));
                            break;

                        case Enemies.Homing:
                            enemiesToAdd.Add(new RangedHomingEnemy(homingEnemySprite, new Rectangle(viewportWidth + (i * 70), randomOffset + (j * 70), 64, 64), 2, 120, baseEnemyProjectileSprite));
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
