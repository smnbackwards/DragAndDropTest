/****************************** Module Header ******************************\
 * Module Name:  MainPage.xaml.cs
 * Project:      CSWindowsStoreAppDragAndDropBetweenGroups
 * Copyright (c) Microsoft Corporation.
 * 
 * This sample demonstrates how to drag and drop item between groups in a 
 * grouped GridView.
 *  
 * This source is subject to the Microsoft Public License.
 * See http://www.microsoft.com/en-us/openness/licenses.aspx#MPL
 * All other rights reserved.
 * 
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
 * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DragAndDropTest.Model;
using Windows.ApplicationModel.DataTransfer;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace DragAndDropTest
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        Book draggedItem;
        public MainPage()
        {
            this.InitializeComponent();
            categoryCollectionViewSource.Source = new SampleData().GetCategoryDataSource();
            bookCollectionViewSource.Source = new SampleData().GetBookDataSource();
        }

        private void ItemsByCategory_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            draggedItem = e.Items[0] as Book;
            e.Data.RequestedOperation = DataPackageOperation.Move;
        }

        private void VariableSizedWrapGrid_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (draggedItem != null)
                {
                    var sourceCategory = draggedItem.Cate;
                    var child = (((VariableSizedWrapGrid)sender).Children[0] as GridViewItem).Content as Book;
                    draggedItem.Cate = child.Cate;

                    child.Cate.BookList.Add(draggedItem);
                    sourceCategory.BookList.Remove(draggedItem);
                    draggedItem = null;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void VariableSizedWrapGrid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
        }
    }
}
