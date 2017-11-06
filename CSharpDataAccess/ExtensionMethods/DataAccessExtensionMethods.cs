using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace CSharpDataAccess.ExtensionMethods
{
    public static class DataAccessExtensionMethods
    {
        private static readonly IDictionary<Type, ICollection<PropertyInfo>> _Properties = new Dictionary<Type, ICollection<PropertyInfo>>();

        public static IEnumerable<T> ToEntities<T>(this DataTable datatable) where T : class, new()
        {
            try
            {
                var objType = typeof(T);

                lock (_Properties)
                {
                    if (!_Properties.TryGetValue(objType, out ICollection<PropertyInfo> properties))
                    {
                        properties = objType.GetProperties().Where(property => property.CanWrite).ToList();
                        _Properties.Add(objType, properties);
                    }
                }

                var list = new List<T>(datatable.Rows.Count);

                //TODO: https://stackoverflow.com/questions/45900952/convert-datatable-to-ienumerablet-in-asp-net-core-2-0
                //foreach (var row in datatable.AsEnumerable().Skip(1))
                //{
                //    var obj = new T();

                //    foreach (var prop in properties)
                //    {
                //        try
                //        {
                //            var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                //            var safeValue = row[prop.Name] == null ? null : Convert.ChangeType(row[prop.Name], propType);

                //            prop.SetValue(obj, safeValue, null);
                //        }
                //        catch (Exception e)
                //        {
                //            Console.WriteLine(e);
                //        }
                //    }

                //    list.Add(obj);
                //}

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return Enumerable.Empty<T>();
            }
        }
    }
}