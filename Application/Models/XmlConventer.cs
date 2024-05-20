using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Npgsql;

namespace Application.Models
{
    public class XmlConventer
    {


        public XmlConventer() { }
        


        public string ConvertToXml(Contract contract)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Contract));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, contract);
                return textWriter.ToString();
            }
        }
       

        public decimal GetTotalPrice(Contract contract)
        {
            string xmlData = ConvertToXml(contract);  
            using (var conn = new NpgsqlConnection("Host=localhost;Database=ContractsDb;Username=postgres;Password=1234"))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT CalculateTotalPrice(@xml_input)", conn))
                {
                    cmd.Parameters.AddWithValue("xml_input", NpgsqlTypes.NpgsqlDbType.Xml, xmlData);
                    var result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? (decimal)result : 0;
                }
            }
        }



    }
}
