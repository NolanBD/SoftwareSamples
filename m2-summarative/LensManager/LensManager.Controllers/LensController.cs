using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LensManager.View;
using LensManager.Data;
using LensManager.Models;

namespace LensManager.Controllers
{
    public class LensController
    {
        private UserInterface userInterface;
        private LensRepository repository;

        public LensController()
        {
            userInterface = new UserInterface();
            repository = new LensRepository();
        }
        public void Run()
        {
            bool keepRunning = true;

            while (keepRunning)
            {
                int menuChoice = userInterface.DisplayMenuAndGetUserChoice();

                switch (menuChoice)
                {
                    case 1:
                        AddLens();
                        break;
                    case 2:
                        ShowAllLenses();
                        break;
                    case 3:
                        SearchLenses();
                        break;
                    case 4:
                        EditLens();
                        break;
                    case 5:
                        DeleteLens();
                        break;
                    case 6:
                        keepRunning = false;
                        break;
                }
            }
        }

        private void AddLens()
        {
            Lenses newLens = userInterface.GetNewLens();
            Lenses addedLens = repository.CreateLens(newLens);

            //succesfully added to repository
            if (addedLens != null)
            {
                //show new lens
                userInterface.DisplayLenses(addedLens);
                userInterface.ShowActionSuccess("Add Lens");
            }
            else
            {
                userInterface.ShowActionFailure("Add Lens");
            }
        }

        private void ShowAllLenses()
        {
            //make a lens array to store our current lenses in
            Lenses[] allLenses = new Lenses[11];
            //populate allLenses with our existing lenses
            allLenses = repository.RetrieveAllLenses();

            for (int i = 0; i < allLenses.Length; i++)
            {
                //if unpopulated, skip that member
                if (allLenses[i] == null)
                {
                    continue;
                }
                //if populated, display member
                else
                {
                    userInterface.DisplayLenses(allLenses[i]);
                }
            }
            userInterface.ShowActionSuccess("Show All Lenses");
        }
        private void SearchLenses()
        {
            //populate an int with a user selection between 0 and 10
            int searchedID = userInterface.LensByID();
            //populate a temporary Lenses object with information from our stored lenses 
            Lenses searchedLens = repository.RetrieveLensByID(searchedID);
            //if no information is present, failure
            if (searchedLens == null)
            {
                userInterface.ShowActionFailure("Search Lens");
            }
            else
            {
                userInterface.DisplayLenses(searchedLens);
                userInterface.ShowActionSuccess("Search Lens");
            }
        }
        private void EditLens()
        {
            //populate an int with a user selection between 0 and 10
            int lensToUpdate = userInterface.LensByID();
            //populate a temporary lens object with information from our stored lenses 
            Lenses updatedLens = repository.RetrieveLensByID(lensToUpdate);
            //if no information is present, failure
            if (updatedLens == null)
            {
                userInterface.ShowActionFailure("Update Lens");
            }
            //otherwise continue to edit the lens
            else
            {
                repository.EditLens(lensToUpdate, updatedLens);
                userInterface.UpdateLensInfo(updatedLens);
                userInterface.DisplayLenses(updatedLens);
                userInterface.ShowActionSuccess("Update Lens");
            }
        }
        private void DeleteLens()
        {
            //if the user agrees to delete a lens, continue
            if (userInterface.DeleteLensByID() == true)
            {
            int deleteID = userInterface.LensByID();
            repository.DeleteLens(deleteID);
            userInterface.ShowActionSuccess("Remove Lens");
            }
            else
            {
                userInterface.ShowActionFailure("Remove Lens");
            }
        }
    }
}
