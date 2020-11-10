using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APIADS.Helpers
{
    /// <summary>
    /// Mapper para la operaciones de conversión de SqlDataReader a objetos.
    /// </summary>
    public static class MapperReader
    {
        /// <summary>
        /// Obtiene una lista de objetos a partir de un SqlDataReader.
        /// </summary>
        /// <typeparam name="T">Clase genérica en donde se obtiene la información del SqlDataReader.</typeparam>
        /// <param name="dataReader">SqlDataReader a leer.</param>
        /// <returns>Lista de objetos.</returns>
        public static List<T> CreateList<T>(SqlDataReader dataReader) where T : class, new()
        {
            var results = new List<T>();
            var properties = typeof(T).GetProperties();
            while (dataReader.Read())
            {
                var item = Activator.CreateInstance<T>();
                foreach (var property in typeof(T).GetProperties())
                {
                    if (IsColumnExists(dataReader, property.Name))
                    {
                        if (!dataReader.IsDBNull(dataReader.GetOrdinal(property.Name)))
                        {
                            Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            property.SetValue(item, Convert.ChangeType(dataReader[property.Name], convertTo), null);
                        }
                    }
                }

                results.Add(item);
            }

            return results;
        }

        /// <summary>
        /// Obtiene un objeto a partir de un SqlDataReader.
        /// </summary>
        /// <typeparam name="T">Clase genérica en donde se obtiene la información del SqlDataReader.</typeparam>
        /// <param name="dataReader">SqlDataReader a leer.</param>
        /// <returns>Objeto T.</returns>
        public static T CreateObject<T>(SqlDataReader dataReader) where T : class, new()
        {
            var properties = typeof(T).GetProperties();
            while (dataReader.Read())
            {
                var item = Activator.CreateInstance<T>();
                foreach (var property in typeof(T).GetProperties())
                {
                    if (IsColumnExists(dataReader, property.Name))
                    {
                        if (!dataReader.IsDBNull(dataReader.GetOrdinal(property.Name)))
                        {
                            Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            property.SetValue(item, Convert.ChangeType(dataReader[property.Name], convertTo), null);
                        }
                    }
                }

                return item;
            }

            return null;
        }

        /// <summary>
        /// Valida si existe un nombre de porpiedad como campo de el IDataReader.
        /// </summary>
        /// <param name="dataReader">IDataReader a validar.</param>
        /// <param name="columnName">Nombre de la columna.</param>
        /// <returns>True = si existe, False = no existe.</returns>
        private static bool IsColumnExists(IDataReader dataReader, string columnName)
        {
            try
            {
                dataReader.GetSchemaTable().DefaultView.RowFilter = $"ColumnName= '{columnName}'";
                if (dataReader.GetSchemaTable().DefaultView.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
            }

            return false;
        }
    }
}
