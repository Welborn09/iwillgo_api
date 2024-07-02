using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IWillGo.DataAccess
{
    public static class Extensions
    {
        public static decimal GetDecimal(this IDataReader reader, string name)
        {
            try
            {
                if (reader.IsDBNull(name))
                    return 0;
                return reader.GetDecimal(reader.GetOrdinal(name));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting value from reader: {name}. Message: {ex.Message}", ex);
            }
        }
        public static bool GetBoolean(this IDataReader reader, string name)
        {
            try
            {
                if (reader.IsDBNull(name))
                    return false;
                return reader.GetBoolean(reader.GetOrdinal(name));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting value from reader: {name}. Message: {ex.Message}", ex);
            }
        }
        public static string GetString(this IDataReader reader, string name)
        {
            try
            {
                if (reader.IsDBNull(name))
                    return string.Empty;
                return reader.GetString(reader.GetOrdinal(name));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting value from reader: {name}. Message: {ex.Message}", ex);
            }
        }
        public static DateTime GetDate(this IDataReader reader, string name)
        {
            try
            {
                if (reader.IsDBNull(name))
                    return new DateTime();
                return reader.GetDateTime(reader.GetOrdinal(name));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting value from reader: {name}. Message: {ex.Message}", ex);
            }
        }
        public static DateTime? GetNullableDate(this IDataReader reader, string name)
        {
            try
            {
                if (reader.IsDBNull(reader.GetOrdinal(name)))
                    return null;
                return reader.GetDateTime(reader.GetOrdinal(name)) as DateTime?;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting value from reader: {name}. Message: {ex.Message}", ex);
            }
        }
        public static int GetInt(this IDataReader reader, string name)
        {
            try
            {
                if (reader.IsDBNull(reader.GetOrdinal(name)))
                    return 0;
                return reader.GetInt32(reader.GetOrdinal(name));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting value from reader: {name}. Message: {ex.Message}", ex);
            }
        }
        public static int GetTinyInt(this IDataReader reader, string name)
        {
            try
            {
                if (reader.IsDBNull(reader.GetOrdinal(name)))
                    return 0;
                return reader.GetInt16(reader.GetOrdinal(name));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting value from reader: {name}. Message: {ex.Message}", ex);
            }
        }
        public static string GetGuid(this IDataReader reader, string name)
        {
            try
            {
                if (reader.IsDBNull(reader.GetOrdinal(name)))
                    return null;
                return reader.GetGuid(reader.GetOrdinal(name)).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting value from reader: {name}. Message: {ex.Message}", ex);
            }
        }
        public static bool IsDBNull(this IDataReader reader, string name)
        {
            try
            {
                if (!reader.HasColumn(name))
                    return true;
                return reader.IsDBNull(reader.GetOrdinal(name));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting IsDBNull from reader: {name}. Message: {ex.Message}", ex);
            }
        }
        public static bool HasColumn(this IDataReader dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
        public static string ToSqlXml(this IEnumerable<string> ids)
        {
            /*
                Returns:
                <ArrayOfString>
	                <string>d834351e-32aa-4228-b89b-1f3c30d52dd0</string>
	                <string>5c044756-2f0b-43e0-9a30-34afbe87d04c</string>
	                <string>4a8e05e5-90c3-4e28-9c8c-b74bbcb54b73</string>
	                <string>45f1047d-db11-4aeb-bb40-e69bae4569aa</string>
                </ArrayOfString>
            */
            XmlSerializer xs = new XmlSerializer(typeof(List<string>));
            using (var stream = new StringWriter())
            {
                xs.Serialize(stream, ids.ToList());
                return stream.ToString();
            }

        }
    }
}
