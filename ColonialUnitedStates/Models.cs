using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColonialUnitedStates
{
    public class USColony
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public int YearEstablished { get; set; }
        public int NumberToJoinTheUnion { get; set; }
    }

    public class USColonyAddEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public int YearEstablished { get; set; }
        public int NumberToJoinTheUnion { get; set; }
    }

    public class USColonyTableEntity : TableEntity
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public int YearEstablished { get; set; }
        public int NumberToJoinTheUnion { get; set; }
    }

    public static class Mappings
    {
        public static USColonyTableEntity ToTableEntity(this USColony colony)
        {
            return new USColonyTableEntity()
            {
                PartitionKey = "USColonies",
                RowKey = colony.Id,
                Name = colony.Name,
                Capital= colony.Capital,
                YearEstablished= colony.YearEstablished,
                NumberToJoinTheUnion= colony.NumberToJoinTheUnion

            };
        }

        public static USColony ToUSColony(this USColonyTableEntity colony)
        {
            return new USColony()
            {
                Id = colony.RowKey,
                Name = colony.Name,
                Capital = colony.Capital,
                YearEstablished = colony.YearEstablished,
                NumberToJoinTheUnion = colony.NumberToJoinTheUnion
            };
        }

    }
}
