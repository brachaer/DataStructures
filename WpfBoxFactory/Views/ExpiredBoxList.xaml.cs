using BoxProject;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace WpfBoxFactory.Views
{
    /// <summary>
    /// Interaction logic for ExpiredBoxList.xaml
    /// </summary>
    public partial class ExpiredBoxList : UserControl
    {
        readonly BoxService service = BoxService.Init;
        int countOnPage = 12;
        public ExpiredBoxList()
        {
            InitializeComponent();
            CreatePages();
        }

        private void CreatePages()
        {
            var count = service.GetExpiredBoxes().Count();
            if (count > countOnPage)
            {
                var pages = count / countOnPage + 1;
                var countedBoxes = service.GetExpiredBoxes().Take(countOnPage);
                foreach (var item in countedBoxes)
                {
                    lvExpired.Items.Add(item);
                }

                spButtonsExpired.Children.Clear();
                for (int i = 1; i <= pages; i++)
                {
                    var btn = new Button { Content = i.ToString() };
                    btn.Click += (s, e) => UpdateBoxesList(service.GetExpiredBoxes(), int.Parse(btn.Content.ToString()));
                    spButtonsExpired.Children.Add(btn);
                }
            }
        }
        private void UpdateBoxesList(IEnumerable<Box> boxes, int i)
        {
            lvExpired.Items.Clear();
            var countedBoxes = boxes.Skip((i - 1) * countOnPage).Take(countOnPage);
            foreach (var item in countedBoxes)
            {
                lvExpired.Items.Add(item);
            }
        }

    }
}
