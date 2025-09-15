using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
			MonthsBox.Text = "";
			MonthlyRateBox.Text = "";
			RepaymentBox.Text = "";
			PrincipalBox.Focus(FocusState.Programmatic);
		}

		private void Calculate_Click(object sender, RoutedEventArgs e)
		{
			// Parse inputs
			if (!decimal.TryParse(PrincipalBox.Text, out var principal) ||
				!decimal.TryParse(AnnualRateBox.Text, out var annualPercent) ||
				!int.TryParse(YearsBox.Text == "" ? "0" : YearsBox.Text, out var years) ||
				!int.TryParse(MonthsBox.Text == "" ? "0" : MonthsBox.Text, out var months) ||
				principal <= 0 || annualPercent < 0)
			{
				RepaymentBox.Text = "Enter valid values.";
				return;
			}

			int n = years * 12 + months;
			if (n <= 0)
			{
				RepaymentBox.Text = "Enter years or months.";
				return;
			}

			// Monthly interest rate (as a fraction)
			double i = (double)annualPercent / 100.0 / 12.0;

			// Show monthly rate as a percentage (e.g., 0.333%)
			MonthlyRateBox.Text = i.ToString("P3");

			// Handle zero-interest case
			if (i == 0.0)
			{
				decimal simple = principal / n;
				RepaymentBox.Text = Math.Round(simple, 2, MidpointRounding.AwayFromZero).ToString("C");
				return;
			}

			double pow = Math.Pow(1 + i, n);
			double m = (double)principal * (i * pow) / (pow - 1);

			var monthly = (decimal)Math.Round(m, 2, MidpointRounding.AwayFromZero);
			RepaymentBox.Text = monthly.ToString("C");
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainMenu));
		}
	}
}
