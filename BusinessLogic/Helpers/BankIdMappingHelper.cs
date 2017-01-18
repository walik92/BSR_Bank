using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Xml.Linq;

namespace BusinessLogic.Helpers
{
    /// <summary>
    ///     Mapowanie identyfiaktora banku na jego adres IP
    /// </summary>
    public static class BankIdMappingHelper
    {
        private static readonly string _pathFileOfBanks = @"banks.xml";

        /// <summary>
        ///     Pobierz adres IP banku
        /// </summary>
        /// <param name="id">Identyfikator banku</param>
        /// <returns>Adres IP</returns>
        public static string GetIP(int id)
        {
            var banks = ReadConfigFile();
            if (!banks.ContainsKey(id))
                throw new FaultException($"Bank with the id {id} isn't exist");
            return banks[id];
        }

        private static Dictionary<int, string> ReadConfigFile()
        {
            try
            {
                var xml = XDocument.Load(_pathFileOfBanks);
                return xml.Root.Element("Banks")
                    .Elements("Bank")
                    .ToDictionary(q => int.Parse(q.Element("id").Value), q => q.Element("IP").Value);
            }
            catch (Exception ex)
            {
                throw new Exception("XML file of Banks is incorrect", ex);
            }
        }
    }
}