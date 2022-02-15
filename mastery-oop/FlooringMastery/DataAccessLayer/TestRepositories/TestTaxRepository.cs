using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.TestRepositories
{
    public class TestTaxRepository : ITaxRepository
    {
        Dictionary<string, StateTax> taxes;
        List<string> rows;
        string path;

        public TestTaxRepository()
        {
            taxes = new Dictionary<string, StateTax>();
            rows = new List<string>();
            path = @"C:\Software Guild\Summatives\mastery-oop\FlooringMastery\TestRepos\StateTaxes\taxes.txt";
            _populateRepositoryFromFile(path);
        }

        private void _populateRepositoryFromFile(string path)
        {
            rows = File.ReadAllLines(path).ToList();

            for (int i = 1; i < rows.Count; i++)
            {
                string[] columns = rows[i].Split(',');
                StateTax stateTax = new StateTax();

                stateTax.StateAbbreviation = columns[0];
                stateTax.StateName = columns[1];
                stateTax.TaxRate = decimal.Parse(columns[2]);

                taxes.Add(stateTax.StateAbbreviation, stateTax);
            }
        }

        public List<StateTax> ReadAll()
        {
            List<StateTax> allTaxes = new List<StateTax>();

            foreach (KeyValuePair<string, StateTax> entry in taxes)
            {
                allTaxes.Add(entry.Value);
            }

            return allTaxes;
        }

        public StateTax ReadByID(string stateAbbreviation)
        {
            if (taxes.ContainsKey(stateAbbreviation))
            {
                StateTax taxInfo = taxes[stateAbbreviation];
                return taxInfo;
            }
            else
            {
                throw new StateDoesNotExistException("The State Selected Does Not Exist in Our Area of Service");
            }
        }
    }
}
