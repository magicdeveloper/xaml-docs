using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace WpfApplication1
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
	}

#region radribbonview-ribbon-tab_2
RadRibbonTab currentlySelectedTab = radRibbonView.SelectedTab;
#endregion

#region radribbonview-ribbon-tab_5
private void radRibbonView_PreviewSelectionChanged(object sender, RadSelectionChangedEventArgs e)
{
 // Get the ribbonView
 RadRibbonView ribbonView = sender as RadRibbonView;
 //Get the selected items
 IList selectedItems = e.AddedItems;
 //Get the unselected items
 IList unselectedItems = e.RemovedItems;
 // Cancel the selection
 e.Handled = true;
}
#endregion

#region radribbonview-ribbon-tab_8
private void radRibbonView_SelectionChanged(object sender, RadSelectionChangedEventArgs e)
{
 // Get the ribbonView
 RadRibbonView ribbonView = sender as RadRibbonView;
 //Get the selected items
 IList selectedItems = e.AddedItems;
 //Get the unselected items
 IList unselectedItems = e.RemovedItems;
}
#endregion
}
