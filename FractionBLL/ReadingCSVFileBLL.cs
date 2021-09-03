using ProjetFractionDAL;
using ProjetFractionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractionBLL
{
    public class ReadingCSVFileBLL
    {
        public static List<FractionDTO> EltsOperationList()
        {
            return FractionDAL.GetElementsOfArithOperation();
        }

        public static List<FractionDTO> EltsCompOperationList()
        {
            return FractionDAL.GetElementsOfCompOperation();
        }
    }
}