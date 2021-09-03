using FractionBLL;
using ProjetFractionDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ProjetFractionUIWithNumericComp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CBSelectOperation.SelectedIndex = 0;
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            #region Arithmetic operation
            List<string> elementsToSave = new List<string>();
            int numerator1 = (int)NUDArithNumerator1.Value;
            int denominator1 = (int)NUDArithDenominator1.Value;
            int numerator2 = (int)NUDArithNumerator2.Value;
            int denominator2 = (int)NUDArithDenominator2.Value;
            CheckDenominators(denominator1, denominator2);
            Fraction f1 = new Fraction(numerator1, denominator1);
            Fraction f2 = new Fraction(numerator2, denominator2);

            FirstElementsToSave(elementsToSave, f1);

            switch (CBSelectOperation.SelectedIndex)
            {
                case 0:
                    TBArithmeticResult.Text = f1 + f2;
                    CompleteList(elementsToSave, f2);
                    break;
                case 1:
                    TBArithmeticResult.Text = f1 - f2;
                    CompleteList(elementsToSave, f2);
                    break;
                case 2:
                    TBArithmeticResult.Text = f1 * f2;
                    CompleteList(elementsToSave, f2);
                    break;
                case 3:
                    TBArithmeticResult.Text = f1 / f2;
                    CompleteList(elementsToSave, f2);
                    break;
            }
            SavingHistoric(elementsToSave, SendDataToBLL(elementsToSave));
            #endregion
        }

        private void BtnCompare_Click(object sender, EventArgs e)
        {
            #region Comparing operation
            List<string> comparingElementsToSave = new List<string>();
            int numerator1 = (int)NUDCompNumerator1.Value;
            int denominator1 = (int)NUDCompDenominator1.Value;
            int numerator2 = (int)NUDCompNumerator2.Value;
            int denominator2 = (int)NUDCompDenominator2.Value;
            Fraction f1 = new Fraction(numerator1, denominator1);
            Fraction f2 = new Fraction(numerator2, denominator2);

            FirstElementsToSave(comparingElementsToSave, f1);

            if (f1 > f2)
            {
                TBComparingResult.Text = $"{f1} > {f2}";
                CompleteListEltsComparing(comparingElementsToSave, ">", f2);
            }
            else if (f1 < f2)
            {
                TBComparingResult.Text = $"{f1} < {f2}";
                CompleteListEltsComparing(comparingElementsToSave, "<", f2);
            }
            else
            {
                TBComparingResult.Text = $"{f1} = {f2}";
                CompleteListEltsComparing(comparingElementsToSave, "=", f2);
            }

            SavingHistoric(comparingElementsToSave, SendDataToBLL(comparingElementsToSave));
            #endregion
        }

        #region Methods
        private static void CheckDenominators(int denominator1, int denominator2)
        {
            if (denominator1 == 0 || denominator2 == 0) throw new DivideByZeroException("Le(s) dénominateur(s) doivent être différent(s) de zéro.");
        }

        private void CompleteListEltsComparing(List<string> pComparingElementsToSave, string pCompSign, Fraction pFraction2)
        {
            pComparingElementsToSave.Add(pCompSign);
            pComparingElementsToSave.Add(pFraction2);
        }

        private bool SendDataToBLL(List<string> pElementsToSave)
        {
            return Fraction.EltsToSave(pElementsToSave);
        }

        private void SavingHistoric(List<string> pElementsToSave, bool pResult)
        {
            if (pResult)
            {
                StringBuilder savingHistoric = new StringBuilder();
                foreach (var element in pElementsToSave)
                {
                    savingHistoric.Append($"{element},");
                }
                int lastPosition = savingHistoric.Length - 1;
                RTBSavingHistoric.Text += savingHistoric.ToString().Remove(lastPosition) + "\n";
            }
        }

        private void CompleteList(List<string> pCompleteElementsToSave, Fraction pFraction2)
        {
            if (CBSelectOperation.Text == "Addition")
            {
                pCompleteElementsToSave.Add("+");
            }
            else if (CBSelectOperation.Text == "Soustraction")
            {
                pCompleteElementsToSave.Add("-");
            }
            else if (CBSelectOperation.Text == "Multiplication")
            {
                pCompleteElementsToSave.Add("*");
            }
            else
            {
                pCompleteElementsToSave.Add("/");
            }
            pCompleteElementsToSave.Add(pFraction2);
            pCompleteElementsToSave.Add(TBArithmeticResult.Text);
        }

        private void FirstElementsToSave(List<string> pElementsToSave, Fraction pFraction1)
        {
            pElementsToSave.Add(DateTime.Now.ToShortDateString());
            pElementsToSave.Add(DateTime.Now.ToShortTimeString());
            pElementsToSave.Add(pFraction1);
        }
        #endregion

        #region Read CSV file: arithmetic operation
        private void BtnReadCsvFile_Click(object sender, EventArgs e)
        {
            //List<FractionDTO> eltsOperationList = ReadingCSVFileBLL.EltsOperationList();
            IEnumerable<FractionDTO> eltsOperationList = ReadingCSVFileBLL.EltsOperationList();
            LVOperationsList.Items.Clear();
            LVOperationsList.Columns.Remove(txtResultat);
            LVOperationsList.Columns.Insert(5, txtResultat);
            LVOperationsList.View = View.Details;
            foreach (var element in eltsOperationList)
            {
                LVOperationsList.Items.Add(new ListViewItem(
                    new string[] {
                        element.Date.ToString(),
                        element.Hour.ToString(),
                        element.Fraction1.ToString(),
                        element.Operation.ToString(),
                        element.Fraction2.ToString(),
                        element.Result.ToString()
                    }));
            }
        }
        #endregion

        #region Read CSV file: comparing operation
        private void BtnComparing_Click(object sender, EventArgs e)
        {
            IEnumerable<FractionDTO> eltsOperationList = ReadingCSVFileBLL.EltsCompOperationList();
            LVOperationsList.Items.Clear();
            LVOperationsList.Columns.Remove(txtResultat);
            LVOperationsList.View = View.Details;
            foreach (var element in eltsOperationList)
            {
                LVOperationsList.Items.Add(new ListViewItem(
                    new string[] {
                        element.Date.ToString(),
                        element.Hour.ToString(),
                        element.Fraction1.ToString(),
                        element.Operation.ToString(),
                        element.Fraction2.ToString(),
                    }));
            }
        } 
        #endregion
    }
}
