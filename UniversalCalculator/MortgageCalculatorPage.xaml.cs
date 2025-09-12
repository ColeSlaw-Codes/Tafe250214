using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	public sealed partial class MortgageCalculatorPage : Page
	{
		public MortgageCalculatorPage()
		{
			this.InitializeComponent();
		}

		private void Clear_Click(object sender, RoutedEventArgs e)
		{
			PrincipalBox.Text = "";
			AnnualRateBox.Text = "";
			YearsBox.Text = "";
			ResultText.Text = "";
			PrincipalBox.Focus(FocusState.Programmatic);
		}

		private void Calculate_Click(object sender, RoutedEventArgs e)
		{
			decimal principal;
			decimal annualPercent;
			int years;

			bool okPrincipal = decimal.TryParse(PrincipalBox.Text, out principal);
			bool okRate = decimal.TryParse(AnnualRateBox.Text, out annualPercent);
			bool okYears = int.TryParse(YearsBox.Text, out years);

			if (!okPrincipal || !okRate || !okYears || principal <= 0 || annualPercent < 0 || years <= 0)
			{
				ResultText.Text = "Please enter valid values.";
				return;
			}

			decimal monthly = CalculateMonthlyRepayment(principal, annualPercent, years);
			ResultText.Text = "Monthly payment: " + monthly.ToString("C");
		}

		private static decimal CalculateMonthlyRepayment(decimal principal, decimal annualRatePercent, int termYears)
		{
			// i = annual rate / 100 / 12
			double i = (double)annualRatePercent / 100.0;
			i = i / 12.0;

			// n = years * 12
			int n = termYears * 12;

			if (i == 0.0)
			{
				decimal simple = principal / n;
				return Math.Round(simple, 2, MidpointRounding.AwayFromZero);
			}

			double onePlusI = 1.0 + i;
			double pow = Math.Pow(onePlusI, n);
			double top = (double)principal * i * pow;
			double bottom = pow - 1.0;
			double m = top / bottom;

			decimal result = (decimal)m;
			result = Math.Round(result, 2, MidpointRounding.AwayFromZero);
			return result;
		}
	}
}
