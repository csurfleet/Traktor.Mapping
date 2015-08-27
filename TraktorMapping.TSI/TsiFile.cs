using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;
using TraktorMapping.TSI.Format;
using TraktorMapping.TSI.Utils;

namespace TraktorMapping.TSI
{
    public class TsiFile
    {
        private const string XPATH_TO_DATA = "/NIXML/TraktorSettings/Entry[@Name='DeviceIO.Config.Controller']";

        private readonly DeviceMappingsContainer _devicesContainer;

        private string fileContents;
        private string filePath;

        public TsiFile(string filePath)
        {
            this.filePath = filePath;
            fileContents = File.ReadAllText(filePath);

            XDocument xml;
            using (TextReader textReader = new StringReader(fileContents))
            {
                xml = XDocument.Load(textReader);
            }

            string data = xml.XPathSelectElement(XPATH_TO_DATA).Attribute("Value").Value;

            byte[] decoded = Convert.FromBase64String(data);

            _devicesContainer = new DeviceMappingsContainer(new MemoryStream(decoded));
            Devices = _devicesContainer.Devices.List.AsReadOnly();
        }

        public IReadOnlyCollection<Device> Devices { get; private set; }

        public void Save()
        {
            MemoryStream stream = new MemoryStream();

            Writer writer = new Writer(stream);
            _devicesContainer.Write(writer);

            stream.Seek(0, SeekOrigin.Begin);

            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);

            string tsiData = Convert.ToBase64String(data, Base64FormattingOptions.None);

            string newFileContents = Regex.Replace(fileContents,
                              "<Entry Name=\"DeviceIO.Config.Controller\"(.*)Value=\".*\"",
                              String.Format("<Entry Name=\"DeviceIO.Config.Controller\"$1Value=\"{0}\"", tsiData));

            File.WriteAllText(filePath, newFileContents);
        }
    }
}
