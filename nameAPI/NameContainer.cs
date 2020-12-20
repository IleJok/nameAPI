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
        private const string filePath = "C:/Users/Ilkka/source/repos/nameAPI/nameAPI/names.json";

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
            namesList = new List<Name>();
        }

        private async Task getNamesFromJsonAsync()
        {
            Rootobject listOfNames = new Rootobject();
            try
            {
                using FileStream openStream = File.OpenRead(filePath);
                listOfNames = await JsonSerializer.DeserializeAsync<Rootobject>(openStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            foreach (var item in listOfNames.names)
            {
                namesList.Add(item);
            }


        }

        public async Task<List<Name>> getNamesAsync()
        {
            
            await getNamesFromJsonAsync();
            
            return this.namesList;
        }
    }
}
