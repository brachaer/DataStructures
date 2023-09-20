using BoxProject;
using System.Linq;
using System.Windows.Controls;

namespace WpfBoxFactory.Views
{
	/// <summary>
	/// Interaction logic for ValidBoxList.xaml
	/// </summary>

	public partial class ValidBoxList : UserControl
	{
		readonly BoxService service = BoxService.Init;
		int countOnPage = 12;
		public ValidBoxList()
		{
			InitializeComponent();
			CreatePages();
		}

		public void CreatePages()
		{
			lvValid.Items.Clear();
			var count = service.GetValidBoxesForList().Count();

			if (count > countOnPage)
			{
				var pages = count / countOnPage + 1;
				var countedBoxes = service.GetValidBoxesForList().Take(countOnPage);
				foreach (var item in countedBoxes)
				{
					lvValid.Items.Add(item);
				}
				spButtonsValid.Children.Clear();
				for (int i = 1; i <= pages; i++)
				{
					var btn = new Button { Content = i.ToString() };
					btn.Click += (s, e) => UpdateBoxesList(int.Parse(btn.Content.ToString()));
					spButtonsValid.Children.Add(btn);
				}
			}
		}
		private void UpdateBoxesList(int i)
		{
			lvValid.Items.Clear();
			var countedBoxes = service.GetValidBoxesForList().Skip((i - 1) * countOnPage).Take(countOnPage);
			foreach (var item in countedBoxes)
			{
				lvValid.Items.Add(item);
			}
		}
	}
}
