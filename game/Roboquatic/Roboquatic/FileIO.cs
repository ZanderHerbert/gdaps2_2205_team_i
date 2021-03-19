using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Roboquatic
{
    class FileIO
    {
        //Fields
        private Random rng;
        List<bool[,]> formations = new List<bool[,]>();
        private StreamReader load;
        private StringReader loadFormation;
        private StreamWriter save;

        //Constructor
        public FileIO(Random rng)
        {
            this.rng = rng;
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
                    formations.Add(new bool[formationWidth, formationHeight]);

                    //Iterates through the size of array to plcve true where enemies are and false where they are not
                    for (int j = 0; j < formationHeight; j++)
                    {
                        loadFormation = new StringReader(load.ReadLine());
                        for (int k = 0; k < formationWidth; k++)
                        {
                            int filled = loadFormation.Read();
                            //int 120 is an x
                            if (filled == 120)
                            {
                                formations[i][k, j] = true;
                            }
                            //int 111 is an o
                            else if (filled == 111)
                            {
                                formations[i][k, j] = false;
                            }
                        }
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
            }
        }

        /*
        public Enemy[] GetFormation(int index)
        {

        }

        public Enemy[] RandomFormation()
        {

        }
        */
    }
}
