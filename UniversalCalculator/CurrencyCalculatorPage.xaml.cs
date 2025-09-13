using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class CurrencyCalculatorPage : Page
	{
		const double USDtoEUR = 0.85189982;
		const double USDtoGBP = 0.72872436;
		const double USDtoINR = 74.257327;

		const double EURtoUSD = 1.1739732;
		const double EURtoGBP = 0.8556672;
		const double EURtoINR = 87.00755;

		const double GBPtoUSD = 1.361907;
		const double GBPtoEUR = 1.686692;
		const double GBPtoINR = 101.68635;

		const double INRtoUSD = 0.011492628;
		const double INRtoEUR = 0.013492774;
		const double INRtoGBP = 0.0098339397;
		public CurrencyCalculatorPage()
		{
			this.InitializeComponent();
			exitCurrencyButton.Click += ExitCurrencyButton_Click;
		}

		private void ExitCurrencyButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainMenu));
		}

		public void convert(String from, String to, double conversionRate, double reverse, double amountToConvert)
		{
			double conversionAmount = amountToConvert * conversionRate;
			amountTextBlock.Text = amountToConvert + " " + from + " =";
			convertedTextBlock.Text = conversionAmount.ToString() + " " + to;
			currencyRateTextBlock.Text = "1 " + from + " = " + conversionRate + " " + to;
			currencyReverseTextBlock.Text = "1 " + to + " = " + reverse + " " + from;
		}

		private async void calculateCurrencyButton_Click(object sender, RoutedEventArgs e)
		{
			double amount;
			double conversion = 1;
			double conversionReverse = 1;
			try
			{
				amount = double.Parse(amountTextBox.Text);
			}
			catch (Exception theException)
			{
				var dialogMessage = new MessageDialog("Error! please enter a valid amount!");
				await dialogMessage.ShowAsync();
				return;
			}
			int conversionFrom = fromComboBox.SelectedIndex;
			int conversionTo = toComboBox.SelectedIndex;

			switch (conversionFrom)
			{
				case 0: //USD
					switch (conversionTo)
					{
						case 0: //USD
							convert("USD", "USD", conversion, conversionReverse, amount);
							break;
						case 1: //EUR
							conversion = USDtoEUR;
							conversionReverse = EURtoUSD;
							convert("USD", "EUR", conversion, conversionReverse, amount);
							break;
						case 2: //INR
							conversion = USDtoINR;
							conversionReverse = INRtoUSD;
							convert("USD", "INR", conversion, conversionReverse, amount);
							break;
						case 3: //GBP
							conversion = USDtoGBP;
							conversionReverse = GBPtoUSD;
							convert("USD", "GBP", conversion, conversionReverse, amount);
							break;
						default:

							break;
					}
					break;

				case 1: //EUR
					switch (conversionTo)
					{
						case 0: //USD
							conversion = EURtoUSD;
							conversionReverse = USDtoEUR;
							convert("EUR", "USD", conversion, conversionReverse, amount);
							break;
						case 1: //EUR
							convert("EUR", "EUR", conversion, conversionReverse, amount);
							break;
						case 2: //INR
							conversion = EURtoINR;
							conversionReverse = INRtoEUR;
							convert("EUR", "INR", conversion, conversionReverse, amount);
							break;
						case 3: //GBP
							conversion = EURtoGBP;
							conversionReverse = GBPtoEUR;
							convert("EUR", "GBP", conversion, conversionReverse, amount);
							break;
						default:

							break;
					}
					break;
				case 2: //INR
					switch (conversionTo)
					{
						case 0: //USD
							conversion = INRtoUSD;
							conversionReverse = USDtoINR;
							convert("INR", "USD", conversion, conversionReverse, amount);
							break;
						case 1: //EUR
							conversion = INRtoEUR;
							conversionReverse = EURtoINR;
							convert("INR", "EUR", conversion, conversionReverse, amount);
							break;
						case 2: //INR
							convert("INR", "INR", conversion, conversionReverse, amount);
							break;
						case 3: //GBP
							conversion = INRtoGBP;
							conversionReverse = GBPtoINR;
							convert("INR", "GBP", conversion, conversionReverse, amount);
							break;
						default:

							break;
					}
					break;
				case 3: //GBP
					switch (conversionTo)
					{
						case 0: //USD
							conversion = GBPtoUSD;
							conversionReverse = USDtoGBP;
							convert("GBP", "USD", conversion, conversionReverse, amount);
							break;
						case 1: //EUR
							conversion = GBPtoEUR;
							conversionReverse = EURtoGBP;
							convert("GBP", "EUR", conversion, conversionReverse, amount);
							break;
						case 2: //INR
							conversion = GBPtoINR;
							conversionReverse = INRtoGBP;
							convert("GBP", "INR", conversion, conversionReverse, amount);
							break;
						case 3: //GBP
							convert("GBP", "GBP", conversion, conversionReverse, amount);
							break;
						default:

							break;
					}
					break;
				default:

					break;
			}

		}
	}
}
