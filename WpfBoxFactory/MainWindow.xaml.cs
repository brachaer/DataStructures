using BoxProject;
using System;
using System.Windows;
using System.Windows.Controls;
using WpfBoxFactory.Views;

namespace WpfBoxFactory
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		readonly BoxService service = BoxService.Init;
		ExpiredBoxList expiredBoxesUC = new ExpiredBoxList();
		ValidBoxList validBoxesUC = new ValidBoxList();
		Box selectedBox { get; set; }
		public MainWindow()
		{
			InitializeComponent();
			LoadListToGrid(validGR, validBoxesUC);
			LoadListToGrid(expiredGR, expiredBoxesUC);
			BuyOptionInvisible();
		}
		private void LoadListToGrid(Grid grid, UserControl userControl)
		{
			grid.Children.Add(userControl);
		}
		private void BuyOptionInvisible()
		{
			btnBuy.Visibility = Visibility.Collapsed;
			tbAmount.Visibility = Visibility.Collapsed;
			tblHowMany.Visibility = Visibility.Collapsed;
		}
		private void BuyOptionVisible()
		{
			btnBuy.Visibility = Visibility.Visible;
			tbAmount.Visibility = Visibility.Visible;
			tblHowMany.Visibility = Visibility.Visible;
		}

		private void BtnBuy_Click(object sender, RoutedEventArgs e)
		{
			if (tbAmount.Text == "") MessageBox.Show("Please Enter Amount to buy");
			else if (selectedBox == null) lblInfo.Content = "Please search for box to buy";
			else
			{
				int amount = int.Parse(tbAmount.Text);
				if (amount > selectedBox.Amount)
					lblInfo.Content = $"Not enough boxes.\n please enter amount up to {selectedBox.Amount}";
				else
				{
					service.UpdateAmountToBuy(selectedBox, amount);
					lblInfo.Content = $"You purchased {amount} boxes. \n Base Size: {selectedBox.Base}  Y Size (Hight): {selectedBox.Y} \n Have a nice day!";
					BuyOptionInvisible();
					validBoxesUC.CreatePages();
				}
				tbAmount.Text = null;
			}
		}
		private void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			if (tbX.Text == null || double.Parse(tbX.Text) == 0)
				MessageBox.Show("Please enter size X to Add box");
			if (tbY.Text == null || double.Parse(tbY.Text) == 0)
				MessageBox.Show("Please enter size Y to Add box");
			if (tbQuantity.Text == null || int.Parse(tbQuantity.Text) == 0)
				MessageBox.Show("Please enter Ammount to Add box");
			else
			{
				double x = double.Parse(tbX.Text);
				double y = double.Parse(tbY.Text);
				int amount = int.Parse(tbQuantity.Text);
				var box = new Box() { X = x, Y = y, Amount = amount, Base = x * x };
				service.AddBox(box);
				validBoxesUC.CreatePages();
				tbX.Text = null;
				tbY.Text = null;
				tbQuantity.Text = null;
			}
		}
		private void BtnSearch_Click(object sender, RoutedEventArgs e)
		{
			if (tbSearchBase.Text == null)
				lblInfo.Content = "Please enter\nBase size \nto Search box";
			if (tbSearchY.Text == null)
				lblInfo.Content = "Please enter\n size Y \nto Search box";
			double x = Math.Sqrt(double.Parse(tbSearchBase.Text));
			double y = double.Parse(tbSearchY.Text);
			tbSearchBase.Text = null;
			tbSearchY.Text = null;
			var box = service.GetBox(x, y);
			if (box != null)
			{
				lblInfo.Content = $"Box Found:\n  Amount: {box.Amount} in storage";
				selectedBox = box;
				BuyOptionVisible();
			}
			else if (box == null)
			{
				var boxRange = service.SearchBoxesInRange(x, y);
				if (boxRange == null)
					lblInfo.Content = "Sorry,\n No box matches your search \n try different search";
				else
				{
					lblInfo.Content = $" Found Similar Box:" +
						$"\n Base: {boxRange.Base} Y (Height): {boxRange.Y} " +
						$"\n Amount in storage: {boxRange.Amount} " +
						$"\n Please search for this size " +
						$"\n if you are intrested yo buy";
				}
			}
		}
	}
}


