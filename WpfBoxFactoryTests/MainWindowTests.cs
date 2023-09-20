using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfBoxFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfBoxFactory.Views;
using BoxProject;

namespace WpfBoxFactory.Tests
{
	[TestClass()]
	public class MainWindowTests
	{
		readonly BoxService service = BoxService.Init;
		ExpiredBoxList expiredBoxesUC = new ExpiredBoxList();
		ValidBoxList validBoxesUC = new ValidBoxList();
		Box selectedBox { get; set; }
		[TestMethod()]
		public void MainWindowTest()
		{
			
			MainWindow mainWindow=new MainWindow();
			Assert.Fail();
		}
	}
}