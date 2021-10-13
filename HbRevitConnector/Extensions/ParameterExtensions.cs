using System;
using Autodesk.Revit.DB;

namespace HbRevitConnector.Extensions
{
    internal static class ParameterExtensions
    {
        /// <summary>
        /// Return the corresponding Storage Type value of a given Revit Parameter as a string representation
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Returns null if input parameter is also null </returns>
        public static string GetParameterValueAsString(this Parameter parameter)
        {
            string output = "";

            if (parameter == null) return output;

            switch (parameter.StorageType)
            {
                case StorageType.Double:

                {
                    double value = Math.Round(parameter.AsDouble(), 2);

                    string data = value.ToString();

                    if (data != null)
                        output = data;

                    break;
                }

                case StorageType.Integer:

                {
                    string data = parameter.AsInteger().ToString();

                    if (data != null)
                        output = data;

                    break;
                }

                case StorageType.String:

                {
                    string data = parameter.AsString();

                    if (data != null)
                        output = data;

                    break;
                }

                case StorageType.ElementId:

                {
                    //string data = parameter.AsElementId().IntegerValue.ToString();

                    string data = parameter.AsValueString();

                    if (data != null)
                        output = data;

                    break;
                }

                case StorageType.None:

                {
                    output = "none";

                    break;
                }

                default:

                    throw new ArgumentOutOfRangeException();
            }

            return output;
        }
    }
}