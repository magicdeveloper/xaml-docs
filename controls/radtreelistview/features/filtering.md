---
title: Filtering
page_title: Filtering
description: Filtering
slug: radtreelistview-features-filtering
tags: filtering
published: True
position: 7
---

# Filtering



## 

The __RadTreeListView__ provides you with a built-in filtering functionality, which allows the user to easily filter the data by one or more columns.

Clicking the filtering icon in the column headers opens a menu with the distinct values for the current column and the user can select which of these values to be displayed. The user can enter a filter criterion in the filter box. A drop-down list allows the user to select a filter expression that is applied to the criterion for the column. The case sensitivity can also be specified via a check box. When the user presses the filter button, the grid displays only the records matching the filter criteria specified by using the filter boxes. 

>Only the values at the highest level of the hierarchy are included in the distinct filter list.

In order to filter __TreeListView__'s low level nodes directly through the distinct values listbox, you can manually set its __ItemsSource__.
          Just subscribe to the __DistinctValuesLoading__ event of __TreeListView__ and set the __e.ItemsSource__ to the collection you want to show.
        

For example:

#### __C#__

{{region radtreeliestview-getting-started_10}}
	private void radTreeListView_DistinctValuesLoading(object sender, GridView.GridViewDistinctValuesLoadingEventArgs e)
	{
	    e.ItemsSource = WarehouseService.GetNames();
	}
	{{endregion}}



As the __RadTreeListView__ directly inherits the __RadGridView__ it uses its filtering mechanism. The __RadGridView__ allows you to implement programmatic filtering, custom filtering and more. To learn more read the 
          {% if site.site_name == 'Silverlight' %}[respective topics](http://www.telerik.com/help/silverlight/gridview-filtering-basic.html){% endif %}{% if site.site_name == 'WPF' %}[respective topics](http://www.telerik.com/help/wpf/gridview-filtering-basic.html){% endif %}
        in the __RadGridView's__ documentation.

# See Also

 * [Getting Started]({%slug radtreeliestview-getting-started%})

 * [Hierarchy Column]({%slug radtreelistview-features-hierarchy-column%})

 * [Sorting]({%slug radtreelistview-features-sorting%})

 * [Selection]({%slug radtreelistview-features-selection%})