using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LensManager.Models;

namespace LensManager.Data
{
    public class LensRepository
    { 
        Lenses[] lensesList;

        public LensRepository()
        {
            //initialize array
            lensesList = new Lenses[11];

            //creating a demonstration for the user
            Lenses example = new Lenses();
            example.LensID = 0;
            example.LensName = "Viltrox Cine Lens for L-Mount";
            example.MinimumFocalLength = 20;
            example.MinimumAperture = 2.0;
            example.IsCineLens = true;

            lensesList[0] = example;
        }

        public Lenses CreateLens(Lenses lens)
        {
            //find first open spot in lens list
            for (int i = 0; i < lensesList.Length; i++)
            {
                if (lensesList[i] == null)
                {
                    //set id number for lens
                    lens.LensID = i;
                    //add lens to list
                    lensesList[i] = lens;
                    return lens;
                }
            }
            //array is full, not able to add new lens
            return null;
        }

        public Lenses[] RetrieveAllLenses()
        {
            //returns all members
            return lensesList;
        }

        public Lenses RetrieveLensByID(int lensID)
        {
            //returns a member by ID number
            return lensesList[lensID];
        }

        public void DeleteLens(int lensID)
        {
            //deletes a lens from the list
            lensesList[lensID] = null;
        }

        public void EditLens(int updateId, Lenses lens)
        {
            //userInterface = new UserInterface();
            //lensesList[updateId] = null;
            //lensesList[updateId] = userInterface.UpdateLensInfo(lens);
            lensesList[updateId] = lens;
        }
    }
}
