using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;

namespace GP.TransactionSearch
{
    static class ObjectMapper
    {
        //Usage:  
        //Convert data table to CustomerInfoDetail List<>
        //customerList = ObjectMapper.DataTableToList<CustomerInfoDetail>(dataTable);
        //pmTrx = ObjectMapper.DataRowToObject<PMTransaction>(dataTable.Rows[0]);

        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            //From: https://codereview.stackexchange.com/questions/30714/converting-datatable-to-list-of-class

            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }


        public static T DataRowToObject<T>(this DataRow row) where T : class, new()
        {
            //Variant of DataTableToList code from: https://codereview.stackexchange.com/questions/30714/converting-datatable-to-list-of-class

            try
            {
                List<T> list = new List<T>();

                T obj = new T();

                foreach (var prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                        propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                    }
                    catch
                    {
                        continue;
                    }
                }

                return obj;
            }
            catch
            {
                return null;
            }
        }


        public static List<T> DataRowArrayToList<T>(this DataRow[] dataRowArray) where T : class, new()
        {
            //Variant of DataTableToList code from: https://codereview.stackexchange.com/questions/30714/converting-datatable-to-list-of-class

            try
            {
                List<T> list = new List<T>();

                foreach (var row in dataRowArray.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

    } 
}
