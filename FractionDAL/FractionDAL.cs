using ProjetFractionDTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFractionDAL
{
    public class FractionDAL
    {
        #region Constants
        private const string FILENAMEARITH = "ArithmeticOperationscsvfile.csv";
        private const string FILENAMECOMP = "ComparingOperationscsvfile.csv";
        private const string HEADOFFILE = "Date,Heure,Fraction1,Opération,Fraction2"; 
        #endregion

        public static bool SaveToCSVFile(List<string> pElements)
        {
            try
            {
                #region Save arithmetic elements
                if (pElements.Count > 5)
                {
                    if (!File.Exists(@$".\{FILENAMEARITH}"))
                    {
                        File.AppendAllText(@$".\{FILENAMEARITH}", HEADOFFILE + ",Résultat\n");
                    }
                    StringBuilder elementsToSave = new StringBuilder();
                    foreach (var element in pElements)
                    {
                        elementsToSave.Append($"{element},");
                    }
                    int positionLastCharOfString = elementsToSave.Length - 1;
                    string lineToSave = elementsToSave.ToString().Remove(positionLastCharOfString);
                    File.AppendAllText(@$".\{FILENAMEARITH}", lineToSave + "\n");
                    return true;
                } 
                #endregion

                #region Save comparing elements
                if (!File.Exists(@$".\{FILENAMECOMP}"))
                {
                    File.AppendAllText(@$".\{FILENAMECOMP}", HEADOFFILE + "\n");
                }
                StringBuilder elementsCompToSave = new StringBuilder();
                foreach (var element in pElements)
                {
                    elementsCompToSave.Append($"{element},");
                }
                int positionCompLastCharOfString = elementsCompToSave.Length - 1;
                string myLineToSave = elementsCompToSave.ToString().Remove(positionCompLastCharOfString);
                File.AppendAllText(@$".\{FILENAMECOMP}", myLineToSave + "\n");
                return true; 
                #endregion
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Read arithmetic and comparing csv file
        public static List<FractionDTO> GetElementsOfArithOperation()
        {
            // Lire le fichier ArithmeticOperationscsvfile.csv ligne à ligne
            // pour chaque ligne, on crée un objet de type FractionDTO
            // ajouter à la liste
            if (!File.Exists(@$".\{FILENAMEARITH}")) throw new FileNotFoundException("Le fichier qui stocke les opérations arithméthiques n'existe pas.");
            try
            {
                List<FractionDTO> elementsOfOperationList = new List<FractionDTO>();
                var GetLinesArithmeticFile = File.ReadLines(@$".\{FILENAMEARITH}").Skip(1);
                foreach (var line in GetLinesArithmeticFile)
                {
                    FractionDTO fdto = StringToFractionDTO(line);
                    elementsOfOperationList.Add(fdto);
                }
                return elementsOfOperationList;
            }
            catch (Exception)
            {

                throw new FileNotFoundException("Problème de lecture du fichier!");
            }

        }

        public static List<FractionDTO> GetElementsOfCompOperation()
        {
            if (!File.Exists(@$".\{FILENAMECOMP}")) throw new FileNotFoundException("Le fichier qui stocke les opérations de comparaison n'existe pas.");
            // Lire le fichier ArithmeticOperationscsvfile.csv ligne à ligne
            // pour chaque ligne, on crée un objet de type FractionDTO
            // ajouter à la liste
            try
            {
                List<FractionDTO> elementsOfOperationList = new List<FractionDTO>();
                var GetLinesComparingFile = File.ReadLines(@$".\{FILENAMECOMP}").Skip(1);
                foreach (var line in GetLinesComparingFile)
                {
                    FractionDTO fdto = ConvertStringToFractionDTO(line);
                    elementsOfOperationList.Add(fdto);
                }
                return elementsOfOperationList;
            }
            catch (Exception)
            {

                throw new FileNotFoundException("Problème de lecture du fichier!");
            }

        }
        private static FractionDTO StringToFractionDTO(string pLine)
        {
            string[] operationElts = pLine.Split(',', StringSplitOptions.None);
            string date = operationElts[0];
            string hour = operationElts[1];
            string fraction1 = operationElts[2];
            string operation = operationElts[3];
            string fraction2 = operationElts[4];
            string result = operationElts[5];
            FractionDTO DtoObject = new FractionDTO
            {
                Date = date,
                Hour = hour,
                Fraction1 = fraction1,
                Operation = operation,
                Fraction2 = fraction2,
                Result = result
            };
            return DtoObject;
        }

        private static FractionDTO ConvertStringToFractionDTO(string pLine)
        {
            string[] operationElts = pLine.Split(',', StringSplitOptions.None);
            string date = operationElts[0];
            string hour = operationElts[1];
            string fraction1 = operationElts[2];
            string operation = operationElts[3];
            string fraction2 = operationElts[4];
            FractionDTO DtoObject = new FractionDTO
            {
                Date = date,
                Hour = hour,
                Fraction1 = fraction1,
                Operation = operation,
                Fraction2 = fraction2
            };
            return DtoObject;
        }
        #endregion
    }
}
