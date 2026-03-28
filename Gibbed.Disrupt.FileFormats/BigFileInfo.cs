using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Gibbed.Disrupt.FileFormats
{
    public class BigFileInfo
    {
        public class Entry
        {
            public string Path;
            public uint Crc;
            public long FilePosition;
            public uint FileSize;
            public uint FileTime; // TNT CRC hash
        }

        public List<Entry> Entries = new List<Entry>();

        public List<Entry> GetEntries()
        {
            return this.Entries;
        }
        
        public void Deserialize(Stream input)
        {
            using (var reader = XmlReader.Create(input))
            {
                while (reader.Read())
                {
                    if (reader.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }

                    if (reader.Name != "File")
                    {
                        continue;
                    }

                    var entry = new Entry();

                    entry.Path = reader.GetAttribute("Path") ?? string.Empty;

                    entry.Crc = uint.Parse(reader.GetAttribute("Crc") ?? "0",
                        CultureInfo.InvariantCulture);

                    entry.FilePosition = long.Parse(reader.GetAttribute("FilePosition") ?? "0",
                        CultureInfo.InvariantCulture);

                    entry.FileSize = uint.Parse(reader.GetAttribute("FileSize") ?? "0",
                        CultureInfo.InvariantCulture);

                    entry.FileTime = uint.Parse(reader.GetAttribute("FileTime") ?? "0",
                        CultureInfo.InvariantCulture);

                    Entries.Add(entry);
                }
            }
        }
    }
}