using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace WpfUtils
{
    public static class Utils
    {
        /// <summary>
        /// GetVisualChild
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        /// <remarks>https://stackoverflow.com/questions/12164079/change-datagrid-cell-value-programmatically-in-wpf</remarks>
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        public static DataGridRow GetSelectedRow(this DataGrid grid)
        {
            return (DataGridRow)grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem);
        }
        public static DataGridRow GetRow(this DataGrid grid, int index)
        {
            DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                // May be virtualized, bring into view and try again.
                grid.UpdateLayout();
                grid.ScrollIntoView(grid.Items[index]);
                row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        /// <summary>
        /// GetCell from DataGrid by DataGridRow and number of column
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns>DataGridCell</returns>
        /// <remarks>https://stackoverflow.com/questions/12164079/change-datagrid-cell-value-programmatically-in-wpf</remarks>
        public static DataGridCell GetCell(this DataGrid grid, DataGridRow row, int column)
        {
            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    grid.ScrollIntoView(row, grid.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }

        /// <summary>
        /// GetCell from DataGrid by number of row and number of column
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns>DataGridCell</returns>
        /// <remarks>https://stackoverflow.com/questions/12164079/change-datagrid-cell-value-programmatically-in-wpf</remarks>

        public static DataGridCell GetCell(this DataGrid grid, int row, int column)
        {
            DataGridRow rowContainer = grid.GetRow(row);
            return grid.GetCell(rowContainer, column);
        }


        /// <summary>
        /// Find T parent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="child"></param>
        /// <remarks>https://stackoverflow.com/questions/30780789/get-column-name-from-object-sender-routedeventargs-e</remarks>
        /// <example>DataGrid parentGrid = FindParent<DataGrid>(sender as TextBox);<br/>
        /// DataGrid parentGrid = FindParent<DataGrid>(sender as DataGridColumnHeader );
        /// </example>
        /// <returns></returns>
        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }


    }
}
