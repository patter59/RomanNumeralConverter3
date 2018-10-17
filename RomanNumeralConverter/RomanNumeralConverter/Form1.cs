using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanNumeralConverter
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            ConversionSwitch();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtArabic.Text = "";
            txtRoman.Text = "";
            lblWarning.Text = "";
        }

        private void ConversionSwitch()
        {
            if(isRoman() == isArabic())
            {
                lblWarning.Text = "You either entered values in both fields or none at all please correct this issue and try again.";
            }
            if (isArabic() && !isRoman())
            {
                txtRoman.Text = arabicToRoman(int.Parse(txtArabic.Text));
            }
            if(isRoman() && !isArabic())
            {
                romanToArabic(txtRoman.Text.ToString().ToUpper());
            }
           
        }
        private bool isArabic()
        {
            if (txtArabic.Text != null && txtArabic.Text != "")
            {
                return isValidArabic();
            }
            else
            {
                return false;
            }
           
        }
        private bool isValidArabic()
        {
            try
            {
                var arabic = int.Parse(txtArabic.Text);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string arabicToRoman(int input)
        {
            var romanNumerals = new string[][]
            {
            new string[]{"", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"},
            new string[]{"", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC"},
            new string[]{"", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM"},
            new string[]{"", "M", "MM", "MMM"}
            };

            var intArray = input.ToString().Reverse().ToArray();
            var len = intArray.Length;
            var romanNumeral = "";
            var i = len;

            while (i-- > 0)
            {
                romanNumeral += romanNumerals[i][int.Parse(intArray[i].ToString())];
            }

            return romanNumeral;
        }

        private bool isRoman()
        {
            if(txtRoman.Text != null && txtRoman.Text != "")
            {
                return isValidRoman();
            }
            else
            {
                return false;
            }
        }
        private bool isValidRoman()
        {
            var romanInput = txtRoman.Text.ToString().ToUpper();

            if(ruleCheck_ValidLetters(romanInput))
            {
                if(ruleCheck_RuleOfThree_IXCM(romanInput))
                {
                    return true;
                }
                else
                {
                    lblWarning.Text = "The Roman Numeral entered is not valid it violates the rule of three.";
                    return false;
                }
            }
            else
            {
                lblWarning.Text = "The Roman numeral entered is not valid try again.";
                return false;
            }

        }
        //The Romans wrote their numbers using letters; specifically the letters 'I' meaning '1', 
        //'V' meaning '5', 'X' meaning '10', 'L' meaning '50', 'C' meaning '100', 'D' meaning '500', and 'M' meaning '1000'. 
        private bool ruleCheck_ValidLetters(string input)
        {
            var allowableletters = "IVXLCDM";

            foreach (char c in input)
            {
                if(!allowableletters.Contains(c.ToString()))
                {
                    return false;
                }
            }
            return true;
        }
        //The symbols 'I', 'X', 'C', and 'M' can be repeated at most 3 times in a row
        private bool ruleCheck_RuleOfThree_IXCM(string input)
        {
            var l1 = "";
            var l2 = "";
            var l3 = "";
            var l4 = "";

            var charArray = input.ToArray();

            for(int x = 3; x < charArray.Length; x++)
            {
                l1 = charArray[x - 3].ToString();
                l2 = charArray[x - 2].ToString();
                l3 = charArray[x - 1].ToString();
                l4 = charArray[x].ToString();
                if(l1 == l2 && l2 == l3 && l3 == l4)
                {
                    return false;
                }
            }

            return true;
        }
        //The symbols 'V', 'L', and 'D' can never be repeated.
        private bool ruleCheck_RepeatRule_VLD(string input)
        {
            var V = 0;
            var L = 0;
            var D = 0;

            foreach(char c in input)
            {
                if (c.ToString() == "V") V++;
                if (c.ToString() == "L") L++;
                if (c.ToString() == "D") D++;

                if (V > 1 || L > 1 || D > 1)
                {
                    return false;
                }
            }
            return true;
        }
        
        private void romanToArabic(string input)
        {
            var result = 0;

            foreach (var c in input)
            {
                result += convertRomanToArabic(c);
            }
            if (input.Contains("IV") || input.Contains("IX"))
                result -= 2;

            if (input.Contains("XL") || input.Contains("XC"))
                result -= 20;

            if (input.Contains("CD") || input.Contains("CM"))
                result -= 200;

            txtArabic.Text = result.ToString();
        }
        private int convertRomanToArabic(char input)
        {
            switch(input)
            {
                case 'I': return 1;
                case 'V': return 5;
                case 'X': return 10;
                case 'L': return 50;
                case 'C': return 100;
                case 'D': return 500;
                case 'M': return 1000;
                default: throw new ArgumentException("Invalid Format");
            }
        }


    }
}
