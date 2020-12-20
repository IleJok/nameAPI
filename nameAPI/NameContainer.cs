using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace nameAPI
{
    public class NameContainer
    {
        /*Singleton class to read the names from JSON, no need to have multiple name containers*/
        private static NameContainer nameContainer;
        private List<Name> namesList = null;

        public static NameContainer GetNameContainer()
        {
            if (nameContainer == null)
            {
                /* If we the nameContainer is null, let's create a new object*/
                nameContainer = new NameContainer();
            }
            return nameContainer;
        }

        private NameContainer()
        {
            this.namesList = new List<Name>();
        }

        public async Task getNamesFromJsonAsync()
        {
            try
            {
                using FileStream openStream = File.OpenRead("names.json");
                namesList = await JsonSerializer.DeserializeAsync<List<Name>>(openStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        public List<Name> getNames()
        {
            return this.namesList;
        }
    }
}
