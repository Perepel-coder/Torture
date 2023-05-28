using System;
using System.Reflection;

namespace Services
{
    public static class Tools
    {
        public static T CreateInstance<T>(string nameBuild, string nameType, object[]? args = null) where T : class
        {
            Type? type = Assembly.Load(nameBuild).GetType(nameType);
            if (type != null)
            {
                T? result = Activator.CreateInstance(type, args) as T;
                return result == null ?
                    throw new Exception("Не возможно создать экземпляр текущего типа") :
                    result;
            }
            else
            {
                throw new Exception("Этот тип не существует в данной сборке");
            }
        }
        public static bool CompareEquals(object objectFromCompare, object objectToCompare, Type MyType)
        {
            if (objectFromCompare == null && objectToCompare == null)
                return true;
            else if (objectFromCompare == null && objectToCompare != null)
                return false;
            else if (objectFromCompare != null && objectToCompare == null)
                return false;
            PropertyInfo[] props = MyType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                object dataFromCompare = objectFromCompare.GetType().GetProperty(prop.Name).GetValue(objectFromCompare, null);
                object dataToCompare = objectToCompare.GetType().GetProperty(prop.Name).GetValue(objectToCompare, null);
                Type type = objectFromCompare.GetType().GetProperty(prop.Name).GetValue(objectToCompare, null).GetType();
                if (prop.PropertyType.IsClass && !prop.PropertyType.FullName.Contains("System.String"))
                {
                    dynamic convertedFromValue = Convert.ChangeType(dataFromCompare, type);
                    dynamic convertedToValue = Convert.ChangeType(dataToCompare, type);

                    bool compareResult = Tools.CompareEquals(convertedFromValue, convertedToValue, MyType);
                    if (!compareResult)
                        return false;
                }
                else if (!dataFromCompare.Equals(dataToCompare))
                    return false;
            }
            return true;
        }
    }
}
